using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Denxorz.InputOutputSnappingCanvas;

namespace Sample
{
    public partial class ItemWithColor : ISnapHost
    {
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                nameof(Color),
                typeof(SolidColorBrush),
                typeof(ItemWithColor),
                new FrameworkPropertyMetadata(Brushes.Aqua, OnColorPropertyChanged));

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public IReadOnlyCollection<IConnectionOutput> Outputs => new IConnectionOutput[] { outControl };

        public IReadOnlyCollection<IConnectionInput> Inputs => Array.Empty<IConnectionInput>();

        private readonly ColorProvider colorProvider;

        public ItemWithColor()
        {
            InitializeComponent();
            DataContext = this;

            colorProvider = new ColorProvider();
            outControl.ObjectToOutput = colorProvider;
        }

        private void UpdateColor()
        {
            colorProvider.UpdateColor(Color);
        }

        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ItemWithColor)d).UpdateColor();
        }
    }
}
