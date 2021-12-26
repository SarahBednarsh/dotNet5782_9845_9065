using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class DroneInCharge : DependencyObject
    {

        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(DroneInCharge));
        static readonly DependencyProperty BatteryProperty = DependencyProperty.Register("Battery", typeof(int), typeof(DroneInCharge));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public int Battery { get => (int)GetValue(BatteryProperty); set => SetValue(BatteryProperty, value); }
    }
}
