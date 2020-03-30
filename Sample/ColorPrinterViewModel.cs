using Denxorz.InputOutputSnappingCanvas;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace Sample
{
    class ColorPrinterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ColorProvider connectedProvider;

        private IConnectionInput inControl;

        public string ColorName { get; private set; }
        public SolidColorBrush Color { get; private set; }
        public double Top { get; set; }
        public double Left { get; set; }

        public ColorPrinterViewModel() { }

        public ColorPrinterViewModel(double left, double top)
            : this()
        {
            Left = left;
            Top = top;
        }

        public void SetUiControls(IConnectionInput inputControl)
        {
            inControl = inputControl;
            inControl.ConnectionChanged += OnConnectionChanged;
            inControl.ConnectionChanging += OnConnectionChanging;

            UpdateColor(null);
        }

        private void UpdateColor(ColorProvider provider)
        {
            Color = provider?.Color;
            ColorName = Color != null ? ColorProvider.GetColorName(Color) : "[???]";

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorName)));
        }

        private void OnConnectionChanging(object sender, InputConnectionChangingEventArgs e)
        {
            e.IsCancelled = !(e.NewOutput is ColorProvider);
        }

        private void OnConnectionChanged(object sender, EventArgs e)
        {
            if (inControl.GetContextFromConnectedOutput() is ColorProvider provider)
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

        private void ConnectedProvider_ColorUpdated(object sender, EventArgs e)
        {
            UpdateColor(connectedProvider);
        }
    }
}
