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

namespace lesson5
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
        private void brakes(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                System.Windows.MessageBox.Show("BRAKES");
            }
        }
        private void slow(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                car.Margin = new Thickness(car.Margin.Left - 10, car.Margin.Top, car.Margin.Right, car.Margin.Bottom);
            }
        }

        private void go(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                car.Margin = new Thickness(car.Margin.Left - 50, car.Margin.Top, car.Margin.Right, car.Margin.Bottom);
            }
        }

    }
}
