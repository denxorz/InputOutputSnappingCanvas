using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public class InputConnectionChangedEventArgs : EventArgs
    {
        public object Input { get; }
        public object OldOutput { get; }
        public object NewOutput { get; }

        public InputConnectionChangedEventArgs(
            object input,
            object oldOutput,
            object newOutput)
        {
            Input = input;
            OldOutput = oldOutput;
            NewOutput = newOutput;
        }
    }
}
