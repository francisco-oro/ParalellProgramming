using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
        public int CalculateValue()
        {
            Thread.Sleep(5000);
            return 123; 
        }

        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(5000);
            return 123; 
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            int value = await CalculateValueAsync();
            LblResult.Content = value.ToString();

            await Task.Delay(5000);

            using (var wc = new WebClient())
            {
                string data = await
                    wc.DownloadStringTaskAsync("http://google.com/robots.txt");
                LblResult.Content = data.Split('\n')[0].Trim();
            }
        }
    }
}
