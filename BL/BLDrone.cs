using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public Drone SearchDrone(int droneId) { }
            public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging) { }
            public void UpdateDroneName(int droneId, string newName) { }
            public void DroneToCharge(int droneId)
            {
                Drone drone = SearchDrone(droneId);
                IEnumerable < Station > stations= YieldStation();//im assuming that it returns ibl.bo stations

                double minDistance = -1;
                foreach(Station station in stations)
                {
                    if(station.OpenChargeSlots > 0)
                    {
                        minDistance = station.Location.CalcDis(drone.Location);
                    }
                }
                foreach(Station station in stations)
                {
                    if ((station.OpenChargeSlots > 0) && (station.Location.CalcDis(drone.Location) < minDistance))
                        minDistance = station.Location.CalcDis(drone.Location);
                }
                //if battery isnt enough to get there throw exception- or if t
            }
            public void ReleaseCharging(int droneId, int timeCharging) { }
            public void PickUpAParcel(int droneId) { }
            public void DeliverAParcel(int droneId) { }
            public IEnumerable<Drone> YieldDrone() { }
        }
    }
}
