using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses Status { get; set; }
            public Location Location  { get; set; }
            public int IdOfParcel { get; set; }
            public override string ToString()
            {
                return string.Format($"Id: {Id}, Model: {Model}, Maximum weight: {MaxWeight}, Battery: {Battery}, Status: {Status}, Location: {Location}, Id of parcel: {IdOfParcel}");
            }
        }
    }
}
