using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public enum Directions { N, S, E, W }
    public class Sexagesimal
    {
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public double Seconds { get; set; }
        public Directions Direction { get; set; }
        public override string ToString()
        {
            String direction = "N";
            if (Direction == Directions.S)
                direction = "S";
            if (Direction == Directions.E)
                direction = "E";
            if (Direction == Directions.W)
                direction = "W";
            return String.Format("{0}°{1}'{2}''{3}", Degrees, Minutes, Math.Round(Seconds, 4), direction);
        }
    }
}
