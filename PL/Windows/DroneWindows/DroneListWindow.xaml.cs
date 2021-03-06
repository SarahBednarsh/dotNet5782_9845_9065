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
using PO;

namespace PL
{

    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        public List<IGrouping<DroneStatuses, DroneToList>> GroupingData;
        internal static ObservableCollection<DroneToList> Drones;
        public DroneListWindow()
        {
            InitializeComponent();
            Drones = new ObservableCollection<DroneToList>((from drone in bl.ListDrone()
                                                            select Adapter.DroneToListBotoPo(drone)).ToList());
            DataContext = Drones;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

        }
        #region controls
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<DroneToList> drones = DataContext as ObservableCollection<DroneToList>;
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            droneDataGrid.ItemsSource = new ObservableCollection<DroneToList>(from drone in drones where drone.MaxWeight == (WeightCategories)WeightSelector.SelectedItem select drone);
            if (groupingDataGrid.Visibility == Visibility.Visible)//updates the datagrid with updated list
            {
                GroupingData = (droneDataGrid.ItemsSource as ObservableCollection<DroneToList>).GroupBy(x => x.Status).ToList();
                groupingDataGrid.DataContext = GroupingData;
            }
        }
        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow().ShowDialog();
            WeightSelector_SelectionChanged(WeightSelector, null);
        }
        private void ClearWeight_Click(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = -1;
            WeightSelector.Text = "";
            droneDataGrid.ItemsSource = DataContext as ObservableCollection<DroneToList>;
            if (groupingDataGrid.Visibility == Visibility.Visible)
            {
                GroupingData = (DataContext as ObservableCollection<DroneToList>).GroupBy(x => x.Status).ToList();
                groupingDataGrid.DataContext = GroupingData;
            }
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            DroneToList d = cell.DataContext as DroneToList;
            Drone dro = Adapter.DroneBotoPo(bl.SearchDrone(d.Id));
            new DroneWindow(dro).Show();
        }
        #endregion

        #region grouping
        private void DataGridCell_MouseDoubleClick_Grouped(object sender, MouseButtonEventArgs e)//open a drone from a grouped list
        {
            if ((sender as DataGridCell).DataContext is not DroneToList)
                return;
            DataGridCell cell = sender as DataGridCell;
            DroneToList tmp = cell.DataContext as DroneToList;
            Drone dro = Adapter.DroneBotoPo(bl.SearchDrone(tmp.Id));
            new DroneWindow(dro).Show();
            GroupingData = (DataContext as ObservableCollection<DroneToList>).GroupBy(x => x.Status).ToList();
            groupingDataGrid.DataContext = GroupingData;

        }

        private void groupStatus_Click(object sender, RoutedEventArgs e)
        {

            GroupingData = (droneDataGrid.ItemsSource as ObservableCollection<DroneToList>).GroupBy(x => x.Status).ToList();
            groupingDataGrid.DataContext = GroupingData;
            droneDataGrid.Visibility = Visibility.Hidden;
            groupingDataGrid.Visibility = Visibility.Visible;
        }

        private void unGroupStatus_Click(object sender, RoutedEventArgs e)
        {
            groupingDataGrid.Visibility = Visibility.Hidden;
            droneDataGrid.Visibility = Visibility.Visible;
        }
        #endregion
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
