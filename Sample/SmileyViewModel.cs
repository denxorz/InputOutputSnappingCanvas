using Denxorz.InputOutputSnappingCanvas;
using System.ComponentModel;
using System.Windows.Media;

namespace Sample;

class SmileyViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private ColorProvider? connectedProvider1;
    private ColorProvider? connectedProvider2;

    private IConnectionInput? inControl1;
    private IConnectionInput? inControl2;

    public SolidColorBrush? Color1 { get; private set; }
    public SolidColorBrush? Color2 { get; private set; }
    public double Top { get; set; }
    public double Left { get; set; }

    public SmileyViewModel() { }

    public SmileyViewModel(double left, double top)
        : this()
    {
        Left = left;
        Top = top;
    }

    public void SetUiControls(IConnectionInput inputControl1, IConnectionInput inputControl2)
    {
        inControl1 = inputControl1;
        inControl1.ConnectionChanged += OnConnectionChanged1;
        inControl1.ConnectionChanging += OnConnectionChanging;

        inControl2 = inputControl2;
        inControl2.ConnectionChanged += OnConnectionChanged2;
        inControl2.ConnectionChanging += OnConnectionChanging;

        UpdateColor(null, null);
    }

    public override string ToString()
    {
        return "Smiley";
    }

    private void UpdateColor(ColorProvider? provider1, ColorProvider? provider2)
    {
        Color1 = provider1?.Color;
        Color2 = provider2?.Color;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color1)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color2)));
    }

    private void OnConnectionChanging(object? sender, InputConnectionChangingEventArgs e)
    {
        e.IsCancelled = e.NewOutput is not ColorProvider;
    }

    private void OnConnectionChanged1(object? sender, EventArgs e)
    {
        if (inControl1?.GetContextFromConnectedOutput() is ColorProvider provider)
        {
            connectedProvider1 = provider;
            connectedProvider1.ColorUpdated += ConnectedProvider_ColorUpdated;
        }
        else
        {
            if (connectedProvider1 != null)
            {
                connectedProvider1.ColorUpdated -= ConnectedProvider_ColorUpdated;
            }
            connectedProvider1 = null;
        }

        UpdateColor(connectedProvider1, connectedProvider2);
    }

    private void OnConnectionChanged2(object? sender, EventArgs e)
    {
        if (inControl2?.GetContextFromConnectedOutput() is ColorProvider provider)
        {
            connectedProvider2 = provider;
            connectedProvider2.ColorUpdated += ConnectedProvider_ColorUpdated;
        }
        else
        {
            if (connectedProvider2 != null)
            {
                connectedProvider2.ColorUpdated -= ConnectedProvider_ColorUpdated;
            }
            connectedProvider2 = null;
        }

        UpdateColor(connectedProvider1, connectedProvider2);
    }

    private void ConnectedProvider_ColorUpdated(object? sender, EventArgs e)
    {
        UpdateColor(connectedProvider1, connectedProvider2);
    }
}
