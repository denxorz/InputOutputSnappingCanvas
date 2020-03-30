using System.Collections.ObjectModel;
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
           new ItemWithColor { DataContext = new ItemWithColorViewModel(73, 84, Brushes.Green) },
        };

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
