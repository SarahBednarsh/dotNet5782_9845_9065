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
                return InitializeSexagesimal(copy.Degrees, copy.Minutes, copy.Seconds, copy.Direction);
            }
            public static double ParseDouble(Sexagesimal coordinate)//converts a sexagesimal coordinate to decimal
            {
                int factor = 1;
                if (coordinate.Direction == Directions.S || coordinate.Direction == Directions.W)
                    factor = -1;
                return factor * (int)(coordinate.Degrees + (double)coordinate.Minutes / 60 + (double)coordinate.Seconds / 3600);
            }
        }
    }
}
