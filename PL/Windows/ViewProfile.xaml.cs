using PO;
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

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for ViewProfile.xaml
    /// </summary>
    public partial class ViewProfile : Window
    {
        User user;

        public ViewProfile(User _user)
        {
            InitializeComponent();
            user = _user;
            DataContext = user;
            profilePhoto.Source = new BitmapImage(new Uri(user.Photo));
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
