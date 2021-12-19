using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class Station : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("StationId", typeof(int), typeof(Drone));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("StationName", typeof(string), typeof(Drone));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("StationLongitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("StationLatitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty OpenChargeSlotsProperty = DependencyProperty.Register("OpenChargeSlots", typeof(int), typeof(Drone));
        static readonly DependencyProperty ChargingProperty = DependencyProperty.Register("Charging", typeof(List<int>), typeof(Drone));


        public int StationId { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string StationName { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public string StationLongitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string StationLatitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public int OpenChargeSlots { get => (int)GetValue(OpenChargeSlotsProperty); set => SetValue(OpenChargeSlotsProperty, value); }
        public List<int> Charging { get => (List<int>)GetValue(ChargingProperty); set => SetValue(ChargingProperty, value); }
    }
}
