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
using System.Globalization;
using System.Collections.ObjectModel;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL bl;
        private ObservableCollection<Drone> drones;
        public DroneListWindow(IBL bl, ObservableCollection<Drone> drones)
        {
            InitializeComponent();
            this.bl = bl;
            this.drones = drones;
            DataContext = MainWindow.drones;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
                droneDataGrid.ItemsSource = bl.ListDroneConditional(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem);
            else
                droneDataGrid.ItemsSource = bl.ListDroneConditional(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem && x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);

        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (StatusSelector == null || StatusSelector.SelectedIndex == -1)
                droneDataGrid.ItemsSource = bl.ListDroneConditional(x => x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);
            else
                droneDataGrid.ItemsSource = bl.ListDroneConditional(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem && x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);
        }
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl, drones).ShowDialog();
            //drones = new ObservableCollection<Drone>((from drone in bl.ListDrone()// this does not affect anything= it doesnt change MainWindow.drones!
            //                                          select Adapter.DroneBotoPo(bl.SearchDrone(drone.Id))).ToList());
            //MainWindow.drones = new ObservableCollection<Drone>((from drone in bl.ListDrone()
            //         select Adapter.DroneBotoPo(bl.SearchDrone(drone.Id))).ToList());
            //droneDataGrid.ItemsSource = drones;
            WeightSelector_SelectionChanged(WeightSelector, null);
            StatusSelector_SelectionChanged(StatusSelector, null);
        }
        private void ClearStatus_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = -1;
            StatusSelector.Text = "";
            if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
            {
                droneDataGrid.ItemsSource = bl.ListDrone();
                return;
            }
            WeightSelector_SelectionChanged(WeightSelector, null);
        }
        private void ClearWeight_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = -1;
            WeightSelector.Text = "";
            if (StatusSelector == null || StatusSelector.SelectedIndex == -1)
            {
                droneDataGrid.ItemsSource = bl.ListDrone();
                return;
            }
            StatusSelector_SelectionChanged(StatusSelector, null);
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            Drone d = cell.DataContext as Drone;
            new DroneWindow(bl, drones, d.Id).ShowDialog();
            int droneIndex = drones.IndexOf(d);
            drones[droneIndex] = Adapter.DroneBotoPo(bl.SearchDrone(d.Id));
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
