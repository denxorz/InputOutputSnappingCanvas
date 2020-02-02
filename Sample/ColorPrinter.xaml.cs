using Denxorz;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sample
{
    public partial class ColorPrinter : ISnapHost, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string colorName;

        public IReadOnlyCollection<ISnapOutput> Outputs => Array.Empty<ISnapOutput>();

        public IReadOnlyCollection<ISnapInput> Inputs => new ISnapInput[] { inControl };

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
            inControl.Snapped += this.InControl_Snapped;
            inControl.Attached = this;
        }

        private void InControl_Snapped(object sender, EventArgs e)
        {
            ColorName = inControl.GetLinkedObject() is IColorProvider c ? c.Color.Name : "";
        }
    }
}
