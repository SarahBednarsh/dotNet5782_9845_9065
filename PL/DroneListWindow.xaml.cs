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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private IBL.BO.IBL bl;
        public DroneListWindow(IBL.BO.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            DronesListView.ItemsSource = bl.ListDrone();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DronesListView.ItemsSource = bl.ListDrone();

        }
    }
}
