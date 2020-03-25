using Denxorz.InputOutputSnappingCanvas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Sample
{
    public partial class ColorPrinter : ISnapHost, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IReadOnlyCollection<IConnectionOutput> Outputs => Array.Empty<IConnectionOutput>();

        public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl };

        public string ColorName { get; private set; }

        public SolidColorBrush Color { get; private set; }

        private ColorProvider connectedProvider;

        public ColorPrinter()
        {
            DataContext = this;
            InitializeComponent();

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
            e.IsCancelled = !(e.NewOutput.ObjectToOutput is ColorProvider);
        }

        private void OnConnectionChanged(object sender, EventArgs e)
        {
            if (inControl.GetObjectFromConnectedOutput() is ColorProvider provider)
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
