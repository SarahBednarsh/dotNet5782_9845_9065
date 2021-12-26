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
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(Parcel));
        static readonly DependencyProperty SenderProperty = DependencyProperty.Register("Sender", typeof(CustomerInParcel), typeof(Parcel));
        static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(CustomerInParcel), typeof(Parcel));
        static readonly DependencyProperty WeightProperty = DependencyProperty.Register("Weight", typeof(WeightCategories), typeof(Parcel));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(Parcel));
        static readonly DependencyProperty DroneIdProperty = DependencyProperty.Register("DroneId", typeof(string), typeof(Parcel));
        static readonly DependencyProperty CreationProperty = DependencyProperty.Register("Creation", typeof(DateTime?), typeof(Parcel));
        static readonly DependencyProperty AttributionProperty = DependencyProperty.Register("Attribution", typeof(DateTime?), typeof(Parcel));
        static readonly DependencyProperty PickUpProperty = DependencyProperty.Register("PickUp", typeof(DateTime?), typeof(Parcel));
        static readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(DateTime?), typeof(Parcel));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public CustomerInParcel Sender { get => (CustomerInParcel)GetValue(SenderProperty); set => SetValue(SenderProperty, value); }
        public CustomerInParcel Target { get => (CustomerInParcel)GetValue(TargetProperty); set => SetValue(TargetProperty, value); }
        public WeightCategories Weight { get => (WeightCategories)GetValue(WeightProperty); set => SetValue(WeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
        //needs to be drone in parcel
        public string DroneId { get => (string)GetValue(DroneIdProperty); set => SetValue(DroneIdProperty, value); }
        public DateTime? Creation { get => (DateTime?)GetValue(CreationProperty); set => SetValue(CreationProperty, value); }
        public DateTime? Attribution { get => (DateTime?)GetValue(AttributionProperty); set => SetValue(AttributionProperty, value); }
        public DateTime? PickUp { get => (DateTime?)GetValue(PickUpProperty); set => SetValue(PickUpProperty, value); }
        public DateTime? Delivery { get => (DateTime?)GetValue(DeliveryProperty); set => SetValue(DeliveryProperty, value); }


    }
}
