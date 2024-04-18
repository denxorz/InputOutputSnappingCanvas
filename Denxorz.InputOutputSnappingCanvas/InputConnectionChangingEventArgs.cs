namespace Denxorz.InputOutputSnappingCanvas;

public class InputConnectionChangingEventArgs(
    object? intput,
    object? oldOutput,
    object? newOutput) : EventArgs
{
    public bool IsCancelled { get; set; }
    public object? Input { get; } = intput;
    public object? OldOutput { get; } = oldOutput;
    public object? NewOutput { get; } = newOutput;
}
