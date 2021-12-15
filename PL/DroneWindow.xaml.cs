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
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private readonly IBL bl;
        private BO.Drone boDrone;
        private Drone plDrone;
        private int droneIndex;
        ObservableCollection<Drone> drones;
        /// <summary>
        /// add ctor
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneId"></param>
        public DroneWindow(IBL bl, ObservableCollection<Drone> drones)
        {
            InitializeComponent();
            this.bl = bl;
        }
        public DroneWindow(IBL bl, ObservableCollection<Drone> drones, int droneId)
        {
            InitializeComponent();
            this.bl = bl;
            boDrone = bl.SearchDrone(droneId);
            this.drones = drones;
            plDrone = drones.Where(d => d.Id == droneId).FirstOrDefault();
            droneIndex = drones.IndexOf(plDrone);
        }
    }
}
