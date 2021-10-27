using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            //public Parcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, 
            //    DateTime requested, int droneId, DateTime scheduled, DateTime pickedUp, DateTime delivered)
            //{
            //    this.Id = id;
            //    this.SenderId = senderId;
            //    this.TargetId = targetId;
            //    this.Weight = weight;
            //    this.Priority = priority;
            //    this.Requested = requested;
            //    this.DroneId = droneId;
            //    this.Scheduled = scheduled;
            //    this.PickedUp = pickedUp;
            //    this.Delivered = delivered;
            //}
            //public Parcel(int id, int senderId, int targetId, WeightCategories weight, Priorities priority, 
            //    DateTime requested, int droneId)
            //{
            //    this.Id = id;
            //    this.SenderId = senderId;
            //    this.TargetId = targetId;
            //    this.Weight = weight;
            //    this.Priority = priority;
            //    this.Requested = requested;
            //    this.DroneId = droneId;
            //    this.Scheduled = DateTime.Now;
            //    this.PickedUp = DateTime.Now; // not nullable, what to do?
            //    this.Delivered = DateTime.Now; //kanal
            //}
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
                return string.Format("Id: {0}, SenderId: {1}, TargetId: {2}, Weight: {3}, Priority: {4}, Requested: {5}, DroneId: {6}, Scheduled: {7}, PickedUp: {8}, Delivered: {9}", Id, SenderId, TargetId, Weight, Priority, Requested, DroneId, Scheduled, PickedUp, Delivered);
            }
        }
    }
}
