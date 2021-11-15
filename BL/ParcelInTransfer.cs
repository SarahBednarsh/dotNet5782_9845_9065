using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace IBL
{
    namespace BO
    {
        public class ParcelInTransfer
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Reciever { get; set; }
            public Coordinates PickUp { get; set; }
            public Coordinates Destination { get; set; }
            public double Distance { get; set; }
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
