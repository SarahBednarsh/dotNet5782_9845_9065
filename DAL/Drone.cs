using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public Drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
            {
                this.Id = id;
                this.Model = model;
                this.MaxWeight = maxWeight;
                this.Status = status;
                this.Battery = battery;
            }
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
        }
    }
}