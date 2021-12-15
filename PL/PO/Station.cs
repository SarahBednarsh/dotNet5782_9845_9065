using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    class Station : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(Drone));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Drone));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty OpenChargeSlotsProperty = DependencyProperty.Register("OpenChargeSlots", typeof(int), typeof(Drone));
        static readonly DependencyProperty ChargingProperty = DependencyProperty.Register("Charging", typeof(List<int>), typeof(Drone));


        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public string Longitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string Latitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public int OpenChargeSlots { get => (int)GetValue(OpenChargeSlotsProperty); set => SetValue(OpenChargeSlotsProperty, value); }
        public List<int> Charging { get => (List<int>)GetValue(ChargingProperty); set => SetValue(ChargingProperty, value); }
    }
}
