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
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly IBL bl = BlFactory.GetBL();//changes to public and static cause i needed it in login- should we send a parameter in login so we can open dronelistwindow?

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            bl.ReleaseAllCharging(); // status is random, so it can't start with being in maintenance for next time
        }

        private void managerLogin_Click(object sender, RoutedEventArgs e)
        {
            new Login(true).ShowDialog();
        }

        private void managerSignup_Click(object sender, RoutedEventArgs e)
        {
            new Signup(true).ShowDialog();
        }

        private void customerSignup_Click(object sender, RoutedEventArgs e)
        {
            new Signup(false).ShowDialog();
        }

        private void customerLogin_Click(object sender, RoutedEventArgs e)
        {
            new Login(false).ShowDialog();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DroneListWindow().ShowDialog();
        }
    }
}
