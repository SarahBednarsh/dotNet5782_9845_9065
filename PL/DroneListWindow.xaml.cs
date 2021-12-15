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
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL bl;
        private ObservableCollection<Drone> drones;
        public DroneListWindow(IBL bl, ObservableCollection<Drone> drones)
        {
            InitializeComponent();
            this.bl = bl;
            this.drones = drones;
            DataContext = MainWindow.drones;
        }
    }
}
