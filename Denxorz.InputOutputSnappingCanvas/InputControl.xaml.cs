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
                if (snappedOutput != value)
                {
                    var oldOutput = snappedOutput;
                    snappedOutput = value;
                    PropertyChanged?.Invoke(this, new(nameof(ConnectedOutput)));
                    ConnectionChanged?.Invoke(this, new(Context, oldOutput?.Context, snappedOutput?.Context));
                }
            } 
        }

        public object Context { get; set; }

        public InputControl()
        {
            InitializeComponent();
        }

        public object GetContextFromConnectedOutput()
        {
            return ConnectedOutput?.Context;
        }

        public bool AllowsSnapTo(IConnectionOutput output)
        {
            var eventArgs = new InputConnectionChangingEventArgs(Context, snappedOutput?.Context, output?.Context);
            ConnectionChanging?.Invoke(this, eventArgs);
            return !eventArgs.IsCancelled;
        }
    }
}
