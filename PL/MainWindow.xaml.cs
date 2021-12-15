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
        private readonly IBL bl = BlFactory.GetBL();

        public static ObservableCollection<Drone> drones;
        public MainWindow()
        {
            InitializeComponent();
            drones = new ObservableCollection<Drone>((from drone in bl.ListDrone()
                                                      select Adapter.DroneBotoPo(bl.SearchDrone(drone.Id))).ToList());
            new DroneListWindow(bl, drones).ShowDialog();
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
    }
}
