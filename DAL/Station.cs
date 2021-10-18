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
            public override string ToString()
            {
                return string.Format("Id: {0}, Name: {1}, Longitude: {2}, Latitude: {3}, ChargeSlots: {4}", Id, Name, Longitude, Latitude, ChargeSlots);
            }
        }
    }
}
