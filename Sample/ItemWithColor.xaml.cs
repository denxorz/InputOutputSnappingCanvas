using Denxorz.InputOutputSnappingCanvas;

namespace Sample;

public partial class ItemWithColor : ISnapHost
{
    public IReadOnlyCollection<IConnectionOutput> Outputs => new IConnectionOutput[] { outControl };
    public IReadOnlyCollection<IConnectionInput> Inputs => Array.Empty<IConnectionInput>();

    public ItemWithColor()
    {
        InitializeComponent();
        DataContextChanged += (s, e) =>  outControl.Context = ((ItemWithColorViewModel)DataContext).ColorProvider;
    }
}
