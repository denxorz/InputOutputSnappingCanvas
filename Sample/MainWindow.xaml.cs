using Denxorz.InputOutputSnappingCanvas;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sample
{
    public partial class MainWindow
    {
        public ObservableCollection<UserControl> Items { get; } = new ObservableCollection<UserControl>()
        {
           new ColorPrinter { DataContext = new ColorPrinterViewModel(240, 84) },
           new ColorPrinter { DataContext = new ColorPrinterViewModel(529, 285) },
           new DarkLight { DataContext = new DarkLightViewModel(260, 205) },
           new AnimalPrinter { Left = 621, Top = 74 },
           new ItemWithColor { DataContext = new ItemWithColorViewModel(429, 84, Brushes.Blue) },
           new ItemWithColor { DataContext = new ItemWithColorViewModel(125, 84, Brushes.Green) },
        };

        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => FindVisualChild<InputOutputSnappingCanvas>(this).ForceLinkAll();
        }

        public TChildItem FindVisualChild<TChildItem>(DependencyObject obj) where TChildItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is TChildItem)
                    return (TChildItem)child;

                var childOfChild = FindVisualChild<TChildItem>(child);

                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }
    }
}
