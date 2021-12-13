using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Parcel
    {

        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requested { get; set; }//Creation
        public int DroneId { get; set; }
        public DateTime? Scheduled { get; set; }//Attribution
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }
        public override string ToString()
        {
            return string.Format("Id: {0}, SenderId: {1}, TargetId: {2}, Weight: {3}, Priority: {4}, Requested: {5}, DroneId: {6}, Scheduled: {7}, PickedUp: {8}, Delivered: {9}", Id, SenderId, TargetId, Weight, Priority, Requested, DroneId, Scheduled, PickedUp, Delivered);
        }
    }
}
