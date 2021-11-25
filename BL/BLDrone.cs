using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public Drone SearchDrone(int droneId)
            {
                
            }
            private Drone CreateDrone (IDAL.DO.Drone old)
            {
                Drone drone = new Drone { Battery = new Random().Next(20, 40) , Id=old.Id, Location=};//need to finish
                return drone;
            }
            public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging)
            {
                Drone drone = new Drone { Battery = new Random().Next(20, 40), Location = SearchStation(stationIdForCharging).Location, Id = id, MaxWeight = maxWeight, Model = model, Status = DroneStatuses.InMaintenance };
                //what should we do about the parcel- maybe do automatic null?
            }
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
                        minDistance = LocationStaticClass.CalcDis(station.Location,drone.Location);
                    }
                }
                foreach(Station station in stations)
                {
                    if ((station.OpenChargeSlots > 0) && (LocationStaticClass.CalcDis(station.Location,drone.Location) < minDistance))
                        minDistance = LocationStaticClass.CalcDis(station.Location,drone.Location);
                }S
                //if battery isnt enough to get there throw exception- or if t
                //if it is:
                //need to decide how to update it right- does search drone return hafnaya or whateveror is it a cpy? its a copy so what do we do
               // dalAP.UpdateDrone()

            }
            public void DeliverAParcel(int droneId)
            {

            }
            public void ReleaseCharging(int droneId, int timeCharging)
            {
                
            }
            public void AttributeUpAParcel(int droneId) 
            { }
            public void PickUpAParcel(int droneId) { }
            public void DeliverAParcel(int droneId) { }
            public IEnumerable<Drone> YieldDrone() { }
        }
    }
}
