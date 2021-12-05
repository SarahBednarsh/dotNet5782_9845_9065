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

namespace PL
{
    /// <summary>
    /// Interaction logic for TimeSpanWindow.xaml
    /// </summary>
    public partial class TimeSpanWindow : Window
    {
        public string Hours { get { return HoursBox.Text; } }
        public string Minutes { get { return MinutesBox.Text; } }
        public string Seconds { get { return SecondsBox.Text; } }
        public TimeSpanWindow()
        {
            InitializeComponent();
        }

        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (!Validate(box.Text))
            {
                if (OKButton != null)
                    OKButton.IsEnabled = false;
                MakeTextBoxRed(box);
            }
            else
            {
                if (OKButton != null)
                    OKButton.IsEnabled = true;
                MakeTextBoxWhite(box);
            }
        }
        private bool Validate(string text)
        {
            if (!int.TryParse(text, out int num))
                return false;
            return num >= 0;
        }
        private void MakeTextBoxRed(TextBox textBox)
        {
            textBox.Background = Brushes.Red;
            textBox.BorderBrush = Brushes.Red;
        }
        private void MakeTextBoxWhite(TextBox textBox)
        {
            textBox.Background = Brushes.White;
            textBox.BorderBrush = Brushes.Blue;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate(Hours) && Validate(Minutes) && Validate(Seconds))
            {
                MessageBox.Show("great, thanks");
                Close();
                return;
            }
            MessageBox.Show("Nope. try again");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
