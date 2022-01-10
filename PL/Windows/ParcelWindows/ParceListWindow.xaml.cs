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
using System.ComponentModel;
using System.Globalization;
using PO;
namespace PL
{
    //public class Parcels : ObservableCollection<ParcelToList>
    //{

    //    // Creating the Tasks collection in this way enables data binding from XAML.
    //}
    /// <summary>
    /// Interaction logic for ParceListWindow.xaml
    /// </summary>
    public partial class ParceListWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        public List<IGrouping<string, ParcelToList>> GroupingData;
        enum GridKind { Normal = 1, Sender , Target }
        GridKind kind;
        public ParceListWindow()
        {
            InitializeComponent();
            List<ParcelToList> parcels = (from parcel in bl.ListParcel()
                                          select Adapter.ParcelToListBotoPo(parcel)).ToList();
            DataContext = parcels;
            //SenderSelector.ItemsSource = (from parcel in parcels
            //                              select parcel.SenderName).ToList();
            //TargetSelector.ItemsSource = (from parcel in parcels
            //                              select parcel.TargetName).ToList();
            kind = GridKind.Normal;
            refreshGrid(true);

        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            ParcelToList p = cell.DataContext as ParcelToList;
            Parcel parcelToOpen = Adapter.ParcelBotoPo(bl.SearchParcel(p.Id));
            new ParcelWindow(parcelToOpen).ShowDialog();
            DataContext = (from parcel in bl.ListParcel()
                           select Adapter.ParcelToListBotoPo(parcel)).ToList();
        }


        //private void SenderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    List<ParcelToList> parcels = DataContext as List<ParcelToList>;
        //    if ((sender as ComboBox).SelectedIndex == -1)
        //        return;
        //    else if (TargetSelector == null || TargetSelector.SelectedIndex == -1)
        //    {
        //        //parcelDataGrid.ItemsSource = (from parcel in parcels group parcel by parcel.SenderName).ToList()
        //        //       .Find(x => x.Key == (string)(sender as ComboBox).SelectedItem);
        //        var tmp = from item in parcels
        //                  group item by item.SenderName
        //                 into g
        //                  orderby g.Key
        //                  select g;
        //        foreach (var item in tmp)
        //            if (item.Key == (string)SenderSelector.SelectedItem)
        //            {
        //                parcelDataGrid.ItemsSource = item.ToList();
        //                break;
        //            }
        //    }
        //    else
        //        parcelDataGrid.ItemsSource = (from parcel in parcels
        //                                      where parcel.SenderName == (string)SenderSelector.SelectedItem && parcel.TargetName == (string)TargetSelector.SelectedItem
        //                                      select parcel).ToList();
        //}

        //private void TargetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    List<ParcelToList> parcels = DataContext as List<ParcelToList>;
        //    DataContext = parcels;
        //    if ((sender as ComboBox).SelectedIndex == -1)
        //        return;
        //    else if (SenderSelector == null || SenderSelector.SelectedIndex == -1)
        //        parcelDataGrid.ItemsSource = (from parcel in parcels group parcel by parcel.TargetName).ToList()
        //                                        .Find(x => x.Key == (string)(sender as ComboBox).SelectedItem);
        //    else
        //        parcelDataGrid.ItemsSource = (from parcel in parcels
        //                                      where parcel.TargetName == (string)TargetSelector.SelectedItem && parcel.SenderName == (string)SenderSelector.SelectedItem
        //                                      select parcel).ToList();
        //}

        //private void ClearSenderSelection_Click(object sender, RoutedEventArgs e)
        //{
        //    SenderSelector.SelectedItem = -1;
        //    SenderSelector.Text = "";
        //    if (TargetSelector == null || TargetSelector.SelectedIndex == -1)
        //    {
        //        parcelDataGrid.ItemsSource = DataContext as List<ParcelToList>;
        //        return;
        //    }
        //    TargetSelector_SelectionChanged(TargetSelector, null);
        //}
        //private void ClearTargetSelection_Click(object sender, RoutedEventArgs e)
        //{
        //    TargetSelector.SelectedItem = -1;
        //    TargetSelector.Text = "";
        //    if (SenderSelector == null || SenderSelector.SelectedIndex == -1)
        //    {
        //        parcelDataGrid.ItemsSource = DataContext as List<ParcelToList>;
        //        return;
        //    }
        //    SenderSelector_SelectionChanged(SenderSelector, null);
        //}

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addParcel_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow().ShowDialog();
            refreshGrid(true);
            //SenderSelector.ItemsSource = (from parcel in DataContext as List<ParcelToList>
            //                              select parcel.SenderName).ToList();
            //TargetSelector.ItemsSource = (from parcel in DataContext as List<ParcelToList>
            //                              select parcel.TargetName).ToList();

        }

        private void groupSender_Click(object sender, RoutedEventArgs e)
        {
            kind = GridKind.Sender;
            refreshGrid(false);
        }

        private void unGroupSender_Click(object sender, RoutedEventArgs e)
        {
            kind = GridKind.Normal;
            refreshGrid(false);
        }

        private void groupTarget_Click(object sender, RoutedEventArgs e)
        {
            kind = GridKind.Target;
            refreshGrid(false);
        }

        private void unGroupTarget_Click(object sender, RoutedEventArgs e)
        {
            kind = GridKind.Normal;
            refreshGrid(false);
        }
        private void refreshGrid(bool needToRenewList)
        {
            if(needToRenewList)
                DataContext = (from parcel in bl.ListParcel()
                               select Adapter.ParcelToListBotoPo(parcel)).ToList();
            DataGridBySender.Visibility = Visibility.Hidden;
            DataGridByTarget.Visibility = Visibility.Hidden;
            parcelDataGrid.Visibility = Visibility.Hidden;

            switch (kind)
            {

                case GridKind.Normal:
                    parcelDataGrid.Visibility = Visibility.Visible;
                    //thik about the date sort
                    ;
                    break;
                case GridKind.Sender:
                    DataGridBySender.Visibility = Visibility.Visible;
                    GroupingData = (parcelDataGrid.ItemsSource as List<ParcelToList>).GroupBy(x => x.SenderName).ToList();
                    DataGridBySender.DataContext = GroupingData;
                    break;
                case GridKind.Target:
                    DataGridByTarget.Visibility = Visibility.Visible;
                    GroupingData = (parcelDataGrid.ItemsSource as List<ParcelToList>).GroupBy(x => x.TargetName).ToList();
                    DataGridByTarget.DataContext = GroupingData;
                    break;

            }
            
        }

        private void dateRangeSelector_Click(object sender, RoutedEventArgs e)
        {
           // parcelDataGrid.ItemsSource=(DataContext as List<ParcelToList>).GroupBy(x => x.)
        }
    }
}

