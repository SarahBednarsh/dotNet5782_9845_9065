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
            this.bl = bl;
            this.parcels = parcels;
            DataContext = parcels;
            SenderSelector.ItemsSource = (from parcel in parcels
                                          select parcel.ParcelSenderId).ToList();
            TargetSelector.ItemsSource = (from parcel in parcels
                                          select parcel.ParcelTargetId).ToList();
        }

        private void SenderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (TargetSelector == null || TargetSelector.SelectedIndex == -1)
                parcelDataGrid.ItemsSource = (from parcel in parcels group parcel by parcel.ParcelSenderId).ToList()
                                                .Find(x => x.Key == (int)(sender as ComboBox).SelectedItem);
            else
                parcelDataGrid.ItemsSource = (from parcel in parcels
                                              where parcel.ParcelSenderId == (int)SenderSelector.SelectedItem && parcel.ParcelTargetId == (int)TargetSelector.SelectedItem
                                              select parcel).ToList();
        }



        private void TargetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (SenderSelector == null || SenderSelector.SelectedIndex == -1)
                parcelDataGrid.ItemsSource = (from parcel in parcels group parcel by parcel.ParcelTargetId).ToList()
                                                .Find(x => x.Key == (int)(sender as ComboBox).SelectedItem);
            else
                parcelDataGrid.ItemsSource = (from parcel in parcels
                                              where parcel.ParcelTargetId == (int)TargetSelector.SelectedItem && parcel.ParcelSenderId == (int)SenderSelector.SelectedItem
                                              select parcel).ToList();
        }

        private void ClearSenderSelection_Click(object sender, RoutedEventArgs e)
        {
            SenderSelector.SelectedItem = -1;
            SenderSelector.Text = "";
            if (TargetSelector == null || TargetSelector.SelectedIndex == -1)
            {
                parcelDataGrid.ItemsSource = parcels;
                return;
            }
            TargetSelector_SelectionChanged(TargetSelector, null);
        }
        private void ClearTargetSelection_Click(object sender, RoutedEventArgs e)
        {
            TargetSelector.SelectedItem = -1;
            TargetSelector.Text = "";
            if (SenderSelector == null || SenderSelector.SelectedIndex == -1)
            {
                parcelDataGrid.ItemsSource = parcels;
                return;
            }
            SenderSelector_SelectionChanged(SenderSelector, null);
        }
    }
}

