using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class DroneInParcel : DependencyObject
    {

        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(string), typeof(DroneInParcel));
        static readonly DependencyProperty BatteryProperty = DependencyProperty.Register("Battery", typeof(int), typeof(DroneInParcel));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(string), typeof(DroneInParcel));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(string), typeof(DroneInParcel));

        public string Id { get => (string)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public int Battery { get => (int)GetValue(BatteryProperty); set => SetValue(BatteryProperty, value); }
        public string Longitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string Latitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
    }
}
