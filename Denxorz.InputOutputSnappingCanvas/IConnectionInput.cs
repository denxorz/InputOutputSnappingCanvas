using System;

namespace Denxorz.InputOutputSnappingCanvas
{
    public interface IConnectionInput
    {
        event EventHandler<InputConnectionChangedEventArgs> ConnectionChanged;
        event EventHandler<InputConnectionChangingEventArgs> ConnectionChanging;

        IConnectionOutput ConnectedOutput { get; set; }

        object GetContextFromConnectedOutput();

        bool AllowsSnapTo(IConnectionOutput output);

        object Context { get; set; }
    }
}
