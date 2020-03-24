using System;
using System.ComponentModel;

namespace Denxorz.InputOutputSnappingCanvas
{
    public partial class OutputControl : INotifyPropertyChanged, IConnectionOutput
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<OutputConnectionChangedEventArgs> ConnectionChanged;

        private IConnectionInput snappedInput;

        public IConnectionInput ConnectedInput
        {
            get { return snappedInput; }
            set
            {
                var oldInput = snappedInput;
                snappedInput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectedInput)));
                ConnectionChanged?.Invoke(this, new OutputConnectionChangedEventArgs(this, oldInput, snappedInput));
            }
        }

        public object ObjectToOutput { get; set; }

        public OutputControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
