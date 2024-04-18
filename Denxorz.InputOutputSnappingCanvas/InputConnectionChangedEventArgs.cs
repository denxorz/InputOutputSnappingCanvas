namespace Denxorz.InputOutputSnappingCanvas;

public class InputConnectionChangedEventArgs(
    object? input,
    object? oldOutput,
    object? newOutput) : EventArgs
{
    public object? Input { get; } = input;
    public object? OldOutput { get; } = oldOutput;
    public object? NewOutput { get; } = newOutput;
}
