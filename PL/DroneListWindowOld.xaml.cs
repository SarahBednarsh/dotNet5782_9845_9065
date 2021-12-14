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
using BO;
using BlApi;
using DroneStatuses = BO.DroneStatuses;
using WeightCategories = BO.WeightCategories;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindowOld : Window
    {
        private IBL bl;
        public DroneListWindowOld(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            DronesListView.ItemsSource = bl.ListDrone();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
                DronesListView.ItemsSource = bl.ListDroneConditional(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem);
            else
                DronesListView.ItemsSource = bl.ListDroneConditional(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem && x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);

        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (StatusSelector == null || StatusSelector.SelectedIndex == -1)
                DronesListView.ItemsSource = bl.ListDroneConditional(x => x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);
            else
                DronesListView.ItemsSource = bl.ListDroneConditional(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem && x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem);
        }

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).ShowDialog();
            DronesListView.ItemsSource = bl.ListDrone();
            WeightSelector_SelectionChanged(WeightSelector, null);
            StatusSelector_SelectionChanged(StatusSelector, null);
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((DroneToList)(sender as ListView).SelectedItem).Id;
            new DroneWindow(bl, bl.SearchDrone(id)).ShowDialog();
            DronesListView.ItemsSource = bl.ListDrone();
            WeightSelector_SelectionChanged(WeightSelector, null);
            StatusSelector_SelectionChanged(StatusSelector, null);
        }

        private void ClearStatus_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = -1;
            StatusSelector.Text = "";
            if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
            {
                DronesListView.ItemsSource = bl.ListDrone();
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
                DronesListView.ItemsSource = bl.ListDrone();
                return;
            }
            StatusSelector_SelectionChanged(StatusSelector, null);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
