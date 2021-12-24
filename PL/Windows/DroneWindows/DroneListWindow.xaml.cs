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
            DataContext = this.drones;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
                droneDataGrid.ItemsSource = (from drone in drones where drone.Status == (DroneStatuses)StatusSelector.SelectedItem select drone).ToList();
            else droneDataGrid.ItemsSource = (from drone in drones where drone.Status == (DroneStatuses)StatusSelector.SelectedItem && 
                                              drone.MaxWeight==(WeightCategories)WeightSelector.SelectedItem select drone).ToList();
        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (StatusSelector == null || StatusSelector.SelectedIndex == -1)
                droneDataGrid.ItemsSource = (from drone in drones where drone.MaxWeight == (WeightCategories)WeightSelector.SelectedItem select drone).ToList();
            else droneDataGrid.ItemsSource = (from drone in drones where drone.MaxWeight == (WeightCategories)WeightSelector.SelectedItem && 
                                              drone.Status == (DroneStatuses)StatusSelector.SelectedItem select drone).ToList();
        }
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl, drones).ShowDialog();
            WeightSelector_SelectionChanged(WeightSelector, null);
            StatusSelector_SelectionChanged(StatusSelector, null);
        }
        private void ClearStatus_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = -1;
            StatusSelector.Text = "";
            if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
            {
                droneDataGrid.ItemsSource = drones;
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
                droneDataGrid.ItemsSource = drones;
                return;
            }
            StatusSelector_SelectionChanged(StatusSelector, null);
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            Drone d = cell.DataContext as Drone;
            //int droneIndex = drones.IndexOf(d);
            new DroneWindow(bl, drones, d.Id).ShowDialog();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
