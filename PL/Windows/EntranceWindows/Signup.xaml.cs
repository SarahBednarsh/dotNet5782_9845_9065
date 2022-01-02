using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
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
using Microsoft.Win32;
using PO;
namespace PL
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        private IBL bl = BlFactory.GetBL();
        private User user;
        private bool isManager;
        private bool userNameHasBeenClicked;
        private bool passwordHasBeenClicked;
        private bool closeAllowed;
        private bool ImageChanged = false;
        OpenFileDialog op;

        public Signup(bool isManager)
        {
            InitializeComponent();
            this.isManager = isManager;
            userNameHasBeenClicked = false;
            if (!isManager)
                title.Text = "Customer Signup";
            user = new User();
            user.IsManager = isManager;
            DataContext = user;
        }

        
        private void close_Click(object sender, RoutedEventArgs e)
        {
            closeAllowed = true;
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!closeAllowed)
            {
                e.Cancel = true;
                MessageBox.Show("Cannot close implicitly. Please click the cancel button", "ERROR", MessageBoxButton.OK,
                                  MessageBoxImage.Information);
            }
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {

           try
            {
                bl.AddUser(user.Id, user.UserName,
                    ImageChanged ? ManagerImage.Source.ToString() : null,
                    user.Email,
                    PasswordBox.Password,
                    isManager) ;
                if (!isManager)
                    bl.AddCustomer(user.Id, user.UserName, phoneBox.Text, double.Parse(longitudeBox.Text), double.Parse(latitudeBox.Text));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            closeAllowed = true;
            Close();
        }

        private void Button_Click_UploadImage(object sender, RoutedEventArgs e)
        {
            op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ImageChanged = true;
                ManagerImage.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
