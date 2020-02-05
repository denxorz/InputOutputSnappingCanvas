namespace Denxorz.SnappingCanvas
{
    public interface IConnectionInput
    {
        IConnectionOutput ConnectedOutput { get; set; }

        object GetObjectFromConnectedOutput();
    }
}
