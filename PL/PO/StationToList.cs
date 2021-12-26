//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;


//namespace PL
//{
//    public class StationToList : DependencyObject
//    {
//        static readonly DependencyProperty IdProperty = DependencyProperty.Register("StationId", typeof(int), typeof(StationToList));
//        static readonly DependencyProperty NameProperty = DependencyProperty.Register("StationName", typeof(string), typeof(StationToList));
//        static readonly DependencyProperty OpenChargeSlotsProperty = DependencyProperty.Register("ToListOpenChargeSlots", typeof(int), typeof(StationToList));
//        static readonly DependencyProperty UsedChargeSlotsProperty = DependencyProperty.Register("ToListUsedChargeSlots", typeof(int), typeof(StationToList));


//        public int StationId { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
//        public string StationName { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
//        public string StationLongitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
//        public string StationLatitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
//        public int OpenChargeSlots { get => (int)GetValue(OpenChargeSlotsProperty); set => SetValue(OpenChargeSlotsProperty, value); }
//        public List<int> Charging { get => (List<int>)GetValue(ChargingProperty); set => SetValue(ChargingProperty, value); }
//    }
//}
