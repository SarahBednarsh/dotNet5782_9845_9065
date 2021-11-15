using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {
        public class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public States State { get; set; }
            public ParcelInTransfer Parcel { get; set; }
            public Coordinates Location { get; set; }
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
