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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        private readonly IBL bl;
        private Station plStation;
        private int index;
        public StationWindow(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            Width = 350;
            DataContext = plStation;
        }
        public StationWindow(IBL bl, int stationId)
        {
            InitializeComponent();
            this.bl = bl;
            plStation = Adapter.StationBotoPo(bl.SearchStation(stationId));
            index = MainWindow.stations.IndexOf(Adapter.StationToListBotoPo(bl.ListStation().Where(x => x.Id == stationId).FirstOrDefault()));
            actionsTitle.Text = string.Format($"Customer {plStation.Id}");
            DataContext = plStation;
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
                MainWindow.stations.Add(Adapter.StationToListBotoPo(bl.ListStation().Where(x => x.Id == id).FirstOrDefault()));
                MessageBox.Show("Added station successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void chargingList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox list = sender as ListBox;
            new DroneWindow(bl, MainWindow.drones, (int)list.SelectedItem).Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(slotsBox.Text, out int chargeSlots);
            bl.UpdateStationInfo(plStation.Id, nameBox.Text, chargeSlots);
        }
    }
}
