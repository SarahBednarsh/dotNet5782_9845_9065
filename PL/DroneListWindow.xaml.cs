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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL.BO.IBL bl;
        public DroneListWindow(IBL.BO.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            DronesListView.ItemsSource = bl.ListDrone();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bl.ListDroneConditional(x => x.Status == (DroneStatuses)StatusSelector.SelectedItem);

        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bl.ListDroneConditional(x => x.MaxWeight == (WeightCategories)WeightSelector.SelectedItem);

        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();
            DronesListView.ItemsSource = bl.ListDrone();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((DroneToList)(sender as ListView).SelectedItem).Id;
            new DroneWindow(bl, id).Show();
            DronesListView.ItemsSource = bl.ListDrone();
        }
    }
}
