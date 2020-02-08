using Denxorz.InputOutputSnappingCanvas;
using System;
using System.Collections.Generic;

namespace Sample
{
    public partial class DarkLight : ISnapHost
    {
        private readonly ColorProvider lightOutputProvider;
        private readonly ColorProvider darkOutputProvider;

        public IReadOnlyCollection<IConnectionOutput> Outputs => new IConnectionOutput[] { lightOutControl, darkOutControl };

        public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl };

        public DarkLight()
        {
            InitializeComponent();

            lightOutputProvider = new ColorProvider();
            darkOutputProvider = new ColorProvider();

            lightOutControl.ObjectToOutput = lightOutputProvider;
            darkOutControl.ObjectToOutput = darkOutputProvider;

            inControl.ConnectionChanged += OnConnectionChanged;
        }

        private void OnConnectionChanged(object sender, EventArgs e)
        {
            if (inControl.GetObjectFromConnectedOutput() is ColorProvider provider)
            {
                var inputColorName = ColorProvider.GetColorName(provider.Color);

                lightOutputProvider.UpdateColor($"Light{inputColorName}");
                darkOutputProvider.UpdateColor($"Dark{inputColorName}");
            }
            else
            {
                lightOutputProvider.RemoveColor();
                darkOutputProvider.RemoveColor();
            }
        }
    }
}
