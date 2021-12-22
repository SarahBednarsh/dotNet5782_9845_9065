using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace PL
{
    public class Parcel : DependencyObject
    {
        static readonly DependencyProperty ParcelIdProperty = DependencyProperty.Register("ParcelId", typeof(int), typeof(Drone));
        static readonly DependencyProperty ParcelSenderProperty = DependencyProperty.Register("ParcelSender", typeof(CustomerInParcel), typeof(Drone));
        static readonly DependencyProperty ParcelTargetProperty = DependencyProperty.Register("ParcelTarget", typeof(CustomerInParcel), typeof(Drone));
        static readonly DependencyProperty ParcelWeightProperty = DependencyProperty.Register("ParcelWeight", typeof(WeightCategories), typeof(Drone));
        static readonly DependencyProperty ParcelPriorityProperty = DependencyProperty.Register("ParcelPriority", typeof(Priorities), typeof(Drone));
        static readonly DependencyProperty ParcelDroneIdProperty = DependencyProperty.Register("ParcelDroneId", typeof(string), typeof(Drone));
        static readonly DependencyProperty CreationProperty = DependencyProperty.Register("Creation", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty AttributionProperty = DependencyProperty.Register("Attribution", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty PickUpProperty = DependencyProperty.Register("PickUp", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(DateTime?), typeof(Drone));

        public int ParcelId { get => (int)GetValue(ParcelIdProperty); set => SetValue(ParcelIdProperty, value); }
        public CustomerInParcel ParcelSender { get => (CustomerInParcel)GetValue(ParcelSenderProperty); set => SetValue(ParcelSenderProperty, value); }
        public CustomerInParcel ParcelTarget { get => (CustomerInParcel)GetValue(ParcelTargetProperty); set => SetValue(ParcelTargetProperty, value); }
        public WeightCategories ParcelWeight { get => (WeightCategories)GetValue(ParcelWeightProperty); set => SetValue(ParcelWeightProperty, value); }
        public Priorities ParcelPriority { get => (Priorities)GetValue(ParcelPriorityProperty); set => SetValue(ParcelPriorityProperty, value); }
        //needs to be drone in parcel
        public string ParcelDroneId { get => (string)GetValue(ParcelDroneIdProperty); set => SetValue(ParcelDroneIdProperty, value); }
        public DateTime? Creation { get => (DateTime?)GetValue(CreationProperty); set => SetValue(CreationProperty, value); }
        public DateTime? Attribution { get => (DateTime?)GetValue(AttributionProperty); set => SetValue(AttributionProperty, value); }
        public DateTime? PickUp { get => (DateTime?)GetValue(PickUpProperty); set => SetValue(PickUpProperty, value); }
        public DateTime? Delivery { get => (DateTime?)GetValue(DeliveryProperty); set => SetValue(DeliveryProperty, value); }


    }
}
