using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using IBL.BO;

namespace IBL
{
    namespace BO
    {
        public static class LocationStaticClass //functions to handle Location class
        {
            /// <summary>
            /// creates Location from double longitude and latitude
            /// </summary>
            /// <param name="longitude"></param>
            /// <param name="latitude"></param>
            /// <returns></returns>
            public static Location InitializeLocation(double longitude, double latitude)
            {
                return new Location { Longitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(latitude, "Latitude") };
            }
            /// <summary>
            /// creates Location from Sexagesimal longitude and latitude
            /// </summary>
            /// <param name="longitude"></param>
            /// <param name="latitude"></param>
            /// <returns></returns>
            public static Location InitializeLocation(Sexagesimal longitude, Sexagesimal latitude)
            {
                return new Location { Longitude = StaticSexagesimal.InitializeSexagesimal(longitude), Latitude = StaticSexagesimal.InitializeSexagesimal(latitude) };
            }
            /// <summary>
            /// creates Location from another Location object
            /// </summary>
            /// <param name="l"></param>
            /// <returns></returns>
            public static Location InitializeLocation(Location l)
            {
                return InitializeLocation(l.Longitude, l.Latitude);
            }
            /// <summary>
            /// calculates distance between 2 Locations
            /// </summary>
            /// <param name="location1"></param>
            /// <param name="location2"></param>
            /// <returns></returns>
            public static double CalcDis(Location location1, Location location2)
            {
                return StaticSexagesimal.CalcDis(location1.Longitude, location1.Latitude, location2.Longitude, location2.Latitude);
            }
        }
    }
}