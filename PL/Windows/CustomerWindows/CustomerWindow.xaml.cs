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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly IBL bl;
        private Customer plCustomer;
        private int index;
        public CustomerWindow(IBL bl, int customerId)
        {
            InitializeComponent();
            this.bl = bl;
            plCustomer = Adapter.CustomerBotoPo(bl.SearchCustomer(customerId));
            index = MainWindow.customers.IndexOf(MainWindow.customers.Where(x=>x.Id==customerId).FirstOrDefault());
            actionsTitle.Text = string.Format($"Customer {plCustomer.CustomerId}");
            DataContext = plCustomer;
            Width = 800;
            actionsGrid.Visibility = Visibility.Visible;
        }
        public CustomerWindow(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            DataContext = plCustomer;
            Width = 350;
            addGrid.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(idBox.Text, out int id);
                bl.UpdateCustomerInfo(id, nameBox.Text, phoneBox.Text);
                MainWindow.customers[index] = Adapter.CustomerToListBotoPo(bl.ListCustomer().Where(x => x.Id == plCustomer.CustomerId).FirstOrDefault());
                //MainWindow.customers = new ObservableCollection<CustomerToList>((from customer in bl.ListCustomer()
                //                                                                 select Adapter.CustomerToListBotoPo(customer)).ToList());
                plCustomer = Adapter.CustomerBotoPo(bl.SearchCustomer(plCustomer.CustomerId));
                MessageBox.Show("Updated successfully");
            }
            catch (Exception exception)
            { MessageBox.Show(exception.Message); }
        }
        private void parcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox list = sender as ListBox;
            new ParcelWindow(bl, MainWindow.parcels, (int)list.SelectedItem).Show();
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
                MainWindow.customers.Add(Adapter.CustomerToListBotoPo(bl.ListCustomer().Where(x => x.Id == id).FirstOrDefault()));
                MessageBox.Show("Added customer successfully");
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
