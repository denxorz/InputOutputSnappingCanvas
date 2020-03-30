using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public class OutputConnectionChangedEventArgs : EventArgs
    {
        public object Output { get; }
        public object OldInput { get; }
        public object NewInput { get; }

        public OutputConnectionChangedEventArgs(
            object output,
            object oldInput,
            object newInput)
        {
            Output = output;
            OldInput = oldInput;
            NewInput = newInput;
        }
    }
}
