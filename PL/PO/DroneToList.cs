using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL;

namespace PO
{
    public class DroneToList : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(DroneToList));
        static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(string), typeof(DroneToList));
        static readonly DependencyProperty MaxWeightProperty = DependencyProperty.Register("MaxWeight", typeof(WeightCategories), typeof(DroneToList));
        static readonly DependencyProperty BatteryProperty = DependencyProperty.Register("Battery", typeof(int), typeof(DroneToList));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(DroneStatuses), typeof(DroneToList));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(string), typeof(DroneToList));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(string), typeof(DroneToList));
        static readonly DependencyProperty ParcelIdPropery = DependencyProperty.Register("ParcelId", typeof(string), typeof(DroneToList));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Model { get => (string)GetValue(ModelProperty); set => SetValue(ModelProperty, value); }
        public WeightCategories MaxWeight { get => (WeightCategories)GetValue(MaxWeightProperty); set => SetValue(MaxWeightProperty, value); }
        public int Battery { get => (int)GetValue(BatteryProperty); set => SetValue(BatteryProperty, value); }
        public DroneStatuses Status { get => (DroneStatuses)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
        public string Longitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string Latitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public string ParcelId { get => (string)GetValue(ParcelIdPropery); set => SetValue(ParcelIdPropery, value); }


    }
}
