using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public Parcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, 
                DateTime requested, int droneId, DateTime scheduled, DateTime pickedUp, DateTime delivered)
            {
                this.Id = id;
                this.SenderId = senderId;
                this.TargetId = targetId;
                this.Weight = weight;
                this.Priority = priority;
                this.Requested = requested;
                this.DroneId = droneId;
                this.Scheduled = scheduled;
                this.PickedUp = pickedUp;
                this.Delivered = delivered;
            }
            public Parcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, 
                DateTime requested, int droneId)
            {
                this.Id = id;
                this.SenderId = senderId;
                this.TargetId = targetId;
                this.Weight = weight;
                this.Priority = priority;
                this.Requested = requested;
                this.DroneId = droneId;
                this.Scheduled = DateTime.Now;
                
            }
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                return string.Format("Id: {0}, Name: {1}, Phone: {2}, Longitude: {3}, Latitude: {4}", Id, Name, Phone, Longitude, Lattitude);
            }
        }
    }
}
