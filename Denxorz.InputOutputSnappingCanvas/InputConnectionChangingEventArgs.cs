using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public class InputConnectionChangingEventArgs : EventArgs
    {
        public bool IsCancelled { get; set; }
        public IConnectionInput Input { get; }
        public IConnectionOutput OldOutput { get; }
        public IConnectionOutput NewOutput { get; }

        public InputConnectionChangingEventArgs(
            IConnectionInput intput,
            IConnectionOutput oldOutput,
            IConnectionOutput newOutput)
        {
            Input = intput;
            OldOutput = oldOutput;
            NewOutput = newOutput;
        }
    }
}
