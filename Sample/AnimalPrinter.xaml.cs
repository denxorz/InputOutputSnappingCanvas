using Denxorz.InputOutputSnappingCanvas;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sample
{
    public partial class AnimalPrinter : ISnapHost, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IReadOnlyCollection<IConnectionOutput> Outputs => Array.Empty<IConnectionOutput>();

        public IReadOnlyCollection<IConnectionInput> Inputs => new IConnectionInput[] { inControl };

        public string Name { get; private set; } = "[???]";

        public double Top { get; set; }
        public double Left { get; set; }

        public AnimalPrinter()
        {
            DataContext = this;
            InitializeComponent();

            inControl.ConnectionChanging += OnConnectionChanging;
        }

        private void OnConnectionChanging(object sender, InputConnectionChangingEventArgs e)
        {
            e.IsCancelled = true; // Non of the outputs are providing Animals... so cancel this.
        }
    }
}
