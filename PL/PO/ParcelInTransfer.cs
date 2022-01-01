using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL;
namespace PO
{
    public class ParcelInTransfer : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(ParcelInTransfer));
        static readonly DependencyProperty PickedUpAlreadyProperty = DependencyProperty.Register("PickedUpAlready", typeof(bool), typeof(ParcelInTransfer));
        static readonly DependencyProperty WeightProperty = DependencyProperty.Register("Weight", typeof(WeightCategories), typeof(ParcelInTransfer));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(ParcelInTransfer));
        static readonly DependencyProperty SenderProperty = DependencyProperty.Register("Sender", typeof(CustomerInParcel), typeof(ParcelInTransfer));
        static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(CustomerInParcel), typeof(ParcelInTransfer));
        static readonly DependencyProperty PickUpLongitudeProperty = DependencyProperty.Register("PickUpLongitude", typeof(string), typeof(ParcelInTransfer));
        static readonly DependencyProperty PickUpLatitudeProperty = DependencyProperty.Register("PickUpLatitude", typeof(string), typeof(ParcelInTransfer));
        static readonly DependencyProperty DestinationLongitudeProperty = DependencyProperty.Register("DestinationLongitude", typeof(string), typeof(ParcelInTransfer));
        static readonly DependencyProperty DestinationLatitudeProperty = DependencyProperty.Register("DestinationLatitude", typeof(string), typeof(ParcelInTransfer));
        static readonly DependencyProperty DistanceProperty = DependencyProperty.Register("Distance", typeof(double), typeof(ParcelInTransfer));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public bool PickedUpAlready { get => (bool)GetValue(PickedUpAlreadyProperty); set => SetValue(PickedUpAlreadyProperty, value); }
        public WeightCategories Weight { get => (WeightCategories)GetValue(WeightProperty); set => SetValue(WeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
        public CustomerInParcel Sender { get => (CustomerInParcel)GetValue(SenderProperty); set => SetValue(SenderProperty, value); }
        public CustomerInParcel Target { get => (CustomerInParcel)GetValue(TargetProperty); set => SetValue(TargetProperty, value); }
        public string PickUpLongitude { get => (string)GetValue(PickUpLongitudeProperty); set => SetValue(PickUpLongitudeProperty, value); }
        public string PickUpLatitude { get => (string)GetValue(PickUpLatitudeProperty); set => SetValue(PickUpLatitudeProperty, value); }
        public string DestinationLongitude { get => (string)GetValue(DestinationLongitudeProperty); set => SetValue(DestinationLongitudeProperty, value); }
        public string DestinationLatitude { get => (string)GetValue(DestinationLatitudeProperty); set => SetValue(DestinationLatitudeProperty, value); }
        public double Distance { get => (double)GetValue(DistanceProperty); set => SetValue(DistanceProperty, value); }

    }
}
