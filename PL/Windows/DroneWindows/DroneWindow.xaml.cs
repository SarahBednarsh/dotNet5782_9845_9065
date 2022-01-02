using BlApi;
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
using System.ComponentModel;
using System.Collections.ObjectModel;
using PO;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        /// <summary>
        /// add ctor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneId"></param>
        public DroneWindow()
        {
            InitializeComponent();
            AddGrid.Visibility = Visibility.Visible;
            WeightSelectorNew.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationIdSelectorNew.ItemsSource = from station in bl.ListStation()
                                               select station.Id;
        }
        public DroneWindow(Drone drone)
        {
            InitializeComponent();

            ActionsGrid.Visibility = Visibility.Visible;
            DataContext = drone;
            if (drone.Parcel != null)
            {
                parcelInTransfer.Visibility = Visibility.Visible;
                parcelInTransfer.DataContext = drone.Parcel;
            }
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            InitializeActionsButton(drone);
        }
        private void InitializeActionsButton(Drone drone)
        {
            Actions.Click -= Charge_Click;
            Actions.Click -= ReleaseCharge_Click;
            Actions.Click -= Deliver_Click;
            Actions.Click -= Pickup_Click;
            if (drone == null)
                throw new ArgumentNullException("No drone");
            if (drone.Status == DroneStatuses.Available)
            {
                Actions.Content = "Charge";
                Actions.Click += Charge_Click;
                Actions2.Visibility = Visibility.Visible;
                Actions2.Content = "Send to delivery";
                Actions2.Click += SendToDelivery_Click;
            }
            else if (drone.Status == DroneStatuses.InMaintenance)
            {
                Actions.Content = "Release charge";
                Actions.Click += ReleaseCharge_Click;
                Actions2.Visibility = Visibility.Hidden;
            }
            else if (bl.SearchDrone(drone.Id).Parcel.PickedUpAlready)
            {
                Actions.Content = "Deliver parcel";
                Actions.Click += Deliver_Click;
                Actions2.Visibility = Visibility.Hidden;
            }
            else
            {
                Actions.Content = "Pick up parcel";
                Actions.Click += Pickup_Click;
                Actions2.Visibility = Visibility.Hidden;
            }
        }
        private void White_GotFocus(object sender, RoutedEventArgs e)
        {
            MakeTextBoxWhite(sender as TextBox);
        }
        private void IdBoxNew_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox id = sender as TextBox;
            if (!ValidateId(id.Text))
                MakeTextBoxRed(sender as TextBox);
            else
                MakeTextBoxWhite(sender as TextBox);
        }
        private bool ValidateId(string text)
        {
            if (!int.TryParse(text, out int id))
                return false;
            if (id < 0)
                return false;
            try
            {
                bl.SearchDrone(id);
                return false;
            }
            catch
            { return true; }
        }
        private void ModelBoxNew_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox model = sender as TextBox;
            if (!ValidateModel(model.Text))
                MakeTextBoxRed(sender as TextBox);
            else
                MakeTextBoxWhite(sender as TextBox);
        }
        private bool ValidateModel(string text)
        {
            return text != null && text != "";
        }
        private void WeightSelectorNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void StationIdSelectorNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MakeTextBoxRed(TextBox textBox)
        {
            textBox.Background = Brushes.PaleVioletRed;
            textBox.BorderBrush = Brushes.Red;
        }
        private void MakeTextBoxWhite(TextBox textBox)
        {
            textBox.Background = Brushes.White;
            textBox.BorderBrush = Brushes.Blue;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateId(IdBoxNew.Text) && ValidateModel(ModelBoxNew.Text) && WeightSelectorNew.SelectedIndex != -1 && StationIdSelectorNew.SelectedIndex != -1)
            {
                int.TryParse(IdBoxNew.Text, out int id);
                try
                {

                    bl.AddDrone(id, ModelBoxNew.Text, (BO.WeightCategories)WeightSelectorNew.SelectedItem, (int)StationIdSelectorNew.SelectedItem);
                    MessageBox.Show("Added drone successfully");
                    this.Close();
                    return;
                }
                catch { }
            }
            MessageBox.Show("Couldn't add drone");
        }
        private void ModelBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ModelBoxNew_LostFocus(sender, e);
            string model = (sender as TextBox).Text;
            if (Update == null)
                return;
            int.TryParse(IdBox.Text, out int id);
            if (model == bl.SearchDrone(id).Model)
                Update.IsEnabled = false;
            else
                Update.IsEnabled = true;
        }
        private void Pickup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.PickUpAParcel((DataContext as Drone).Id);
                MessageBox.Show("Picked up parcel successfully");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void Deliver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeliverAParcel((DataContext as Drone).Id);
                MessageBox.Show("Delivered parcel successfully");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void SendToDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(IdBox.Text, out int id);
                bl.AttributeAParcel(id);
                MessageBox.Show("Attributed parcel successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void ReleaseCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ReleaseCharging((DataContext as Drone).Id);
                MessageBox.Show("Drone realeased from chraging successfully");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneToCharge((DataContext as Drone).Id);
                MessageBox.Show("Drone sent to charge successfully");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateModel(ModelBox.Text))
            {
                MessageBox.Show("Enter a valid model");
                return;
            }
            try
            {
                int.TryParse(IdBox.Text, out int id);
                bl.UpdateDroneModel(id, ModelBox.Text);
                MessageBox.Show("Updated model successfully");
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ActionsGrid_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }


        private void viewParcel_Click(object sender, RoutedEventArgs e)
        {
            Parcel parcelToOpen = Adapter.ParcelBotoPo(bl.SearchParcel((DataContext as Drone).Parcel.Id));
            new ParcelWindow(parcelToOpen).ShowDialog();
        }
    }

}


