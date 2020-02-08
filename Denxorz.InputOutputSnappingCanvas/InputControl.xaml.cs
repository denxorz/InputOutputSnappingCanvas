using System;
using System.ComponentModel;

namespace Denxorz.InputOutputSnappingCanvas
{
    public partial class InputControl : INotifyPropertyChanged, IConnectionInput
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ConnectionChanged;

        private IConnectionOutput snappedOutput;

        public IConnectionOutput ConnectedOutput 
        { 
            get { return snappedOutput; } 
            set 
            {
  
                snappedOutput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectedOutput)));
                ConnectionChanged?.Invoke(this, EventArgs.Empty);
            } 
        }

        public InputControl()
        {
            InitializeComponent();
        }

        public object GetObjectFromConnectedOutput()
        {
            return ConnectedOutput?.ObjectToOutput;
        }
    }
}
