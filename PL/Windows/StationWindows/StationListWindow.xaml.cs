using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BlApi;
using BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {
        private IBL bl;
        private ObservableCollection<Station> stations;
        public StationListWindow(IBL bl, ObservableCollection<Station> stations)
        {
            InitializeComponent();
            this.bl = bl;
            this.stations = stations;
            DataContext = stations;
        }
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow(bl).ShowDialog();
            //stations = new ObservableCollection<Station>((from station in bl.ListStation()// this does not affect anything= it doesnt change MainWindow.drones!
            //                                          select Adapter.StationBotoPo(bl.SearchStation(station.Id))).ToList());
            //stationDataGrid.ItemsSource = stations;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            Station s = cell.DataContext as Station;
            new StationWindow(bl, s.Id).ShowDialog();
            //int stationIndex = stations.IndexOf(s);
            //stations[stationIndex] = Adapter.StationBotoPo(bl.SearchStation(s.StationId));
        }
    }
}
