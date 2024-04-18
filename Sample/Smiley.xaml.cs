using Denxorz.InputOutputSnappingCanvas;

namespace Sample;

public partial class Smiley : ISnapHost
{
    public IReadOnlyCollection<IConnectionOutput> Outputs => Array.Empty<IConnectionOutput>();
    public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl1, inControl2 };

    public Smiley()
    {
        InitializeComponent();
        DataContextChanged += (s, e) =>
        {
            ((SmileyViewModel)DataContext).SetUiControls(inControl1, inControl2);
            inControl1.Context = (SmileyViewModel)DataContext;
            inControl2.Context = (SmileyViewModel)DataContext;
        };
    }
}
