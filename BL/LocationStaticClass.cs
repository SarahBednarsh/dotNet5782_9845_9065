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
            public static Location InitializeLocation(double longitude, double latitude)
            {
                return new Location { Longitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Latitude") };
            }
            public static Location InitializeLocation(Sexagesimal longitude, Sexagesimal latitude)
            {
                return new Location { Longitude = StaticSexagesimal.InitializeSexagesimal(longitude), Latitude = StaticSexagesimal.InitializeSexagesimal(longitude) };
            }
            public static Location InitializeLocation(Location l)
            {
                return InitializeLocation(l.Longitude, l.Latitude);
            }
            public static double CalcDis(Location location1, Location location2)
            {
                return StaticSexagesimal.CalcDis(location1.Longitude, location1.Latitude, location2.Longitude, location2.Latitude);
            }
        }
    }
}