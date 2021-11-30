﻿using System;
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
                IEnumerable<DroneToList> drones = from currentDrone in dronesBL
                                                   where currentDrone.Id == droneId //drone found
                                                   select currentDrone;
                if (drones.Count() < 1) //no drone with given id was found
                    throw new KeyDoesNotExist("No such drone");
                DroneToList drone = drones.First();
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
            public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging)
            { 
                try
                {
                    if (SearchStation(stationIdForCharging).OpenChargeSlots < 1) //station is not available
                        throw new NotEnoughChargingSlots("not enough charging slots in the station requested");
                    DroneToList drone = new DroneToList { Battery = new Random().Next(20, 40), Location = SearchStation(stationIdForCharging).Location, 
                                               Id = id, MaxWeight = maxWeight, Model = model, Status = DroneStatuses.InMaintenance, IdOfParcel = -1};
                    dronesBL.Add(drone);
                    dalAP.AddDrone(id, model, (IDAL.DO.WeightCategories)maxWeight);
                }
                catch (IDAL.DO.DroneException exception)
                {
                    throw new KeyAlreadyExists("Drone requested already exists", exception);
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
                //update in DAL - delete old and add new
                dalAP.DeleteDrone(droneId);
                dalAP.AddDrone(droneId, newModel, dalDrone.MaxWeight);
                DroneToList drone = dronesBL.Find(X => X.Id == droneId);
                drone.Model = newModel;
                //update in BL - remove old and insert new
                dronesBL.RemoveAll(X => X.Id == droneId);
                dronesBL.Add(drone);
            }
            public void DroneToCharge(int droneId)
            {
                Drone drone = SearchDrone(droneId);
                if (drone == null)
                    throw new KeyDoesNotExist("No such drone");
                if (drone.Status != DroneStatuses.Available) //drone is not available
                    throw new CannotSendToCharge("Drone is not available so it cannot be sent to charge");

                IEnumerable<Station> stations = YieldStation();
                double minDistance = -1;
                Station stationToSendTo = new Station();
                foreach (Station station in stations)
                {
                    //if station is available and the distance is less than the current chosen station
                    if ((station.OpenChargeSlots > 0) && ((LocationStaticClass.CalcDis(station.Location, drone.Location) < minDistance) || minDistance == -1))
                    {
                        minDistance = LocationStaticClass.CalcDis(station.Location, drone.Location);
                        stationToSendTo = station;
                    }
                }
                if (minDistance == -1) //no available station was found
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
                if (drone.Status != DroneStatuses.InMaintenance) //if drone was not charging
                    throw new CannotReleaseDroneFromCharging("Cannot release drone that is not in maintenence");
                drone.Battery = Math.Min(drone.Battery + timeCharging.TotalSeconds * chargingPace, 100);
                drone.Status = DroneStatuses.Available;
                //update drone in BL
                dronesBL.RemoveAll(x => x.Id == droneId);
                dronesBL.Add(drone);
                dalAP.ReleaseCharging(droneId);//in here it also updates the chargeslots in station 
            }
            public void AttributeAParcel(int droneId)
            {
                Drone drone;
                try
                { drone = SearchDrone(droneId); }
                catch (IDAL.DO.DroneException exception)
                { throw new KeyDoesNotExist("No such drone", exception); }
                if (drone.Status != DroneStatuses.Available)
                    throw new CannotAttribute("Drone is not available");
                IEnumerable<Parcel> parcels = YieldParcel(); //get list of parcels
                for (Priorities highest = Priorities.Emergency; highest > 0; highest--) //for each priority, stating with the most urgent
                {
                    IEnumerable<Parcel> relevant = (from parcel in parcels
                                                    where parcel.Priority == highest && parcel.Weight <= drone.MaxWeight //drone can carry the current parcel
                                                    select parcel).OrderBy(x => LocationStaticClass.CalcDis(GetSenderLocation(x), drone.Location));
                                                    //sort by increasing order of distance to the drone
                    foreach (Parcel p in relevant)
                    {
                        if (CanDeliver(drone, p)) //if drone can deliver parcel
                        {
                            drone.Status = DroneStatuses.Delivering; //update status
                            //update attribution of parcel in DAL
                            dalAP.UpdateParcelsDrone(p.Id, drone.Id);
                            dalAP.ScheduleParcel(p.Id); //set attribution time of parcel
                            return;
                        }
                    }
                }
            }
            private bool CanDeliver(Drone d, Parcel p) //checks if the drone has enough battery for delivering the parcel
            {
                double batteryNeeded = available * LocationStaticClass.CalcDis(d.Location, GetSenderLocation(p)); //battery needed for pickup 
                batteryNeeded += GetUsage(p.Weight) * LocationStaticClass.CalcDis(GetTargetLocation(p), GetSenderLocation(p)); //for delivery
                Station closest = CreateStation(GetClosestStation(GetTargetLocation(p))); //for getting to the closest charging station
                batteryNeeded += available * LocationStaticClass.CalcDis(GetTargetLocation(p), closest.Location);
                return batteryNeeded <= d.Battery; //drone has enough battery
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
                if (drone.Status != DroneStatuses.Delivering) //drone is not delivering
                    throw new CannotPickUp("Drone is not in delivery state");
                if (drone.Parcel.PickedUpAlready == true)//already picked up
                    throw new CannotPickUp("Drone has already picked up a parcel");
                double usage = GetUsage(drone.MaxWeight);
                double distance = LocationStaticClass.CalcDis(drone.Location, drone.Parcel.PickUpLocation);
                if (drone.Battery < distance * usage) //not enough battery
                    throw new NotEnoughBattery("not enough battery to get to sender");
                DroneToList newDrone = dronesBL.Find(x => x.Id == droneId);
                newDrone.Battery = newDrone.Battery - distance * usage; //update battery
                newDrone.Location = drone.Parcel.PickUpLocation; //update location
                //update drone in BL
                dronesBL.RemoveAll(x => x.Id == droneId);
                dronesBL.Add(newDrone);
                //update parcel pick up time
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
                if (drone.Status != DroneStatuses.Delivering) //drone is not delivering
                    throw new CannotDeliver("Drone is not in delivery state");
                double usage = GetUsage(drone.MaxWeight);
                double distance = LocationStaticClass.CalcDis(drone.Location, drone.Parcel.Destination);
                if (drone.Battery < distance * usage) //not anough battery to pick up
                    throw new NotEnoughBattery("Not enough battery to get to destination");
                DroneToList newDrone = dronesBL.Find(x => x.Id == droneId);
                newDrone.Battery = newDrone.Battery - distance * usage; //update battery
                newDrone.Location = drone.Parcel.Destination; //update location
                //update in BL
                dronesBL.RemoveAll(x => x.Id == droneId);
                dronesBL.Add(newDrone);
                //update parcel delivery time
                dalAP.DeliverToCustomer(drone.Parcel.Id);
            }
            private double GetUsage(WeightCategories weight) //return battery consumption for requested weight
            {
                if (weight == WeightCategories.Light) return light;
                else if (weight == WeightCategories.Medium) return medium;
                else return heavy;
            }
            public IEnumerable<DroneToList> ListDrone()
            {
                foreach (DroneToList drone in dronesBL)
                {
                    yield return new DroneToList { Id = drone.Id, Model = drone.Model, Location = LocationStaticClass.InitializeLocation(drone.Location), 
                        Battery = drone.Battery, IdOfParcel = drone.IdOfParcel, MaxWeight = drone.MaxWeight, Status = drone.Status };
                }
            }
        }
    }
}
