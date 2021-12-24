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
        ObservableCollection<CustomerToList> customers;
        public CustomerWindow(IBL bl, ObservableCollection<CustomerToList> customers, int customerId)
        {
            InitializeComponent();
            this.bl = bl;
            this.customers = customers;
            plCustomer = Adapter.CustomerBotoPo(bl.SearchCustomer(customerId));
            DataContext = plCustomer;
            actionsGrid.Visibility = Visibility.Visible;
        }
    }
}
