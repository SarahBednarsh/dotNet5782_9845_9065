using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace System.Windows.Controls
{
    public class WaterMarkBox : TextBox
    {
        private bool isEmpty;
        public double WaterMarkOpacity { get; set; }
        public double NoWaterMarkOpacity { get; set; }
        public string WaterMarkText { get; set; }

        public WaterMarkBox() : base()
        {
            Text = WaterMarkText;
            MessageBox.Show(Opacity.ToString());
            Opacity = WaterMarkOpacity;
            isEmpty = true;
            GotFocus += GotFocusHandleWaterMark;
            LostFocus += LostFocusHandleWaterMark;
            MessageBox.Show(Opacity.ToString());
        }
        private void GotFocusHandleWaterMark(object sender, RoutedEventArgs e)
        {
            if (!isEmpty)
                return;
            isEmpty = false;
            (sender as TextBox).Text = "";
            (sender as TextBox).Opacity = this.NoWaterMarkOpacity;
        }
        private void LostFocusHandleWaterMark(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = WaterMarkText;
                (sender as TextBox).Opacity = WaterMarkOpacity;
                isEmpty = true;
            }
        }
    }
}
