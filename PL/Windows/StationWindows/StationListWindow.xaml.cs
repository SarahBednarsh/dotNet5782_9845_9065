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
        private readonly IBL bl = BlFactory.GetBL();
        public StationListWindow()
        {
            InitializeComponent();
            DataContext = (from station in bl.ListStation()
                           select Adapter.StationToListBotoPo(station)).ToList();
        }
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new StationWindow().ShowDialog();
            DataContext = (from station in bl.ListStation()
                           select Adapter.StationToListBotoPo(station)).ToList();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            StationToList s = cell.DataContext as StationToList;
            Station sta = Adapter.StationBotoPo(bl.SearchStation(s.Id));
            new StationWindow(sta).ShowDialog();
            DataContext = (from station in bl.ListStation()
                           select Adapter.StationToListBotoPo(station)).ToList();
        }
    }
}
