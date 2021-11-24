using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace IBL
{
    namespace BO
    {
        public class Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public Location Location { get; set; }
            public int OpenChargeSlots { get; set; }
            public List<DroneInCharge> Charging;
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
