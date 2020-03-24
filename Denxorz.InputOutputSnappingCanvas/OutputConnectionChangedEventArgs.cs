using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public class OutputConnectionChangedEventArgs : EventArgs
    {
        public IConnectionOutput Output { get; }
        public IConnectionInput OldInput { get; }
        public IConnectionInput NewInput { get; }

        public OutputConnectionChangedEventArgs(
            IConnectionOutput output,
            IConnectionInput oldInput,
            IConnectionInput newInput)
        {
            Output = output;
            OldInput = oldInput;
            NewInput = newInput;
        }
    }
}
