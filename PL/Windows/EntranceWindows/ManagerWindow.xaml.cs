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
using PL.Windows;
using PO;
namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        User manager;
        public ManagerWindow(User _manager)
        {
            InitializeComponent();
            manager = _manager;
            SnackbarWelcome.MessageQueue?.Enqueue(
                $"Welcome, {manager.UserName}! Glad to have you in our program!",
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            if (!(manager.UserName == "admin"))
                viewProfilePhoto.Source = new BitmapImage(new Uri(manager.Photo));
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

        private void viewProfile_Click(object sender, RoutedEventArgs e)
        {
            new ViewProfile(manager).Show();
        }
    }
}
