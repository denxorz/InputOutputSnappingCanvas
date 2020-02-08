namespace Denxorz.InputOutputSnappingCanvas
{
    public interface IConnectionInput
    {
        IConnectionOutput ConnectedOutput { get; set; }

        object GetObjectFromConnectedOutput();
    }
}
