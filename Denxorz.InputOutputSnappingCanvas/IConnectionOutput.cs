using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public interface IConnectionOutput
    {
        event EventHandler<OutputConnectionChangedEventArgs> ConnectionChanged;
        
        IConnectionInput ConnectedInput { get; set; }

        object Context { get; set; }
    }
}
