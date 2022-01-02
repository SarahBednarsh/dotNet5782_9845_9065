using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        public StationWindow()
        {
            InitializeComponent();
            addGrid.Visibility = Visibility.Visible;
            Width = 350;
        }
        public StationWindow(Station station)
        {
            InitializeComponent();
            actionsTitle.Text = string.Format($"Station {station.Id}");
            actionsGrid.Visibility = Visibility.Visible;
            DataContext = station;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(idNewBox.Text, out int id);
                double.TryParse(longitudeNewBox.Text, out double longitude);
                double.TryParse(latitudeNewBox.Text, out double latitude);
                int.TryParse(slotsNewBox.Text, out int chargeSlots);
                bl.AddStation(id, nameNewBox.Text, longitude, latitude, chargeSlots);
                MessageBox.Show("Added station successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void chargingList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListBox list = sender as ListBox;
                DroneInCharge droneInCharge = list.DataContext as DroneInCharge;
                Drone drone = Adapter.DroneBotoPo(bl.SearchDrone(droneInCharge.Id));
                new DroneWindow(drone).Show();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(slotsBox.Text, out int chargeSlots);
            bl.UpdateStationInfo((DataContext as Station).Id, nameBox.Text, chargeSlots);
        }
    }
}
