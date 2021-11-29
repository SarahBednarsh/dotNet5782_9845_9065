using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public static class StaticSexagesimal
        {
            public static Sexagesimal InitializeSexagesimal(double pos, String direction)
            {
                Directions dir;
                if (direction == "Longitude")
                {
                    if (pos < 0)
                        dir = Directions.W;
                    else
                        dir = Directions.E;
                }
                else
                {
                    if (pos < 0)
                        dir = Directions.S;
                    else
                        dir = Directions.N;
                }
                pos = Math.Abs(pos);
                int degrees = (int)pos % 360;
                int minutes = (int)(pos * 60) % 60;
                double seconds = (pos * 60 * 60) % 60;
                return new Sexagesimal { Degrees = degrees, Minutes = minutes, Seconds = seconds, Direction = dir };
            }
            public static Sexagesimal InitializeSexagesimal(int degrees, int minutes, double seconds, Directions direction)
            {
                return new Sexagesimal { Degrees = degrees, Minutes = minutes, Seconds = seconds, Direction = direction };
            }
            public static Sexagesimal InitializeSexagesimal(Sexagesimal copy)
            {
                if (copy == null)
                    throw new ArgumentNullException();//sarah
                return InitializeSexagesimal(copy.Degrees, copy.Minutes, copy.Seconds, copy.Direction);
            }
            public static double ParseDouble(Sexagesimal coordinate)//converts a sexagesimal coordinate to decimal
            {
                int factor = 1;
                if (coordinate.Direction == Directions.S || coordinate.Direction == Directions.W)
                    factor = -1;
                return factor * (int)(coordinate.Degrees + (double)coordinate.Minutes / 60 + (double)coordinate.Seconds / 3600);
            }
            public static double CalcDis(double longitude1, double latitude1, double longitude2, double latitude2)
            {
                double long1 = longitude1 * Math.PI / 180;
                double lat1 = latitude1 * Math.PI / 180;
                double long2 = longitude2 * Math.PI / 180;
                double lat2 = latitude2 * Math.PI / 180;

                // Haversine Formula
                double dlong = long2 - long1;
                double dlat = lat2 - lat1;

                double ans = Math.Pow(Math.Sin(dlat / 2), 2) +
                                      Math.Cos(lat1) * Math.Cos(lat2) *
                                      Math.Pow(Math.Sin(dlong / 2), 2);

                ans = 2 * Math.Asin(Math.Sqrt(ans));

                // Radius of Earth in
                // Kilometers, R = 6371
                // Use R = 3956 for miles
                double R = 6371;

                // Calculate the result
                ans = ans * R;

                return ans;
            }
            public static double CalcDis(Sexagesimal longitude1, Sexagesimal latitude1, Sexagesimal longitude2, Sexagesimal latitude2)
            {
                double long1 = ParseDouble(longitude1);
                double lat1 = ParseDouble(latitude1);
                double long2 = ParseDouble(longitude2);
                double lat2 = ParseDouble(latitude2);
                return CalcDis(long1, lat1, long2, lat2);
               
            }
        }
    }
}
