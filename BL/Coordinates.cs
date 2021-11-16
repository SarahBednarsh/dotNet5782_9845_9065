using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {
        public class Coordinates
        {
            public Sexagesimal Longitude { get; set; }
            public Sexagesimal Latitude { get; set; }
            public Coordinates(double longitude, double latitude)
            {
                Longitude = new Sexagesimal(longitude, "longitude");
                Latitude = new Sexagesimal(latitude, "latitude");
            }
            public double CalcDis(Coordinates location)
            {
                double deltalLongitude = location.Longitude.ParseDouble() - Longitude.ParseDouble();
                double deltalLatitude = location.Latitude.ParseDouble() - Latitude.ParseDouble();
                return Math.Sqrt(Math.Pow(deltalLatitude, 2) + Math.Pow(deltalLongitude, 2));
            }
            public override string ToString()
            {
                return String.Format("Longitude: {0}, Latitude: {1}", Longitude, Latitude);
            }

        }
    }
}
