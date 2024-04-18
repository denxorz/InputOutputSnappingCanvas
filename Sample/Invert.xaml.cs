using Denxorz.InputOutputSnappingCanvas;

namespace Sample;

public partial class Invert : ISnapHost
{
    public IReadOnlyCollection<IConnectionOutput> Outputs => new IConnectionOutput[] { outControl };
    public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl };

    public Invert()
    {
        InitializeComponent();
        DataContextChanged += (s, e) =>
        {
            ((InvertViewModel)DataContext).SetUiControls(inControl);
            outControl.Context = ((InvertViewModel)DataContext).OutputProvider;
            inControl.Context = ((InvertViewModel)DataContext);
        };
    }
}
