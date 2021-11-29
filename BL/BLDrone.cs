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
                DroneToList drone = (from currentDrone in dronesBL
                                     where currentDrone.Id == droneId
                                     select currentDrone).FirstOrDefault();
                return new Drone
                {
                    Id = drone.Id,
                    Battery = drone.Battery,
                    Location = drone.Location,
                    MaxWeight = drone.MaxWeight,
                    Model = drone.Model,
                    Status = drone.Status,
                    Parcel = CreateParcelInTransfer(drone.IdOfParcel)
                };
            }
            private Drone CreateDrone(IDAL.DO.Drone old)//dont think this is right
            {
                Drone drone = new Drone { Battery = new Random().Next(20, 40), Id = old.Id, Location =};//need to finish
                return drone;
            }
            public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging)
            {
                try
                {
                    Drone drone = new Drone { Battery = new Random().Next(20, 40), Location = SearchStation(stationIdForCharging).Location, Id = id, MaxWeight = maxWeight, Model = model, Status = DroneStatuses.InMaintenance, Parcel = null };
                }
                catch (IDAL.DO.StationException exception)
                {
                    throw new KeyDoesNotExist("Station requested does not exist", exception);
                }
            }
            public void UpdateDroneModel(int droneId, string newModel)
            {
                IDAL.DO.Drone dalDrone;
                try
                {
                    dalDrone = dalAP.SearchDrone(droneId);
                }
                catch (IDAL.DO.DroneException exception)
                {
                    throw new KeyDoesNotExist("Drone to update does not exist", exception);
                }
                dalAP.DeleteDrone(droneId);
                dalAP.AddDrone(droneId, newModel, dalDrone.MaxWeight);
                DroneToList drone = dronesBL.Find(X => X.Id == droneId);
                drone.Model = newModel;
                dronesBL.RemoveAll(X => X.Id == droneId);
                dronesBL.Add(drone);
            }
            public void DroneToCharge(int droneId)
            {
                Drone drone = SearchDrone(droneId);
                if (drone == null)
                    throw new KeyDoesNotExist("No such drone");
                if (drone.Status != DroneStatuses.Available)
                    throw new CannotSendToCharge("Drone is not available so it cannot be sent to charge");

                IEnumerable<Station> stations = YieldStation();
                double minDistance = -1;
                Station stationToSendTo = new Station();
                foreach (Station station in stations)
                {
                    if ((station.OpenChargeSlots > 0) && ((LocationStaticClass.CalcDis(station.Location, drone.Location) < minDistance) || minDistance == -1))
                    {
                        minDistance = LocationStaticClass.CalcDis(station.Location, drone.Location);
                        stationToSendTo = station;
                    }
                }
                if (minDistance == -1)
                    throw new CannotSendToCharge("All stations are full");//liorah what should i write?
                double usage = GetUsage(drone.MaxWeight);
                if (drone.Battery < minDistance * usage)
                    throw new NotEnoughBattery("Not enough battery to get to station");
                drone.Battery = drone.Battery - minDistance * usage;
                drone.Location = stationToSendTo.Location;
                drone.Status = DroneStatuses.InMaintenance;
                dalAP.DroneToCharge(drone.Id, stationToSendTo.Id);//in here it also updates the chargeslots in station             
            }
            public void ReleaseCharging(int droneId, TimeSpan timeCharging)
            {
                DroneToList drone = dronesBL.Find(x => x.Id == droneId);
                if (drone == null)
                    throw new KeyDoesNotExist("No such drone");
                //  int stationId = SearchDroneCharge(droneId).stationId;
                if (drone.Status != DroneStatuses.InMaintenance)
                    throw new CannotReleaseDroneFromCharging("Cannot release drone that is not in maintenence");
                drone.Battery = Math.Min(drone.Battery + timeCharging.TotalSeconds * chargingPace, 100);
                drone.Status = DroneStatuses.Available;
                dronesBL.RemoveAll(x => x.Id == droneId);
                dronesBL.Add(drone);
                dalAP.ReleaseCharging(droneId);//in here it also updates the chargeslots in station 
            }
            public void AttributeAParcel(int droneId)
            {
                Drone drone;
                try
                { drone = SearchDrone(droneId); }
                catch(IDAL.DO.DroneException exception)
                { throw new KeyDoesNotExist("No such drone", exception); }
                if (drone.Status != DroneStatuses.Available)
                    throw new CannotAttribute("Drone is not available");

            }
            public void PickUpAParcel(int droneId)
            {
                Drone drone;
                try
                {
                    drone = SearchDrone(droneId);
                }
                catch (IDAL.DO.DroneException exception)
                { throw new KeyDoesNotExist("Drone for delivery does not exist", exception); }
                if (drone.Status != DroneStatuses.Delivering)
                    throw new CannotPickUp("Drone is not in delivery state");
                if (drone.Parcel.PickedUpAlready == true)//already picked up
                    throw new CannotPickUp("Drone has already picked up a parcel");
                double usage = GetUsage(drone.MaxWeight);
                double distance = LocationStaticClass.CalcDis(drone.Location, drone.Parcel.PickUpLocation);
                if (drone.Battery < distance * usage)
                    throw new NotEnoughBattery("not enough battery to get to sender");
                DroneToList newDrone = dronesBL.Find(x => x.Id == droneId);
                newDrone.Battery = newDrone.Battery - distance * usage;
                newDrone.Location = drone.Parcel.PickUpLocation;
                dronesBL.RemoveAll(x => x.Id == droneId);
                dronesBL.Add(newDrone);
                dalAP.PickUpParcel(drone.Parcel.Id);
            }
            public void DeliverAParcel(int droneId)
            {
                Drone drone;
                try
                {
                    drone = SearchDrone(droneId);
                }
                catch (IDAL.DO.DroneException exception)
                { throw new KeyDoesNotExist("Drone for delivery does not exist", exception); }
                if (drone.Parcel.PickedUpAlready == false)//hasn't been picked up
                    throw new CannotDeliver("Drone does not have a parcel attributed to deliver");
                if (drone.Status != DroneStatuses.Delivering)
                    throw new CannotDeliver("Drone is not in delivery state");
                double usage = GetUsage(drone.MaxWeight);
                double distance = LocationStaticClass.CalcDis(drone.Location, drone.Parcel.Destination);
                if (drone.Battery < distance * usage)
                    throw new NotEnoughBattery("Not enough battery to get to destination");
                DroneToList newDrone = dronesBL.Find(x => x.Id == droneId);
                newDrone.Battery = newDrone.Battery - distance * usage;
                newDrone.Location = drone.Parcel.Destination;
                dronesBL.RemoveAll(x => x.Id == droneId);
                dronesBL.Add(newDrone);
                dalAP.DeliverToCustomer(drone.Parcel.Id);
            }
            private double GetUsage(WeightCategories weight)
            {
                if (weight == WeightCategories.Light) return light;
                else if (weight == WeightCategories.Medium) return medium;
                else return heavy;
            }
        }
    }
}
