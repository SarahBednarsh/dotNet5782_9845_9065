using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel Drone { get; set; }
        public DateTime? Creation { get; set; }
        public DateTime? Attribution { get; set; }
        public DateTime? PickUp { get; set; }
        public DateTime? Delivery { get; set; }
        public override string ToString()
        {
            return string.Format($"Id: {Id}, Sender: {Sender}, Target: {Target}, Weight: {Weight}, Priority: {Priority}, Drone: {Drone}, Creation time: {Creation}, Attribution time: {Attribution}, Pickup time: {PickUp}, Delivery time: {Delivery}");
        }
    }
}
