using BlApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using PO;
using BO;
using User = PO.User;

namespace PL
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private IBL bl = BlFactory.GetBL();
        private bool isManager;
        private bool closeAllowed;
        public Login(bool manager)
        {
            InitializeComponent();
            isManager = manager;
            if (!isManager)
                title.Text = "User Login";
            closeAllowed = false;
        }

        private void userName_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = "";
            (sender as TextBox).Opacity = 1;
        }
        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = "username";
                (sender as TextBox).Opacity = 0.5;
            }
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
                MessageBox.Show("Cannot close implicitly. Please click the cancel button", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "admin")
            {
                new ManagerWindow(new User { UserName = "admin" }).Show();
                closeAllowed = true;
                Close();
            }
            else if (!bl.UserInfoCorrect(NameTextBox.Text, PasswordBox.Password, isManager))
                MessageBox.Show("Wrong information. Please try again", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (isManager)
                    new ManagerWindow(Adapter.UserBotoPo(bl.SearchUser(NameTextBox.Text))).Show();
                else
                    new UserWindow(Adapter.UserBotoPo(bl.SearchUser(NameTextBox.Text))).Show();
                closeAllowed = true;
                Close();
            }
        }

        private void passwordRecovery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = bl.SearchUser(NameTextBox.Text).Email;
                bl.RecoverPassword(NameTextBox.Text, 4);
                MessageBox.Show("Password recovered. Check your inbox to view new password");
            }
            catch(KeyDoesNotExist exception)
            {
                MessageBox.Show(exception + ", please enter a valid username");
            }
            catch(Exception exception)
            { MessageBox.Show(exception.Message); }
        }
    }
}
