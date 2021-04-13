using Denxorz.InputOutputSnappingCanvas;
using Denxorz.ObservableCollectionWithAddRange;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sample
{
    public partial class MainWindow
    {
        private InputOutputSnappingCanvas canvas;

        public ObservableCollection<UserControl> Items { get; } = new ObservableCollection<UserControl>()
        {
           new ColorPrinter { DataContext = new ColorPrinterViewModel(240, 84) },
           new ColorPrinter { DataContext = new ColorPrinterViewModel(529, 285) },
           new DarkLight { DataContext = new DarkLightViewModel(260, 205) },
           new AnimalPrinter { Left = 421, Top = 24 },
           new ItemWithColor { DataContext = new ItemWithColorViewModel(429, 120, Brushes.Blue) },
           new ItemWithColor { DataContext = new ItemWithColorViewModel(125, 84, Brushes.Green) },
        };

        public ObservableCollectionWithAddRange<TreeViewItem> Groups { get; } = new ObservableCollectionWithAddRange<TreeViewItem>();
        public ObservableCollectionWithAddRange<string> Groups2 { get; } = new ObservableCollectionWithAddRange<string>();

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            Loaded += (s, e) =>
            {
                canvas = FindVisualChild<InputOutputSnappingCanvas>(this);
                canvas.ForceLinkAll();
                canvas.MouseUp += (s, e) => UpdateGroups();
                UpdateGroups();
            };
        }

        public T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T item)
                {
                    return item;
                }

                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }

        private void UpdateGroups()
        {
            var groups = canvas.GetGroups();
            Groups.ClearAndAddRange(
                groups.Select((g, i) => new TreeViewItem(
                    $"Group {i + 1}",
                    g.Select(m => new TreeViewItem(m.ToString(), Array.Empty<TreeViewItem>())).ToList())));
        }
    }
}
