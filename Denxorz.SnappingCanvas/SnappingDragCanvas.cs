using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.JoshSmith.Controls;

namespace Denxorz
{
    public class SnappingDragCanvas : DragCanvas
    {
        private const int snapThresholdX = 5;
        private const int snapThresholdY = 5;
        private bool hasSnapped = false;
        private Point lastSnap = new Point(0, 0);

        //public IReadOnlyCollection<UserControl> GetAllUserControls(Panel panel)
        //{
        //    List<UserControl> all = new List<UserControl>();
        //    foreach(var control in panel.Children)
        //    {
        //        if (control is Panel p)
        //        {
        //            all.AddRange(GetAllUserControls(p));
        //        }
        //        else if (control is UserControl c)
        //        {
        //            all.Add(c);
        //        }
        //    }
        //    return all;
        //}

        public void ForceSnapAll()
        {
            foreach (var host in Children.OfType<ISnapHost>())
            {
                foreach (ISnapInput input in SnapableInputs(host))
                {
                    TryToSnapInputToOutput(lastSnap, host, input);
                }

                foreach (ISnapOutput output in SnapableOutputs(host))
                {
                    TryToSnapOutputToInput(lastSnap, host, output);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // If no element is being dragged, there is nothing to do.
            if (ElementBeingDragged == null || !IsDragInProgress)
            {
                return;
            }

            Snap(e.GetPosition(this));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            UnSetSnap();
        }

        private void Snap(Point cursorLocation)
        {
            if (ElementBeingDragged is ISnapHost host)
            {
                const int unsnapThresholdX = 2;
                const int unsnapThresholdY = 2;

                if (hasSnapped && (Math.Abs(cursorLocation.X - lastSnap.X) > unsnapThresholdX || Math.Abs(cursorLocation.Y - lastSnap.Y) > unsnapThresholdY))
                {
                    UnSetSnap();
                    return;
                }

                foreach (ISnapInput input in host.Inputs)
                {
                    TryToSnapInputToOutput(cursorLocation, host, input);
                }

                foreach (ISnapOutput output in host.Outputs)
                {
                    TryToSnapOutputToInput(cursorLocation, host, output);
                }
            }
        }

        private List<ISnapOutput> SnapableOutputs(ISnapHost host)
        {
            // TODO : don't snap to already snapped
            var ret = new List<ISnapOutput>();
            foreach (ISnapHost snapHost in Children.OfType<ISnapHost>().Where(t => t != host))
            {
                ret.AddRange(snapHost.Outputs);
            }
            return ret;
        }

        private List<ISnapInput> SnapableInputs(ISnapHost host)
        {
            // TODO : don't snap to already snapped
            var ret = new List<ISnapInput>();
            foreach (ISnapHost snapHost in Children.OfType<ISnapHost>().Where(t => t != host))
            {
                ret.AddRange(snapHost.Inputs);
            }
            return ret;
        }

        private void TryToSnapInputToOutput(Point cursorLocation, ISnapHost inputHost, ISnapInput input)
        {
            var host = inputHost as UIElement;
            foreach (ISnapOutput output in SnapableOutputs(inputHost))
            {
                double xdiff;
                double ydiff;
                if (AreInSnapRange(output, input, out xdiff, out ydiff))
                {
                    Point elementBeingDraggedPoint = host.TranslatePoint(new Point(0, 0), this);

                    Canvas.SetLeft(host, elementBeingDraggedPoint.X - xdiff);
                    Canvas.SetTop(host, elementBeingDraggedPoint.Y - ydiff);

                    lastSnap = cursorLocation;
                    hasSnapped = true;

                    input.SnappedOutput = output;
                    output.SnappedInput = input;
                }
            }
        }

        private void TryToSnapOutputToInput(Point cursorLocation, ISnapHost outputHost, ISnapOutput output)
        {
            var host = outputHost as UIElement;
            foreach (ISnapInput input in SnapableInputs(outputHost))
            {
                double xdiff;
                double ydiff;
                if (AreInSnapRange(output, input, out xdiff, out ydiff))
                {
                    Point elementBeingDraggedPoint = host.TranslatePoint(new Point(0, 0), this);

                    Canvas.SetLeft(host, elementBeingDraggedPoint.X + xdiff);
                    Canvas.SetTop(host, elementBeingDraggedPoint.Y + ydiff);

                    lastSnap = cursorLocation;
                    hasSnapped = true;

                    output.SnappedInput = input;
                    input.SnappedOutput = output;
                }
            }
        }

        private bool AreInSnapRange(ISnapOutput output, ISnapInput input, out double xdiff, out double ydiff)
        {
            const double visualDistanceBetweenInAndOut = -13;

            Point outputPoint = ((UIElement)output).TranslatePoint(new Point(0, 0), this);
            Point inputPoint = ((UIElement)input).TranslatePoint(new Point(0, 0), this);

            xdiff = inputPoint.X - outputPoint.X + visualDistanceBetweenInAndOut;
            ydiff = inputPoint.Y - outputPoint.Y;

            return Math.Abs(xdiff) < snapThresholdX
                && Math.Abs(ydiff) < snapThresholdY;
        }

        private void UnSetSnap()
        {
            lastSnap = new Point(0, 0);
            hasSnapped = false;

            if (!(ElementBeingDragged is ISnapHost))
            {
                return;
            }
            var host = ElementBeingDragged as ISnapHost;
            foreach (ISnapInput input in host.Inputs)
            {
                if (input.SnappedOutput != null)
                {
                    input.SnappedOutput.SnappedInput = null;
                    input.SnappedOutput = null;
                }
            }

            foreach (ISnapOutput output in host.Outputs)
            {
                if (output.SnappedInput != null)
                {
                    output.SnappedInput.SnappedOutput = null;
                    output.SnappedInput = null;
                }
            }
        }
    }


}
