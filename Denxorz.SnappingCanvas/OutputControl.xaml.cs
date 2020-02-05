using System;
using System.ComponentModel;

namespace Denxorz.SnappingCanvas
{
    public partial class OutputControl : INotifyPropertyChanged, IConnectionOutput
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ConnectionChanged;

        private IConnectionInput snappedInput;

        public IConnectionInput ConnectedInput
        {
            get { return snappedInput; }
            set
            {
                snappedInput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectedInput)));
                ConnectionChanged?.Invoke(this, EventArgs.Empty);
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
