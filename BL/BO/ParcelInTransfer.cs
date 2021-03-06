using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class ParcelInTransfer
    {
        public int Id { get; set; }
        public bool PickedUpAlready { get; set; }
        public Priorities Priority { get; set; }
        public WeightCategories Weight { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Target { get; set; }
        public Location PickUpLocation { get; set; }
        public Location Destination { get; set; }
        public double Distance { get; set; }
        public override string ToString()
        {
            return string.Format($"Id: {Id}, Already picked up: {PickedUpAlready}, Priority: {Priority}, Weight: {Weight}, Sender: {Sender}, Target: {Target}, Pick up location: {PickUpLocation}, Destination location: {Destination}, Distance from target: {Distance}");
        }
    }
}
