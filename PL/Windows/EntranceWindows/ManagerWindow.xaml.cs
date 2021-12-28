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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            SnackbarWelcome.MessageQueue?.Enqueue(
                "Welcome, name here! Glad to have you in our program!",
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
        }

        private void Drones_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow().ShowDialog();
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            new StationListWindow().ShowDialog();
        }

        private void Parcels_Click(object sender, RoutedEventArgs e)
        {
            new ParceListWindow().ShowDialog();
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow().ShowDialog();
        }
    }
}
