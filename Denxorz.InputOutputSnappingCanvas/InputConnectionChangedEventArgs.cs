using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public class InputConnectionChangedEventArgs : EventArgs
    {
        public IConnectionInput Input { get; }
        public IConnectionOutput OldOutput { get; }
        public IConnectionOutput NewOutput { get; }

        public InputConnectionChangedEventArgs(
            IConnectionInput input,
            IConnectionOutput oldOutput,
            IConnectionOutput newOutput)
        {
            Input = input;
            OldOutput = oldOutput;
            NewOutput = newOutput;
        }
    }
}
