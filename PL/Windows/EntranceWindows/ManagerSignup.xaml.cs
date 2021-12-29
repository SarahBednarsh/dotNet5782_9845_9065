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

namespace PL
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        private bool isManager;
        private string xmlFile;
        private bool userNameHasBeenClicked;
        private bool passwordHasBeenClicked;
        private bool closeAllowed;

        public Signup(string path, bool isManager)
        {
            InitializeComponent();
            this.isManager = isManager;
            xmlFile = path;
            userNameHasBeenClicked = false;
            if (!isManager)
                title.Text = "הרשמת לקוח";
            title.FontSize = 18;
        }

        private void userName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!userNameHasBeenClicked)
            {
                userNameHasBeenClicked = true;
                (sender as TextBox).Text = "";
                (sender as TextBox).Opacity = 1;
            }
        }
        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (userNameHasBeenClicked && (sender as TextBox).Text == "")
            {
                userNameHasBeenClicked = false;
                (sender as TextBox).Text = "שם משתמש";
                (sender as TextBox).Opacity = 0.5;
            }
        }

        private void email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!passwordHasBeenClicked)
            {
                passwordHasBeenClicked = true;
                (sender as TextBox).Text = "";
                (sender as TextBox).FlowDirection = FlowDirection.LeftToRight;
                (sender as TextBox).Language = System.Windows.Markup.XmlLanguage.GetLanguage("en-us");
                (sender as TextBox).Opacity = 1;
            }

        }

        private void email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordHasBeenClicked && (sender as TextBox).Text == "")
            {
                passwordHasBeenClicked = false;
                (sender as TextBox).FlowDirection = FlowDirection.RightToLeft;
                (sender as TextBox).Language = System.Windows.Markup.XmlLanguage.GetLanguage("he-il");
                (sender as TextBox).Text = "כתובת מייל";
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
                MessageBox.Show("לא ניתן לסגור חלון באמצעות כפתור זה. אנא השתמש בכפתור ביטול", "ERROR", MessageBoxButton.OK,
                                  MessageBoxImage.Information);
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
    }
}
