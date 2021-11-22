using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using IBL.BO;

namespace IBL
{
    namespace BO
    {
        public static class LocationStaticClass
        {
            public double CalcDis(Location location1, Location location2)
            {
                double deltalLongitude = StaticSexagesimal.ParseDouble(location1.Longitude) - StaticSexagesimal.ParseDouble(this.Longitude);
                double deltalLatitude = StaticSexagesimal.ParseDouble(location1.Latitude) - StaticSexagesimal.ParseDouble(this.Latitude);
                return Math.Sqrt(Math.Pow(deltalLatitude, 2) + Math.Pow(deltalLongitude, 2));
            }
        }
    }
}