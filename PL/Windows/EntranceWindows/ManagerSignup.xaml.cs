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
        private string xmlFile;
        private bool userNameHasBeenClicked;
        private bool passwordHasBeenClicked;
        private bool closeAllowed;
        private bool ImageChanged = false;
        OpenFileDialog op;

        public Signup(string path, bool isManager)
        {
            InitializeComponent();
            this.isManager = isManager;
            xmlFile = path;
            userNameHasBeenClicked = false;
            user = new User();
            DataContext = user;
        }

        //private void userName_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (!userNameHasBeenClicked)
        //    {
        //        userNameHasBeenClicked = true;
        //        (sender as TextBox).Text = "";
        //        (sender as TextBox).Opacity = 1;
        //    }
        //}
        //private void userName_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (userNameHasBeenClicked && (sender as TextBox).Text == "")
        //    {
        //        userNameHasBeenClicked = false;
        //        (sender as TextBox).Text = "שם משתמש";
        //        (sender as TextBox).Opacity = 0.5;
        //    }
        //}

        //private void email_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (!passwordHasBeenClicked)
        //    {
        //        passwordHasBeenClicked = true;
        //        (sender as TextBox).Text = "";
        //        (sender as TextBox).FlowDirection = FlowDirection.LeftToRight;
        //        (sender as TextBox).Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-us");
        //        (sender as TextBox).Opacity = 1;
        //    }

        //}

        //private void email_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (passwordHasBeenClicked && (sender as TextBox).Text == "")
        //    {
        //        passwordHasBeenClicked = false;
        //        (sender as TextBox).FlowDirection = FlowDirection.RightToLeft;
        //        (sender as TextBox).Language = System.Windows.Markup.XmlLanguage.GetLanguage("he-il");
        //        (sender as TextBox).Text = "כתובת מייל";
        //        (sender as TextBox).Opacity = 0.5;
        //    }
        //}
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
                MessageBox.Show("Cannot close implicidly. Please click the cancel button", "ERROR", MessageBoxButton.OK,
                                  MessageBoxImage.Information);
            }
        }
        //private void password_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    passwordText.Visibility = Visibility.Hidden;
        //}
        //private void password_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as PasswordBox).Password == "")
        //    {
        //        passwordText.Visibility = Visibility.Visible;
        //    }
        //}

        private void signup_Click(object sender, RoutedEventArgs e)
        {

           try
            {
                bl.AddUser(user.Id, user.UserName,
                    ImageChanged ? ManagerImage.Source.ToString() : null,
                    user.Email,
                    PasswordBox.Password,
                    true) ;
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
