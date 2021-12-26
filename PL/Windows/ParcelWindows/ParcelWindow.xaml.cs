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
using System;
namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        private readonly IBL bl;
        ObservableCollection<ParcelToList> parcels;
        private Parcel plParcel;
        int index;

        public ParcelWindow(IBL bl, ObservableCollection<ParcelToList> parcels, int parcelId)
        {
            InitializeComponent();
            this.bl = bl;
            this.parcels = parcels;
            plParcel = Adapter.ParcelBotoPo(bl.SearchParcel(parcelId));
            DataContext = plParcel;
            index = parcels.IndexOf(parcels.Where(x => x.Id == plParcel.Id).FirstOrDefault());
                //ActionsGrid.Visibility = Visibility.Visible;
            InitializeActionsButton(plParcel);


            //InitializeActionsButton(plDrone);
        }
        private void InitializeActionsButton(Parcel parcel)
        {
            Actions.Click -= Delete_Click;
            Actions.Click -= PickUp_Click;
            Actions.Click -= Deliver_Click;
            if (parcel==null)
                throw new ArgumentNullException("No parcel");
            if(parcel.Attribution==null)
            {
                Actions.Content = "Delete parcel";
                Actions.Click += Delete_Click;
            }            
            else if(parcel.PickUp==null)
            {
                Actions.Content = "Pick up parcel";
                Actions.Click += PickUp_Click;
            }            
            else if(parcel.Delivery==null)
            {
                Actions.Content = "Deliver parcel";
                Actions.Click += Deliver_Click;
            }
        }

        private void Deliver_Click(object sender, RoutedEventArgs e)
        {
            //do we need try and catch here? cause if it entered here there should always be a drone
            //int droneId;
            //try
            //{
            //    droneId = Int32.Parse(plParcel.ParcelDroneId);
            //}
            //catch 
            bl.DeliverAParcel(Int32.Parse(plParcel.DroneId));
            parcels[index] = Adapter.ParcelToListBotoPo(bl.ListParcel().Where(x => x.Id == plParcel.Id).FirstOrDefault());
        }

        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //do we need try and catch here? cause if it entered here there should always be a drone
                bl.PickUpAParcel(Int32.Parse(plParcel.DroneId));
                parcels[index] = Adapter.ParcelToListBotoPo(bl.ListParcel().Where(x => x.Id == plParcel.Id).FirstOrDefault());
            }
            catch(BO.NotEnoughBattery ex)
            {
                MessageBox.Show($"PickUp did not work: " + ex.ToString());
            }
            catch (BO.KeyDoesNotExist ex)
            {
                MessageBox.Show($"PickUp did not work: " + ex.ToString());
            }
            catch (BO.CannotPickUp ex)
            {
                MessageBox.Show($"PickUp did not work: " + ex.ToString());
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show($"Are you sure you want to delete this parcel?", "DELETE PARCEL", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (mbResult == MessageBoxResult.Yes)
            {
                parcels.Remove(parcels[index]);
                bl.DeleteParcel(plParcel.Id);
                Close();
            }
        }

        //private void InitializeActionsButton(Drone drone)
        //{
        //    Actions.Click -= Charge_Click;
        //    Actions.Click -= ReleaseCharge_Click;
        //    Actions.Click -= Deliver_Click;
        //    Actions.Click -= Pickup_Click;
        //    if (drone == null)
        //        throw new ArgumentNullException("No drone");
        //    if (drone.Status == DroneStatuses.Available)
        //    {
        //        Actions.Content = "Charge";
        //        Actions.Click += Charge_Click;
        //        Actions2.Visibility = Visibility.Visible;
        //        Actions2.Content = "Send to delivery";
        //        Actions2.Click += SendToDelivery_Click;
        //    }
        //    else if (drone.Status == DroneStatuses.InMaintenance)
        //    {
        //        Actions.Content = "Release charge";
        //        Actions.Click += ReleaseCharge_Click;
        //        Actions2.Visibility = Visibility.Hidden;
        //    }
        //    else if (bl.SearchDrone(drone.Id).Parcel.PickedUpAlready)
        //    {
        //        Actions.Content = "Deliver parcel";
        //        Actions.Click += Deliver_Click;
        //        Actions2.Visibility = Visibility.Hidden;
        //    }
        //    else
        //    {
        //        Actions.Content = "Pick up parcel";
        //        Actions.Click += Pickup_Click;
        //        Actions2.Visibility = Visibility.Hidden;
        //    }
        //}
        public ParcelWindow()
        {
            InitializeComponent();
        }
    }
}
