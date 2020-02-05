using Denxorz.SnappingCanvas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

namespace Sample
{
    public partial class ColorPrinter : ISnapHost, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string colorName;

        public IReadOnlyCollection<IConnectionOutput> Outputs => Array.Empty<IConnectionOutput>();

        public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl };

        public string ColorName
        {
            get => colorName; private
            set
            {
                colorName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorName)));
            }
        }

        public ColorPrinter()
        {
            DataContext = this;
            InitializeComponent();
            inControl.ConnectionChanged += this.InControl_ConnectionChanged;
            UpdateColor();
        }

        private void InControl_ConnectionChanged(object sender, EventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            ColorName = inControl.GetObjectFromConnectedOutput() is IColorProvider c ? GetColorName(c.Color) : "[not connected]";
        }

        private string GetColorName(SolidColorBrush brush)
        {
            var results = typeof(Colors).GetProperties().Where(p => (Color)p.GetValue(null, null) == brush.Color).Select(p => p.Name);
            return results.Any() ? results.First() : String.Empty;
        }
    }
}
