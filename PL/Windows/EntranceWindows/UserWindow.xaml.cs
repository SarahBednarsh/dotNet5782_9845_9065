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
using BlApi;
using PL.Windows;
using PO;
namespace PL
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly IBL bl = BlFactory.GetBL();
        private User user;
        public UserWindow(User user)
        {
            InitializeComponent();
            title.Content = string.Format(title.Content.ToString(), user.UserName);
            this.user = user;
            DataContext = (from parcel in bl.ListParcelFromCustomer(user.Id)
                           select Adapter.ParcelToListBotoPo(parcel)).ToList();
            viewProfilePhoto.Source = new BitmapImage(new Uri(user.Photo));

        }
        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            ParcelToList p = cell.DataContext as ParcelToList;
            Parcel parcelToOpen = Adapter.ParcelBotoPo(bl.SearchParcel(p.Id));
            new ParcelWindow(parcelToOpen).ShowDialog();
            DataContext = (from parcel in bl.ListParcelFromCustomer(p.Id)
                           select Adapter.ParcelToListBotoPo(parcel)).ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            new ParcelWindow(user.Id).ShowDialog();
            DataContext = (from parcel in bl.ListParcelFromCustomer(user.Id)
                           select Adapter.ParcelToListBotoPo(parcel)).ToList();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Thank you for using .DRONE!\n Are you sure you want to leave?", "Bye", MessageBoxButton.YesNo))
                Close();
        }
        private void viewProfile_Click(object sender, RoutedEventArgs e)
        {
            new ViewProfile(user).Show();
        }
    }
}
