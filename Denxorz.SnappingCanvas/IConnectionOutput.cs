namespace Denxorz.SnappingCanvas
{
    public interface IConnectionOutput
    {
        IConnectionInput ConnectedInput { get; set; }

        object ObjectToOutput { get; }
    }
}
