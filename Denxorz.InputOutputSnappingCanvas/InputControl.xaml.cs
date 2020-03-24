using System;
using System.ComponentModel;

namespace Denxorz.InputOutputSnappingCanvas
{
    public partial class InputControl : INotifyPropertyChanged, IConnectionInput
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<InputConnectionChangedEventArgs> ConnectionChanged;
        public event EventHandler<InputConnectionChangingEventArgs> ConnectionChanging;

        private IConnectionOutput snappedOutput;

        public IConnectionOutput ConnectedOutput 
        { 
            get { return snappedOutput; } 
            set 
            {
                var oldOutput = snappedOutput;
                snappedOutput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectedOutput)));
                ConnectionChanged?.Invoke(this, new InputConnectionChangedEventArgs(this, oldOutput, snappedOutput));
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

        public bool AllowsSnapTo(IConnectionOutput output)
        {
            var eventArgs = new InputConnectionChangingEventArgs(this, snappedOutput, output);
            ConnectionChanging?.Invoke(this, eventArgs);
            return !eventArgs.IsCancelled;
        }
    }
}
