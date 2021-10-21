using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    enum Directions { N, S, E, W }

    class Sexagesimal
    {
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public Directions Direction { get; set; }
        public Sexagesimal(int degrees, int minutes, int seconds, Directions direction)
        {
            Degrees = degrees;
            Minutes = minutes;
            Seconds = seconds;
            Direction = direction;
        }
        public Sexagesimal(double pos, String direction)
        {
            if (direction == "Longitude")
            {
                if (pos < 0)
                    Direction = Directions.W;
                else
                    Direction = Directions.E;
            }
            else
            {
                if (pos < 0)
                    Direction = Directions.S;
                else
                    Direction = Directions.N;
            }
            Degrees = (int)pos % 360;
            Minutes = (int)(pos * 60) % 60;
            Seconds = (int)(pos * 60 * 60) % 60;
        }
        public int ParseInt()
        {
            int factor = 1;
            if (Direction == Directions.S || Direction == Directions.W)
                factor = -1;
            return factor * (int)(Degrees + (double)Minutes / 60 + (double)Seconds / 3600);
        }
        public override string ToString()
        {
            String direction = "N";
            //if (Direction == Directions.N)
            //    direction = "N";
            if (Direction == Directions.S)
                direction = "S";
            if (Direction == Directions.E)
                direction = "E";
            if (Direction == Directions.W)
                direction = "W";
            return String.Format("{}°{}'{}''", Degrees, Minutes, Seconds, direction);
        }

    }
}
