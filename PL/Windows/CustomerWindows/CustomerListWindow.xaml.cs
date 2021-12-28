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
using System.Collections.ObjectModel;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        public CustomerListWindow()
        {
            InitializeComponent();
            DataContext = (from customer in bl.ListCustomer()
                           select Adapter.CustomerToListBotoPo(customer)).ToList();
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            CustomerToList c = cell.DataContext as CustomerToList;
            Customer cus = Adapter.CustomerBotoPo(bl.SearchCustomer(c.Id));
            new CustomerWindow(cus).ShowDialog();
            DataContext = (from customer in bl.ListCustomer()
                           select Adapter.CustomerToListBotoPo(customer)).ToList();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow().ShowDialog();
            DataContext = (from customer in bl.ListCustomer()
                           select Adapter.CustomerToListBotoPo(customer)).ToList();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
