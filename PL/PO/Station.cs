using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL;
namespace PO
{
    public class Station : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(Station));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Station));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(string), typeof(Station));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(string), typeof(Station));
        static readonly DependencyProperty OpenChargeSlotsProperty = DependencyProperty.Register("OpenChargeSlots", typeof(int), typeof(Station));
        static readonly DependencyProperty ChargingProperty = DependencyProperty.Register("Charging", typeof(List<DroneInCharge>), typeof(Station));


        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public string Longitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string Latitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public int OpenChargeSlots { get => (int)GetValue(OpenChargeSlotsProperty); set => SetValue(OpenChargeSlotsProperty, value); }
        public List<DroneInCharge> Charging { get => (List<DroneInCharge>)GetValue(ChargingProperty); set => SetValue(ChargingProperty, value); }
    }
}
