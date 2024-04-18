namespace Denxorz.InputOutputSnappingCanvas;

public class OutputConnectionChangedEventArgs(
    object? output,
    object? oldInput,
    object? newInput) : EventArgs
{
    public object? Output { get; } = output;
    public object? OldInput { get; } = oldInput;
    public object? NewInput { get; } = newInput;
}
