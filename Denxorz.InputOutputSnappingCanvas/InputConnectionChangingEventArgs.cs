using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public class InputConnectionChangingEventArgs : EventArgs
    {
        public bool IsCancelled { get; set; }
        public object Input { get; }
        public object OldOutput { get; }
        public object NewOutput { get; }

        public InputConnectionChangingEventArgs(
            object intput,
            object oldOutput,
            object newOutput)
        {
            Input = intput;
            OldOutput = oldOutput;
            NewOutput = newOutput;
        }
    }
}
