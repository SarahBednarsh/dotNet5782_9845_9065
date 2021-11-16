using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging) { }
            public void UpdateDroneName(int droneId, string newName) { }
            public void DroneToCharge(int droneId) { }
            public void ReleaseCharging(int droneId, int timeCharging) { }
            public void PickUpAParcel(int droneId) { }
            public void DeliverAParcel(int droneId) { }
            public Drone SearchDrone(int droneId) { }
            public IEnumerable<Drone> YieldDrone() { }
        }
    }
}
