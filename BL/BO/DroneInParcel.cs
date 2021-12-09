using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }
        public double Battery { get; set; }
        public Location Location { get; set; }
        public override string ToString()
        {
            return string.Format($"Id: {Id}, Battery: {Battery}, Location: {Location}");
        }
    }
}
