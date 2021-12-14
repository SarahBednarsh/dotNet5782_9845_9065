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
using Microsoft.VisualBasic;
using BlApi;
using BO;
using Dronestatuses = BO.DroneToList;
namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindowOld.xaml
    /// </summary>
    public partial class DroneWindowOld : Window
    {
        private BlApi.IBL bl;
        public DroneWindowOld(BlApi.IBL bl)
        {
            InitializeComponent();
            AddGrid.Visibility = Visibility.Visible;
            this.bl = bl;
            WeightSelectorNew.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationIdSelectorNew.ItemsSource = from station in bl.ListStation()
                                               select station.Id;
        }
        public DroneWindowOld(BlApi.IBL bl, Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException("Drone is null");
            InitializeComponent();
            ActionsGrid.Visibility = Visibility.Visible;
            this.bl = bl;

            IdBox.Text = drone.Id.ToString();
            ModelBox.Text = drone.Model;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            WeightSelector.SelectedItem = drone.MaxWeight;
            BatteryBox.Text = drone.Battery.ToString();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            StatusSelector.SelectedItem = drone.Status;
            LongitudeBox.Text = drone.Longitude;
            LatitudeBox.Text = drone.Latitude;
            IdOfParcelBox.Text = (drone.Parcel != null) ? drone.Parcel.Id.ToString() : "No parcel yet";

            InitializeActionsButton(drone);
        }
        private void InitializeActionsButton(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException("No drone");
            if (drone.Status == BO.DroneStatuses.Available)
            {
                Actions.Content = "Charge";
                Actions.Click += Charge_Click;
                Actions2.Visibility = Visibility.Visible;
                Actions2.Content = "Send to delivery";
                Actions2.Click += SendToDelivery_Click;
            }
            else if (drone.Status == BO.DroneStatuses.InMaintenance)
            {
                Actions.Content = "Release charge";
                Actions.Click += ReleaseCharge_Click;
            }
            else if (drone.Parcel.PickedUpAlready)
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
