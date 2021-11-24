using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Target { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DroneInParcel Drone { get; set; }
            public DateTime Creation { get; set; }
            public DateTime Attribution { get; set; }
            public DateTime PickUp { get; set; }
            public DateTime Delivery { get; set; }
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
