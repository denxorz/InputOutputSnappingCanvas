using Denxorz.InputOutputSnappingCanvas;

namespace Sample;

public partial class ColorPrinter : ISnapHost
{
    public IReadOnlyCollection<IConnectionOutput> Outputs => Array.Empty<IConnectionOutput>();
    public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl };

    public ColorPrinter()
    {
        InitializeComponent();
        DataContextChanged += (s, e) =>
        {
            ((ColorPrinterViewModel)DataContext).SetUiControls(inControl);
            inControl.Context = ((ColorPrinterViewModel)DataContext);
        };
    }
}
