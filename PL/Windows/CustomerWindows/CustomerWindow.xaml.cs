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
            index = MainWindow.customers.IndexOf(MainWindow.customers.Where(x=>x.CustomerToListId==customerId).FirstOrDefault());
            DataContext = plCustomer;
            actionsGrid.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.TryParse(idBox.Text, out int id);
                bl.UpdateCustomerInfo(id, nameBox.Text, phoneBox.Text);
                MainWindow.customers[index] = Adapter.CustomerToListBotoPo(bl.ListCustomer().Where(x => x.Id == plCustomer.CustomerId).FirstOrDefault());
                plCustomer = Adapter.CustomerBotoPo(bl.SearchCustomer(plCustomer.CustomerId));
                MessageBox.Show("Updated successfully");
            }
            catch(Exception exception)
            { MessageBox.Show(exception.Message); }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void parcelsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox list = sender as ListBox;
            new ParcelWindow(bl, MainWindow.parcels, (int)list.SelectedItem).Show();
        }
    }
}
