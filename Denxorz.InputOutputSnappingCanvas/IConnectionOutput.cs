namespace Denxorz.InputOutputSnappingCanvas
{
    public interface IConnectionOutput
    {
        IConnectionInput ConnectedInput { get; set; }

        object ObjectToOutput { get; }
    }
}
