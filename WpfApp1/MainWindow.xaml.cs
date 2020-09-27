using Lorn.GridTradingStaff.DataAdapters.NetEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StockDataClient client = new StockDataClient();
            var Data = client.GetDailyPricesAsync("600019", new DateTime(2020, 1, 5), new DateTime(2020, 9, 30));
            this.Button1.Content = Data.Result.Count.ToString();
            
        }
    }
}
