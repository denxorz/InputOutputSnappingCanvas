using System;
using System.ComponentModel;

namespace Denxorz
{
    public partial class OutControl : INotifyPropertyChanged, ISnapOutput
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Snapped;

        private ISnapInput snappedInput;

        public ISnapInput SnappedInput
        {
            get { return snappedInput; }
            set
            {
                snappedInput = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SnappedInput)));
                Snapped?.Invoke(this, EventArgs.Empty);
            }
        }

        public object Attached { get; set; }
        
        public OutControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public object GetLinkedObject()
        {
            return SnappedInput?.Attached;
        }
    }
}
