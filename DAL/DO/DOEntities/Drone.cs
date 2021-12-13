using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Drone
    {

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public override string ToString()
        {
            return string.Format("Id: {0}, Model: {1}, MaxWeight: {2}", Id, Model, MaxWeight);
        }
    }
}