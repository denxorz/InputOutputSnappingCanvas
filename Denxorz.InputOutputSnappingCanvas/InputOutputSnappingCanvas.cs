using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF.JoshSmith.Controls;

namespace Denxorz.InputOutputSnappingCanvas
{
    public class InputOutputSnappingCanvas : DragCanvas
    {
        private bool hasSnapped = false;
        private Point lastSnap = new(0, 0);

        public int SnapThresholdX { get; set; } = 5;
        public int SnapThresholdY { get; set; } = 5;
        public int UnsnapThresholdX { get; set; } = 2;
        public int UnsnapThresholdY { get; set; } = 2;

        public void ForceSnapAll()
        {
            Application.Current.Dispatcher.VerifyAccess();

            foreach (var host in GetAllHosts())
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

        public void ForceLinkAll()
        {
            Application.Current.Dispatcher.VerifyAccess();

            var allHosts = GetAllHosts();
            var allInputs = allHosts.SelectMany(h => h.Inputs).ToArray();
            var allOutputs = allHosts.SelectMany(h => h.Outputs).ToArray();

            foreach (IConnectionInput input in allInputs)
            {
                foreach (IConnectionOutput output in allOutputs)
                {
                    TryToLink(input, output);
                }
            }

            RemoveDeadLinks(allInputs, allOutputs);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ElementBeingDragged != null && IsDragInProgress)
            {
                Snap(e.GetPosition(this));
            }
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
                if (hasSnapped
                    && (Math.Abs(cursorLocation.X - lastSnap.X) > UnsnapThresholdX
                      || Math.Abs(cursorLocation.Y - lastSnap.Y) > UnsnapThresholdY))
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

        private IEnumerable<IConnectionOutput> SnapableOutputs(ISnapHost host)
        {
            return GetOtherHosts(host).SelectMany(h => h.Outputs);
        }

        private IEnumerable<IConnectionInput> SnapableInputs(ISnapHost host)
        {
            return GetOtherHosts(host).SelectMany(h => h.Inputs);
        }

        private IEnumerable<ISnapHost> GetOtherHosts(ISnapHost host)
        {
            return GetAllHosts().Where(t => t != host);
        }

        private IReadOnlyCollection<ISnapHost> GetAllHosts()
        {
            return Children.OfType<ISnapHost>().ToArray();
        }

        private void TryToSnapInputToOutput(Point cursorLocation, ISnapHost inputHost, IConnectionInput input)
        {
            var host = inputHost as UIElement;
            foreach (IConnectionOutput output in SnapableOutputs(inputHost))
            {
                TryToSnap(host, cursorLocation, input, output, SnapDirection.ToOutput);
            }
        }

        private void TryToSnapOutputToInput(Point cursorLocation, ISnapHost outputHost, IConnectionOutput output)
        {
            var host = outputHost as UIElement;
            foreach (IConnectionInput input in SnapableInputs(outputHost))
            {
                TryToSnap(host, cursorLocation, input, output, SnapDirection.ToInput);
            }
        }

        private void TryToSnap(UIElement host, Point cursorLocation, IConnectionInput input, IConnectionOutput output, SnapDirection direction)
        {
            if (AreInSnapRange(output, input, out double xdiff, out double ydiff))
            {
                if (input.AllowsSnapTo(output))
                {
                    if (direction == SnapDirection.ToOutput)
                    {
                        xdiff *= -1;
                        ydiff *= -1;
                    }
                    Snap(host, cursorLocation, input, output, xdiff, ydiff);
                }
            }
        }

        private void TryToLink(IConnectionInput input, IConnectionOutput output)
        {
            if (AreInSnapRange(output, input, out _, out _))
            {
                if (input.AllowsSnapTo(output))
                {
                    input.ConnectedOutput = output;
                    output.ConnectedInput = input;
                }
            }
        }

        private void Snap(UIElement host, Point cursorLocation, IConnectionInput input, IConnectionOutput output, double xdiff, double ydiff)
        {
            Point elementBeingDraggedPoint = host.TranslatePoint(new(0, 0), this);

            SetLeft(host, elementBeingDraggedPoint.X + xdiff);
            SetTop(host, elementBeingDraggedPoint.Y + ydiff);

            lastSnap = cursorLocation;
            hasSnapped = true;

            input.ConnectedOutput = output;
            output.ConnectedInput = input;
        }

        private bool AreInSnapRange(IConnectionOutput output, IConnectionInput input, out double xdiff, out double ydiff)
        {
            const double visualDistanceBetweenInAndOut = -13;

            Point outputPoint = ((UIElement)output).TranslatePoint(new(0, 0), this);
            Point inputPoint = ((UIElement)input).TranslatePoint(new(0, 0), this);

            xdiff = inputPoint.X - outputPoint.X + visualDistanceBetweenInAndOut;
            ydiff = inputPoint.Y - outputPoint.Y;

            return Math.Abs(xdiff) < SnapThresholdX
                && Math.Abs(ydiff) < SnapThresholdY;
        }

        private void UnSetSnap()
        {
            lastSnap = new(0, 0);
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

        public List<List<object>> GetGroups()
        {
            Application.Current.Dispatcher.VerifyAccess();

            ForceLinkAll();

            var hosts = Children.OfType<ISnapHost>().ToList();
            var startList = hosts.ToList();
            List<List<object>> groups = new List<List<object>>();

            while (startList.Any())
            {
                var startItem = startList.First();

                var linkedItems = GetLinkedHosts(hosts, startItem, null).Distinct().ToList();
                foreach (var host in linkedItems)
                {
                    startList.Remove(host);
                }

                if (linkedItems.Count > 1)
                {
                    groups.Add(new List<object>(linkedItems.Select(t => t.DataContext)));
                }
            }

            return groups;
        }

        private List<ISnapHost> GetLinkedHosts(List<ISnapHost> hosts, ISnapHost startItem, ISnapHost skip)
        {
            var linkedItems = new List<ISnapHost> { startItem };

            foreach (var output in startItem.Outputs.Where(o => o.ConnectedInput != null))
            {
                var item = hosts.First(s => s.Inputs.Contains(output.ConnectedInput));
                if (item != skip)
                {
                    linkedItems.AddRange(GetLinkedHosts(hosts, item, startItem));
                }
            }

            foreach (var input in startItem.Inputs.Where(o => o.ConnectedOutput != null))
            {
                var item = hosts.First(s => s.Outputs.Contains(input.ConnectedOutput));
                if (item != skip)
                {
                    linkedItems.AddRange(GetLinkedHosts(hosts, item, startItem));
                }
            }

            return linkedItems;
        }

        private void RemoveDeadLinks(IReadOnlyCollection<IConnectionInput> allInputs, IReadOnlyCollection<IConnectionOutput> allOutputs)
        {
            var deadInputLinks = allInputs
                .Where(input => input.ConnectedOutput != null && !allOutputs.Contains(input.ConnectedOutput))
                .ToList();

            foreach (var dead in deadInputLinks)
            {
                dead.ConnectedOutput.ConnectedInput = null;
                dead.ConnectedOutput = null;
            }

            var deadOutputLinks = allOutputs
                .Where(output => output.ConnectedInput != null && !allInputs.Contains(output.ConnectedInput))
                .ToList();

            foreach (var dead in deadOutputLinks)
            {
                dead.ConnectedInput.ConnectedOutput = null;
                dead.ConnectedInput = null;
            }
        }

        private enum SnapDirection { ToInput, ToOutput };
    }
}
