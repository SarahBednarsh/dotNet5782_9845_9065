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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        public CustomerWindow(Customer customer)
        {
            InitializeComponent();
            DataContext = customer;
            Width = 800;
            actionsGrid.Visibility = Visibility.Visible;
            actionsTitle.Text = string.Format($"Customer {customer.Id}");
        }
        public CustomerWindow()
        {
            InitializeComponent();
            Width = 350;
            addGrid.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(idBox.Text, out int id);
                bl.UpdateCustomerInfo(id, nameBox.Text, phoneBox.Text);
                MessageBox.Show("Updated successfully");
                Close();
            }
            catch (Exception exception)
            { MessageBox.Show(exception.Message); }
        }
        private void parcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            ParcelAtCustomer parcelAtCustomer = cell.DataContext as ParcelAtCustomer;
            Parcel parcel = Adapter.ParcelBotoPo(bl.SearchParcel(parcelAtCustomer.Id));
            new ParcelWindow(parcel).Show();
            Close();


        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(idNewBox.Text, out int id);
                double.TryParse(longitudeNewBox.Text, out double longitude);
                double.TryParse(latitudeNewBox.Text, out double latitude);
                bl.AddCustomer(id, nameNewBox.Text, phoneNewBox.Text, longitude, latitude);
                MessageBox.Show("Added customer successfully");
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

    }
}
