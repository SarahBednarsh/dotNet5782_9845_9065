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
        public IEnumerable<IGrouping<string, ParcelToList>> GroupingData;
        enum GridKind { Normal = 1, Sender , Target }
        GridKind kind;
        public ParceListWindow()
        {
            InitializeComponent();
            IEnumerable<ParcelToList> parcels = (from parcel in bl.ListParcel()
                                          select Adapter.ParcelToListBotoPo(parcel));
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
            if ((sender as DataGridCell).DataContext is not ParcelToList)
            {
                MessageBox.Show("not parceltolist");
                return;
            }
            DataGridCell cell = sender as DataGridCell;
            ParcelToList p = cell.DataContext as ParcelToList;
            Parcel parcelToOpen = Adapter.ParcelBotoPo(bl.SearchParcel(p.Id));
            new ParcelWindow(parcelToOpen).ShowDialog();
            DataContext = (from parcel in bl.ListParcel()
                           select Adapter.ParcelToListBotoPo(parcel));
        }

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
                               select Adapter.ParcelToListBotoPo(parcel));
            DataGridBySender.Visibility = Visibility.Hidden;
            DataGridByTarget.Visibility = Visibility.Hidden;
            parcelDataGrid.Visibility = Visibility.Hidden;

            switch (kind)
            {
                case GridKind.Normal:
                    parcelDataGrid.Visibility = Visibility.Visible;
                    break;
                case GridKind.Sender:
                    DataGridBySender.Visibility = Visibility.Visible;
                    GroupingData = (parcelDataGrid.ItemsSource as IEnumerable<ParcelToList>).GroupBy(x => x.SenderName);
                    DataGridBySender.DataContext = GroupingData;
                    break;
                case GridKind.Target:
                    DataGridByTarget.Visibility = Visibility.Visible;
                    GroupingData = (parcelDataGrid.ItemsSource as IEnumerable<ParcelToList>).GroupBy(x => x.TargetName);
                    DataGridByTarget.DataContext = GroupingData;
                    break;
            }
            
        }

        private void dateRangeSelector_Click(object sender, RoutedEventArgs e)
        {

            parcelDataGrid.ItemsSource = from BO.ParcelToList parcel in bl.ListParcelCreatedInTimeRange(beginDate.DisplayDate, endDate.DisplayDate)
                                         select Adapter.ParcelToListBotoPo(parcel);
            refreshGrid(false);
        }

        private void dateRangeUnSelector_Click(object sender, RoutedEventArgs e)
        {
            parcelDataGrid.ItemsSource = DataContext as IEnumerable<ParcelToList>;
            refreshGrid(false);
        }

    }
}

