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
        private ObservableCollection<ParcelToList> parcels;
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            ParcelToList p = cell.DataContext as ParcelToList;
            new ParcelWindow(bl, parcels, p.PTLId).ShowDialog();
        }
        public ParceListWindow(IBL bl, ObservableCollection<ParcelToList> parcels)
        {
            InitializeComponent();
            this.bl = bl;
            this.parcels = parcels;
            DataContext = parcels;
            SenderSelector.ItemsSource = (from parcel in parcels
                                          select parcel.PTLSenderName).ToList();
            TargetSelector.ItemsSource = (from parcel in parcels
                                          select parcel.PTLTargetName).ToList();
        }

        private void SenderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (TargetSelector == null || TargetSelector.SelectedIndex == -1)
                parcelDataGrid.ItemsSource = (from parcel in parcels group parcel by parcel.PTLSenderName).ToList()
                                                .Find(x => x.Key == (string)(sender as ComboBox).SelectedItem);
            else
                parcelDataGrid.ItemsSource = (from parcel in parcels
                                              where parcel.PTLSenderName == (string)SenderSelector.SelectedItem && parcel.PTLTargetName == (string)TargetSelector.SelectedItem
                                              select parcel).ToList();
        }

        private void TargetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == -1)
                return;
            else if (SenderSelector == null || SenderSelector.SelectedIndex == -1)
                parcelDataGrid.ItemsSource = (from parcel in parcels group parcel by parcel.PTLTargetName).ToList()
                                                .Find(x => x.Key == (string)(sender as ComboBox).SelectedItem);
            else
                parcelDataGrid.ItemsSource = (from parcel in parcels
                                              where parcel.PTLTargetName == (string)TargetSelector.SelectedItem && parcel.PTLSenderName == (string)SenderSelector.SelectedItem
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

