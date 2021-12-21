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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using BlApi;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly IBL bl = BlFactory.GetBL();//changes to public and static cause i needed it in login- should we send a parameter in login so we can open dronelistwindow?

        public static ObservableCollection<Drone> drones;
        public static ObservableCollection<Station> stations;
        public static ObservableCollection<Parcel> parcels;
        public static ObservableCollection<Customer> customers;
        public MainWindow()
        {
            InitializeComponent();
            drones = new ObservableCollection<Drone>((from drone in bl.ListDrone()
                                                      select Adapter.DroneBotoPo(bl.SearchDrone(drone.Id))).ToList());
            stations = new ObservableCollection<Station>((from station in bl.ListStation()
                                                          select Adapter.StationBotoPo(bl.SearchStation(station.Id))).ToList());
            parcels = new ObservableCollection<Parcel>((from parcel in bl.ListParcel()
                                                          select Adapter.ParcelBotoPo(bl.SearchParcel(parcel.Id))).ToList());
            customers = new ObservableCollection<Customer>((from customer in bl.ListCustomer()
                                                        select Adapter.CustomerBotoPo(bl.SearchCustomer(customer.Id))).ToList());
        }


        private void managerLogin_Click(object sender, RoutedEventArgs e)
        {
            new Login(null, true).ShowDialog();
        }

        private void managerSignup_Click(object sender, RoutedEventArgs e)
        {
            new Signup(null, true).ShowDialog();
        }

        private void customerSignup_Click(object sender, RoutedEventArgs e)
        {
            new Signup(null, false).ShowDialog();
        }

        private void customerLogin_Click(object sender, RoutedEventArgs e)
        {
            new Login(null, false).ShowDialog();
        }

        private void checkstation(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow().ShowDialog();
        }
    }
}
