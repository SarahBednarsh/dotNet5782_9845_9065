using DO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Location
    {
        public Sexagesimal Longitude { get; set; }
        public Sexagesimal Latitude { get; set; }
        public override string ToString()
        {
            return String.Format("Longitude: {0}, Latitude: {1}", Longitude, Latitude);
        }

    }
}

