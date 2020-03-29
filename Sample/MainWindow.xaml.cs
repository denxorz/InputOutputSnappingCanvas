using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sample
{
    public partial class MainWindow
    {
        public ObservableCollection<UserControl> Items { get; } = new ObservableCollection<UserControl>()
        {
           new ColorPrinter { Left = 240, Top = 84 },
           new ColorPrinter { Left = 529, Top = 285 },
           new DarkLight { Left = 260, Top = 205 },
           new AnimalPrinter { Left = 621, Top = 74 },
           new ItemWithColor{ Color = Brushes.Blue, Left = 429, Top = 84 },
           new ItemWithColor{ Color = Brushes.Green, Left = 73, Top = 84 },
        };

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
