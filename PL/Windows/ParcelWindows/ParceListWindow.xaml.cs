using BlApi;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ParceListWindow.xaml
    /// </summary>
    public partial class ParceListWindow : Window
    {
        private IBL bl;
        private ObservableCollection<Parcel> parcels;

        public ParceListWindow(IBL bl, ObservableCollection<Parcel> parcels)
        {
            InitializeComponent();
            this.bl = bl;
            this.parcels = parcels;
            DataContext = parcels;
            SenderSelector.ItemsSource = (from parcel in parcels
                                          select parcel.ParcelSenderId).ToList();
        }

        private void SenderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            //else if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
            parcelDataGrid.ItemsSource = (from parcel in parcels group parcel by parcel.ParcelSenderId).ToList().Find(x => x.Key == (int)(sender as ComboBox).SelectedItem);
            //else
            //  droneDataGrid.ItemsSource = new ObservableCollection<Drone>((from drone in bl.ListDroneConditional(x => x.Status == (BO.DroneStatuses)StatusSelector.SelectedItem && x.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem)
                                                                            // select Adapter.DroneBotoPo(bl.SearchDrone(drone.Id))).ToList());
        }

        private void ClearSenderSelection_Click(object sender, RoutedEventArgs e)
        {
            SenderSelector.SelectedItem = -1;
            SenderSelector.Text = "";
            //if (WeightSelector == null || WeightSelector.SelectedIndex == -1)
            //{
                parcelDataGrid.ItemsSource = parcels;
                return;
            }
           // WeightSelector_SelectionChanged(WeightSelector, null);
        }
    }

