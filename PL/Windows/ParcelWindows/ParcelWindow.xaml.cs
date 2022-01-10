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
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        public ParcelWindow(int senderId = -1)
        {
            InitializeComponent();
            Width = 350;
            AddGrid.Visibility = Visibility.Visible;
            senderBox.ItemsSource = from customer in bl.ListCustomer()
                                    select customer.Id;
            if (senderId != -1)
            {
                senderBox.SelectedItem = senderId;
                senderBox.IsEnabled = false;
            }
            targetBox.ItemsSource = from customer in bl.ListCustomer()
                                    select customer.Id;
            weightBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityBox.ItemsSource = Enum.GetValues(typeof(Priorities));

        }
        public ParcelWindow(Parcel parcel, bool isManager = true)
        {
            InitializeComponent();
            Width = 600;
            ActionsAndDiplayGrid.Visibility = Visibility.Visible;
            openDrone.IsEnabled = openSender.IsEnabled = openTarget.IsEnabled = isManager;
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
            
            Int32.TryParse((DataContext as Parcel).Drone.Id, out int id);
            bl.DeliverAParcel(id);
            ConfirmAction.IsEnabled = true;
            //InitializeActionsButton(plParcel);
            Close();

        }

        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32.TryParse((DataContext as Parcel).Drone.Id, out int id);
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



        private void message_ActionClick(object sender, RoutedEventArgs e)
        {
            ConfirmAction.IsEnabled = false;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddParcel((int)senderBox.SelectedItem, (int)targetBox.SelectedItem, (BO.WeightCategories)weightBox.SelectedItem, (BO.Priorities)priorityBox.SelectedItem);
                MessageBox.Show("Parcel added successfully");
            }
            catch(Exception excpetion)
            {
                MessageBox.Show(excpetion.Message);
            }
            Close();
        }

        private void openDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(Adapter.DroneBotoPo(bl.SearchDrone(Int32.Parse((DataContext as Parcel).Drone.Id)))).ShowDialog();
        }

        private void openTarget_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(Adapter.CustomerBotoPo(bl.SearchCustomer((DataContext as Parcel).Target.Id))).ShowDialog();

        }

        private void openSender_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(Adapter.CustomerBotoPo(bl.SearchCustomer((DataContext as Parcel).Sender.Id))).ShowDialog();
        }
    }
}
