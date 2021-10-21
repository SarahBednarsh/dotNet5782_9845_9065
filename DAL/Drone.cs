﻿using System;
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
            public override string ToString()
            {
                return string.Format("Id: {0}, Model: {1}, MaxWeight: {2}, Status: {3}, Battery: {4}", Id, Model, MaxWeight, Status, Battery);
            }
        }
    }
}