using System;
using System.ComponentModel;

namespace Denxorz
{
    public partial class InputControl : INotifyPropertyChanged, ISnapInput
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Snapped;

        private ISnapOutput snappedOutput;

        public ISnapOutput SnappedOutput 
        { 
            get { return snappedOutput; } 
            set 
            {
  
                snappedOutput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SnappedOutput)));
                Snapped?.Invoke(this, EventArgs.Empty);
            } 
        }

        public object Attached { get; set; }

        public InputControl()
        {
            InitializeComponent();
        }

        public object GetLinkedObject()
        {
            return SnappedOutput?.Attached;
        }
    }
}
