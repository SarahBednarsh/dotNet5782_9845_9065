using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void b1big(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width *= 2;
                b.Height *= 2;
            }
        }

        private void b1small(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width /= 2;
                b.Height /= 2;
            }
        }
        private void b2big(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width *= 2;
                b.Height *= 2;
            }
        }

        private void b2small(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width /= 2;
                b.Height /= 2;
            }
        }
        private void b3big(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width *= 2;
                b.Height *= 2;
            }
        }

        private void b3small(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width /= 2;
                b.Height /= 2;
            }
        }
        private void b4big(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width *= 2;
                b.Height *= 2;
            }
        }

        private void b4small(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Width /= 2;
                b.Height /= 2;
            }
        }

    }
}
