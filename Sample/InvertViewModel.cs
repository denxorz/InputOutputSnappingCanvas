using Denxorz.InputOutputSnappingCanvas;
using System.Windows.Media;

namespace Sample;

public class InvertViewModel
{
    private IConnectionInput? inControl;
    private ColorProvider? connectedProvider;

    public double Top { get; set; }
    public double Left { get; set; }

    public ColorProvider OutputProvider { get; } = new ColorProvider();

    public InvertViewModel() { }

    public InvertViewModel(double left, double top)
        : this()
    {
        Left = left;
        Top = top;
    }

    public void SetUiControls(IConnectionInput inputControl)
    {
        inControl = inputControl;
        inControl.ConnectionChanging += OnConnectionChanging;
        inControl.ConnectionChanged += OnConnectionChanged;
    }

    public override string ToString()
    {
        return "Color inverter";
    }

    private void UpdateColor(ColorProvider? provider)
    {
        if (provider?.Color != null)
        {
            OutputProvider.UpdateColor(new SolidColorBrush(Color.FromRgb((byte)~provider.Color.Color.R, (byte)~provider.Color.Color.G, (byte)~provider.Color.Color.B)));
        }
        else
        {
            OutputProvider.RemoveColor();
        }
    }

    private void OnConnectionChanging(object? sender, InputConnectionChangingEventArgs e)
    {
        e.IsCancelled = e.NewOutput is not ColorProvider;
    }

    private void OnConnectionChanged(object? sender, EventArgs e)
    {
        if (inControl?.GetContextFromConnectedOutput() is ColorProvider provider)
        {
            connectedProvider = provider;
            connectedProvider.ColorUpdated += ConnectedProvider_ColorUpdated;
        }
        else
        {
            if (connectedProvider != null)
            {
                connectedProvider.ColorUpdated -= ConnectedProvider_ColorUpdated;
            }
            connectedProvider = null;
        }
        UpdateColor(connectedProvider);
    }

    private void ConnectedProvider_ColorUpdated(object? sender, EventArgs e)
    {
        UpdateColor(connectedProvider);
    }
}
