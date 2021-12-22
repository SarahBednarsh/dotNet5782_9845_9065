using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using BlApi;
using System.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        private readonly IBL bl;
        ObservableCollection<ParcelToList> parcels;
        private ParcelToList plParcel;

        public ParcelWindow(IBL bl, ObservableCollection<ParcelToList> parcels, int parcelId)
        {
            InitializeComponent();
            this.bl = bl;
            this.parcels = parcels;
            plParcel = parcels.Where(p => p.PTLId == parcelId).FirstOrDefault();
            DataContext = plParcel;
            //ActionsGrid.Visibility = Visibility.Visible;

            //IdBox.Text = plDrone.Id.ToString();
            //ModelBox.Text = plDrone.Model;
            //WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            //WeightSelector.SelectedItem = plDrone.MaxWeight;
            //StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            //StatusSelector.SelectedItem = plDrone.Status;
            //LongitudeBox.Text = plDrone.Longitude.ToString();
            //LatitudeBox.Text = plDrone.Latitude.ToString();
            //IdOfParcelBox.Text = (plDrone.DroneParcelId != null) ? plDrone.DroneParcelId.ToString() : "No parcel yet";

            //InitializeActionsButton(plDrone);
        }
       
        public ParcelWindow()
        {
            InitializeComponent();
        }
    }
}
