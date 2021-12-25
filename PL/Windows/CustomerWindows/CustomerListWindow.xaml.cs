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
        private IBL bl;
        private ObservableCollection<CustomerToList> customers;
        public CustomerListWindow(IBL bl, ObservableCollection<CustomerToList> customers)
        {
            InitializeComponent();
            this.bl = bl;
            this.customers = customers;
            DataContext = this.customers;
        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            CustomerToList c = cell.DataContext as CustomerToList;
            //int droneIndex = drones.IndexOf(d);
            new CustomerWindow(bl, c.Id).ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new CustomerWindow(bl).ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
