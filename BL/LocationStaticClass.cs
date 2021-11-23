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
                double long1 = StaticSexagesimal.ParseDouble(location1.Longitude) * Math.PI / 180;
                double lat1 = StaticSexagesimal.ParseDouble(location1.Latitude) * Math.PI / 180;
                double long2 = StaticSexagesimal.ParseDouble(location2.Latitude) * Math.PI / 180;
                double lat2 = StaticSexagesimal.ParseDouble(location2.Latitude) * Math.PI / 180;

                // Haversine Formula
                double dlong = long2 - long1;
                double dlat = lat2 - lat1;

                double ans = Math.Pow(Math.Sin(dlat / 2), 2) +
                                      Math.Cos(lat1) * Math.Cos(lat2) *
                                      Math.Pow(Math.Sin(dlong /  2), 2);

                ans = 2 * Math.Asin(Math.Sqrt(ans));

                // Radius of Earth in
                // Kilometers, R = 6371
                // Use R = 3956 for miles
                double R = 6371;

                // Calculate the result
                ans = ans * R;

                return ans;
            }
        }
    }
}