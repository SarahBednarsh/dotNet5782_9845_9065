using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public ParcelInTransfer Parcel { get; set; }
        public Location Location { get; set; }
        public override string ToString()
        {
            return string.Format($"Id: {Id}, Model: {Model}, Maximum weight: {MaxWeight}, Battery: {Battery}, Status: {Status}, Parcel in transfer: {Parcel}, Location: {Location}");
        }
    }
}
