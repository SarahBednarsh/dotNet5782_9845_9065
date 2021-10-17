using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public Station(int id, int name, double longitude, double latitude, int chargeSlots)
            {
                this.Id = id;
                this.Name = name;
                this.Longitude = longitude;
                this.Latitude = latitude;
                this.ChargeSlots = chargeSlots;
            }
            public int Id { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ChargeSlots { get; set; }
        }
    }
}
