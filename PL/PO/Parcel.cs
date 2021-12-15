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
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(Drone));
        static readonly DependencyProperty SenderIdProperty = DependencyProperty.Register("SenderId", typeof(int), typeof(Drone));
        static readonly DependencyProperty TargetIdProperty = DependencyProperty.Register("TargetId", typeof(int), typeof(Drone));
        static readonly DependencyProperty WeightProperty = DependencyProperty.Register("Weight", typeof(WeightCategories), typeof(Drone));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(Drone));
        static readonly DependencyProperty DroneIdProperty = DependencyProperty.Register("DroneId", typeof(int), typeof(Drone));
        static readonly DependencyProperty CreationProperty = DependencyProperty.Register("Creation", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty AttributionProperty = DependencyProperty.Register("Attribution", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty PickUpProperty = DependencyProperty.Register("PickUp", typeof(DateTime?), typeof(Drone));
        static readonly DependencyProperty DeliveryProperty = DependencyProperty.Register("Delivery", typeof(DroneStatuses), typeof(Drone));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public int SenderId { get => (int)GetValue(SenderIdProperty); set => SetValue(SenderIdProperty, value); }
        public int TargetId { get => (int)GetValue(TargetIdProperty); set => SetValue(TargetIdProperty, value); }
        public WeightCategories Weight { get => (WeightCategories)GetValue(WeightProperty); set => SetValue(WeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
        public int DroneId { get => (int)GetValue(DroneIdProperty); set => SetValue(DroneIdProperty, value); }
        public DateTime? Creation { get => (DateTime?)GetValue(CreationProperty); set => SetValue(CreationProperty, value); }
        public DateTime? Attribution { get => (DateTime?)GetValue(AttributionProperty); set => SetValue(AttributionProperty, value); }
        public DateTime? PickUP { get => (DateTime?)GetValue(PickUpProperty); set => SetValue(PickUpProperty, value); }
        public DateTime? Delivery { get => (DateTime?)GetValue(DeliveryProperty); set => SetValue(DeliveryProperty, value); }


    }
}
