using Denxorz.InputOutputSnappingCanvas;
using System;

namespace Sample
{
    public class DarkLightViewModel
    {
        private IConnectionInput inControl;

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

        public override string ToString()
        {
            return "Darker/lighter modifier";
        }

        private void OnConnectionChanging(object sender, InputConnectionChangingEventArgs e)
        {
            e.IsCancelled = !(e.NewOutput is ColorProvider);
        }

        private void OnConnectionChanged(object sender, EventArgs e)
        {
            if (inControl.GetContextFromConnectedOutput() is ColorProvider provider)
            {
                var inputColorName = ColorProvider.GetColorName(provider.Color);

                LightOutputProvider.UpdateColor($"Light{inputColorName}");
                DarkOutputProvider.UpdateColor($"Dark{inputColorName}");
            }
            else
            {
                LightOutputProvider.RemoveColor();
                DarkOutputProvider.RemoveColor();
            }
        }
    }
}
