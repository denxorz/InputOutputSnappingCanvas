using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Denxorz.SnappingCanvas;

namespace Sample
{
    public partial class ItemWithColor : ISnapHost, IColorProvider
    {
        public static readonly DependencyProperty ColorProperty = 
            DependencyProperty.Register(nameof(Color), typeof(SolidColorBrush), typeof(ItemWithColor));

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        
        public IReadOnlyCollection<IConnectionOutput> Outputs => new IConnectionOutput[] { outControl };

        public IReadOnlyCollection<IConnectionInput> Inputs => Array.Empty<IConnectionInput>();

        public ItemWithColor()
        {
            InitializeComponent();
            DataContext = this;
            outControl.ObjectToOutput = this;
        }
    }
}
