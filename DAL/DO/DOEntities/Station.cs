using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sexagesimal Longitude { get; set; }
        public Sexagesimal Latitude { get; set; }
        public int ChargeSlots { get; set; }
        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, Longitude: {2}, Latitude: {3}, ChargeSlots: {4}", Id, Name, Longitude, Latitude, ChargeSlots);
        }
    }
}