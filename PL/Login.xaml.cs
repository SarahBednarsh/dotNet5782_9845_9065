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

namespace PL
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private bool isManager;
        private string xmlFile;
        private bool closeAllowed;
        public Login(string path, bool manager)
        {
            InitializeComponent();
            isManager = manager;
            xmlFile = path;
            if (!isManager)
                title.Text = "כניסת לקוח";
            title.FontSize = 18;
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
                (sender as TextBox).Text = "שם משתמש";
                (sender as TextBox).Opacity = 0.5;
            }
        }
        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordText.Visibility = Visibility.Hidden;
        }
        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as PasswordBox).Password == "")
            {
                passwordText.Visibility = Visibility.Visible;
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
                MessageBox.Show("לא ניתן לסגור חלון באמצעות כפתור זה. אנא השתמש בכפתור ביטול");
            }
        }
    }
}
