using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int OpenChargeSlots { get; set; }

        public List<DroneInCharge> Charging;
        public override string ToString()
        {
            string charging = "";
            foreach (DroneInCharge drone in Charging)
                charging += drone.ToString();
            return string.Format($"Id: {Id}, Name: {Name}, Location: {Location}, Open charge slots: {OpenChargeSlots}, Drones charging: {charging}");
        }
    }
}
