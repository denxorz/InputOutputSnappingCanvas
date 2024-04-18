using Denxorz.InputOutputSnappingCanvas;
using System.Windows.Media;

namespace Sample;

public class DarkLightViewModel
{
    private IConnectionInput? inControl;
    private ColorProvider? connectedProvider;

    public double Top { get; set; }
    public double Left { get; set; }

    public ColorProvider LightOutputProvider { get; } = new ColorProvider();
    public ColorProvider DarkOutputProvider { get; } = new ColorProvider();

    public DarkLightViewModel() { }

    public DarkLightViewModel(double left, double top)
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

    private void UpdateColor(ColorProvider? provider)
    {
        if (provider?.Color != null)
        {
            var inputColorName = ColorProvider.GetColorName(provider.Color);

            if (!LightOutputProvider.UpdateColor($"Light{inputColorName}"))
            {
                LightOutputProvider.UpdateColor(ChangeColorBrightness(provider.Color, 0.5f));
            }

            if (!DarkOutputProvider.UpdateColor($"Dark{inputColorName}"))
            {
                DarkOutputProvider.UpdateColor(ChangeColorBrightness(provider.Color, -0.5f));
            }
        }
        else
        {
            LightOutputProvider.RemoveColor();
            DarkOutputProvider.RemoveColor();
        }
    }

    private static SolidColorBrush ChangeColorBrightness(SolidColorBrush brush, float correctionFactor)
    {
        var color = brush.Color;
        float red = color.R;
        float green = color.G;
        float blue = color.B;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (255 - red) * correctionFactor + red;
            green = (255 - green) * correctionFactor + green;
            blue = (255 - blue) * correctionFactor + blue;
        }

        return new(Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue));
    }

    public override string ToString()
    {
        return "Darker/lighter modifier";
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
