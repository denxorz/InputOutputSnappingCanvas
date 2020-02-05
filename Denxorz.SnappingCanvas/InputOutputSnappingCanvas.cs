using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.JoshSmith.Controls;

namespace Denxorz.SnappingCanvas
{
    public class InputOutputSnappingCanvas : DragCanvas
    {
        private const int snapThresholdX = 5;
        private const int snapThresholdY = 5;
        private bool hasSnapped = false;
        private Point lastSnap = new Point(0, 0);

        public void ForceSnapAll()
        {
            foreach (var host in Children.OfType<ISnapHost>())
            {
                foreach (IConnectionInput input in SnapableInputs(host))
                {
                    TryToSnapInputToOutput(lastSnap, host, input);
                }

                foreach (IConnectionOutput output in SnapableOutputs(host))
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

                foreach (IConnectionInput input in host.Inputs)
                {
                    TryToSnapInputToOutput(cursorLocation, host, input);
                }

                foreach (IConnectionOutput output in host.Outputs)
                {
                    TryToSnapOutputToInput(cursorLocation, host, output);
                }
            }
        }

        private List<IConnectionOutput> SnapableOutputs(ISnapHost host)
        {
            // TODO : don't snap to already snapped
            var ret = new List<IConnectionOutput>();
            foreach (ISnapHost snapHost in Children.OfType<ISnapHost>().Where(t => t != host))
            {
                ret.AddRange(snapHost.Outputs);
            }
            return ret;
        }

        private List<IConnectionInput> SnapableInputs(ISnapHost host)
        {
            // TODO : don't snap to already snapped
            var ret = new List<IConnectionInput>();
            foreach (ISnapHost snapHost in Children.OfType<ISnapHost>().Where(t => t != host))
            {
                ret.AddRange(snapHost.Inputs);
            }
            return ret;
        }

        private void TryToSnapInputToOutput(Point cursorLocation, ISnapHost inputHost, IConnectionInput input)
        {
            var host = inputHost as UIElement;
            foreach (IConnectionOutput output in SnapableOutputs(inputHost))
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

                    input.ConnectedOutput = output;
                    output.ConnectedInput = input;
                }
            }
        }

        private void TryToSnapOutputToInput(Point cursorLocation, ISnapHost outputHost, IConnectionOutput output)
        {
            var host = outputHost as UIElement;
            foreach (IConnectionInput input in SnapableInputs(outputHost))
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

                    output.ConnectedInput = input;
                    input.ConnectedOutput = output;
                }
            }
        }

        private bool AreInSnapRange(IConnectionOutput output, IConnectionInput input, out double xdiff, out double ydiff)
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
            foreach (IConnectionInput input in host.Inputs)
            {
                if (input.ConnectedOutput != null)
                {
                    input.ConnectedOutput.ConnectedInput = null;
                    input.ConnectedOutput = null;
                }
            }

            foreach (IConnectionOutput output in host.Outputs)
            {
                if (output.ConnectedInput != null)
                {
                    output.ConnectedInput.ConnectedOutput = null;
                    output.ConnectedInput = null;
                }
            }
        }
    }


}
