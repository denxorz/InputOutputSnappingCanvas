using Denxorz.InputOutputSnappingCanvas;

namespace Sample;

public partial class DarkLight : ISnapHost
{
    public IReadOnlyCollection<IConnectionOutput> Outputs => new IConnectionOutput[] { lightOutControl, darkOutControl };
    public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl };

    public DarkLight()
    {
        InitializeComponent();
        DataContextChanged += (s, e) =>
        {
            ((DarkLightViewModel)DataContext).SetUiControls(inControl);
            lightOutControl.Context = ((DarkLightViewModel)DataContext).LightOutputProvider;
            darkOutControl.Context = ((DarkLightViewModel)DataContext).DarkOutputProvider;
            inControl.Context = ((DarkLightViewModel)DataContext);
        };
    }
}
