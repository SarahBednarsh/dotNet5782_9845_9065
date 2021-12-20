using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ParceListWindow.xaml
    /// </summary>
    public partial class ParceListWindow : Window
    {
        private IBL bl;
        private ObservableCollection<Parcel> parcels;
        public ParceListWindow(IBL bl, ObservableCollection<Parcel> parcels)
        {
            InitializeComponent();
            InitializeComponent();
            this.bl = bl;
            this.parcels = parcels;
            DataContext = parcels;
        }
    }
}
