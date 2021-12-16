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

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private readonly IBL bl;
        private BO.Drone boDrone;
        private Drone plDrone;
        private int droneIndex;
        ObservableCollection<Drone> drones;
        /// <summary>
        /// add ctor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneId"></param>
        public DroneWindow(IBL bl, ObservableCollection<Drone> drones)
        {
            InitializeComponent();
            AddGrid.Visibility = Visibility.Visible;
            this.bl = bl;
            WeightSelectorNew.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationIdSelectorNew.ItemsSource = from station in bl.ListStation()
                                               select station.Id;
        }
        public DroneWindow(IBL bl, ObservableCollection<Drone> drones, int droneId)
        {
            InitializeComponent();
            this.bl = bl;
            boDrone = bl.SearchDrone(droneId);
            this.drones = drones;
            plDrone = drones.Where(d => d.Id == droneId).FirstOrDefault();
            droneIndex = drones.IndexOf(plDrone);
            ActionsGrid.Visibility = Visibility.Visible;

            IdBox.Text = plDrone.Id.ToString();
            ModelBox.Text = plDrone.Model;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            WeightSelector.SelectedItem = plDrone.MaxWeight;
            BatteryBox.Text = plDrone.Battery.ToString();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            StatusSelector.SelectedItem = plDrone.Status;
            LongitudeBox.Text = plDrone.Longitude.ToString();
            LatitudeBox.Text = plDrone.Latitude.ToString();
            IdOfParcelBox.Text = (plDrone.ParcelId != null) ? plDrone.ParcelId.ToString() : "No parcel yet";

            InitializeActionsButton(plDrone);
        }
        private void InitializeActionsButton(Drone drone)
        {
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
            }
            else if (bl.SearchDrone(drone.Id).Parcel.PickedUpAlready)
            {
                Actions.Content = "Deliver parcel";
                Actions.Click += Deliver_Click;
            }
            else
            {
                Actions.Content = "Pick up parcel";
                Actions.Click += Pickup_Click;
            }
        }
        private void IdBoxNew_TextChanged(object sender, TextChangedEventArgs e)
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
        private void ModelBoxNew_TextChanged(object sender, TextChangedEventArgs e)
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
            textBox.Background = Brushes.Red;
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
                    MessageBox.Show("Success");
                    this.Close();
                    return;
                }
                catch { }
            }
            MessageBox.Show("Failure");
        }
        private void ModelBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ModelBoxNew_TextChanged(sender, e);
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
                int.TryParse(IdBox.Text, out int id);
                bl.PickUpAParcel(id);
                MessageBox.Show("Picked up parcel successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Close();
        }
        private void Deliver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(IdBox.Text, out int id);
                bl.DeliverAParcel(id);
                MessageBox.Show("Delivered parcel successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Close();
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
            Close();
        }
        private void ReleaseCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(IdBox.Text, out int id);
                int hours, minutes, seconds;
                TimeSpanWindow timeSpanWindow = new TimeSpanWindow();
                timeSpanWindow.ShowDialog();
                int.TryParse(timeSpanWindow.Hours, out hours);
                int.TryParse(timeSpanWindow.Minutes, out minutes);
                int.TryParse(timeSpanWindow.Seconds, out seconds);
                TimeSpan timeCharging = new TimeSpan(hours, minutes, seconds);
                bl.ReleaseCharging(id);
                //MessageBox.Show(bl.SearchDrone(id).ToString());
                MessageBox.Show("Drone realeased from chraging successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Close();
        }
        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(IdBox.Text, out int id);
                bl.DroneToCharge(id);
                MessageBox.Show("Drone sent to charge successfully");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Close();
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
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Close();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
