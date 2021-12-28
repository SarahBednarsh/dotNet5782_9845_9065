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
        private readonly IBL bl = BlFactory.GetBL();
        public ParcelWindow(Parcel parcel)
        {
            InitializeComponent();
            DataContext = parcel;
            InitializeActionsButton(parcel);
            ConfirmAction.IsEnabled = IsEnabled;
        }
        private void InitializeActionsButton(Parcel parcel)
        {
            Actions.Click -= Delete_Click;
            Actions.Click -= PickUp_Click;
            Actions.Click -= Deliver_Click;
            if (parcel == null)
                throw new ArgumentNullException("No parcel");
            if (parcel.Attribution == null)
            {
                Actions.Content = "Delete parcel";
                Actions.Click += Delete_Click;
            }
            else if (parcel.PickUp == null)
            {
                Actions.Content = "Pick up parcel";
                Actions.Click += PickUp_Click;
            }
            else if (parcel.Delivery == null)
            {
                Actions.Content = "Deliver parcel";
                Actions.Click += Deliver_Click;
            }
            else
            {
                Actions.Visibility = Visibility.Hidden;
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
            Int32.TryParse((DataContext as Parcel).DroneId, out int id);
            bl.DeliverAParcel(id);
            ConfirmAction.IsEnabled = true;
            //InitializeActionsButton(plParcel);
            Close();

        }

        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse((DataContext as Parcel).DroneId, out int id);
                bl.PickUpAParcel(id);
            }
            catch (BO.NotEnoughBattery ex)
            {
                MessageBox.Show($"PickUp did not work: " + ex.Message);
            }
            catch (BO.KeyDoesNotExist ex)
            {
                MessageBox.Show($"PickUp did not work: " + ex.Message);
            }
            catch (BO.CannotPickUp ex)
            {
                MessageBox.Show($"PickUp did not work: " + ex.Message);
            }
            ConfirmAction.IsEnabled = IsEnabled;
            Close();
            MessageBox.Show("Drone picked up succesfully", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show($"Are you sure you want to delete this parcel?", "DELETE PARCEL", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (mbResult == MessageBoxResult.Yes)
            {
                bl.DeleteParcel((DataContext as Parcel).Id);
                Close();
                MessageBox.Show("Drone deleted succesfully", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            MessageBox.Show("Action was cancelleduccesfully", "CANCEL", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public ParcelWindow()
        {
            InitializeComponent();
        }

        private void message_ActionClick(object sender, RoutedEventArgs e)
        {
            ConfirmAction.IsEnabled = false;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
