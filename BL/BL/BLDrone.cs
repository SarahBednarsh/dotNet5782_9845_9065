using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BO;
using DO;
using Parcel = BO.Parcel;
using Priorities = BO.Priorities;
using WeightCategories = BO.WeightCategories;
using Drone = BO.Drone;
using Station = BO.Station;
using Customer = BO.Customer;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal DroneToList GetReferenceDroneToList(int droneId) => dronesBL.Find(x => x.Id == droneId);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneToList SearchDroneToList(int droneId)
        {
            IEnumerable<DroneToList> drones = from currentDrone in dronesBL
                                              where currentDrone.Id == droneId //drone found
                                              select currentDrone;
            if (drones.Count() < 1) //no drone with given id was found
                throw new KeyDoesNotExist("No such drone");
            DroneToList drone = drones.First();
            return new DroneToList
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = drone.Location,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Status = drone.Status,
                IdOfParcel = drone.IdOfParcel
            };
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone SearchDrone(int droneId)
        {
            IEnumerable<DroneToList> drones = from currentDrone in dronesBL
                                              where currentDrone.Id == droneId //drone found
                                              select currentDrone;
            if (drones.Count() < 1) //no drone with given id was found
                throw new KeyDoesNotExist("No such drone");
            DroneToList drone = drones.First();
            ParcelInTransfer parcel;
            try
            {
                parcel = CreateParcelInTransfer(drone.IdOfParcel);
            }
            catch (DO.ParcelException)
            {
                parcel = null;
            }
            return new Drone
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = drone.Location,
                MaxWeight = drone.MaxWeight,
                Model = drone.Model,
                Status = drone.Status,
                Parcel = parcel
            };
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging)
        {
            try
            {
                if (SearchStation(stationIdForCharging).OpenChargeSlots < 1) //station is not available
                    throw new NotEnoughChargingSlots("not enough charging slots in the station requested");
                DroneToList drone = new DroneToList
                {
                    Battery = new Random().Next(20, 40),
                    Location = SearchStation(stationIdForCharging).Location,
                    Id = id,
                    MaxWeight = maxWeight,
                    Model = model,
                    Status = DroneStatuses.InMaintenance,
                    IdOfParcel = -1
                };
                lock (dalAP)
                {
                    dronesBL.Add(drone);
                    dalAP.AddDrone(id, model, (DO.WeightCategories)maxWeight);
                    dalAP.DroneToCharge(id, stationIdForCharging);
                }
            }
            catch (DO.DroneException exception)
            {
                throw new KeyAlreadyExists("Drone requested already exists", exception);
            }
            catch (DO.StationException exception)
            {
                throw new KeyDoesNotExist("Station requested does not exist", exception);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int droneId)
        {
            Drone drone = SearchDrone(droneId);
            if (drone.Status != DroneStatuses.Available)
                throw new CannotDelete($"Drone is {drone.Status}, cannot delete");
            try
            {
                lock (dalAP)
                {
                    dronesBL.RemoveAll(x => x.Id == drone.Id);
                    dalAP.DeleteDrone(droneId);
                }
            }
            catch (DroneException exception)
            {
                throw new KeyDoesNotExist("No such drone", exception);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDroneModel(int droneId, string newModel)
        {
            lock (dalAP)
            {
                DO.Drone dalDrone;
                try
                {
                    dalDrone = dalAP.SearchDrone(droneId);
                }
                catch (DO.DroneException exception)
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
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                throw new CannotSendToCharge("All stations are full");
            if (stationToSendTo.OpenChargeSlots == 0)
                throw new CannotSendToCharge("All charge slots are taken");
            double usage = GetUsage(drone.MaxWeight);
            if (drone.Battery < minDistance * usage)
                throw new NotEnoughBattery("Not enough battery to get to station");
            DroneToList droneInBL = dronesBL.Find(x => x.Id == droneId);
            droneInBL.Battery = drone.Battery - minDistance * usage;
            droneInBL.Location = stationToSendTo.Location;
            droneInBL.Status = DroneStatuses.InMaintenance;
            lock (dalAP)
            {
                dalAP.DroneToCharge(drone.Id, stationToSendTo.Id);//in here it also updates the chargeslots in station             
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseCharging(int droneId)
        {
            lock (dalAP)
            {
                DroneToList drone = dronesBL.Find(x => x.Id == droneId);
                if (drone == null)
                    throw new KeyDoesNotExist("No such drone");
                if (drone.Status != DroneStatuses.InMaintenance) //if drone was not charging
                    throw new CannotReleaseDroneFromCharging("Cannot release drone that is not in maintenence");
                TimeSpan? timeCharging = DateTime.Now - dalAP.GetBeginningChargeTime(droneId);
                drone.Battery = Math.Min(drone.Battery + timeCharging.Value.TotalSeconds * chargingPace, 100);
                drone.Status = DroneStatuses.Available;
                //update drone in BL
                //dronesBL.RemoveAll(x => x.Id == droneId);
                //dronesBL.Add(drone);
                dalAP.ReleaseCharging(droneId);//in here it also updates the chargeslots in station 
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AttributeAParcel(int droneId)
        {
            Drone drone;
            try
            { drone = SearchDrone(droneId); }
            catch (DO.DroneException exception)
            { throw new KeyDoesNotExist("No such drone", exception); }
            if (drone.Status != DroneStatuses.Available)
                throw new CannotAttribute("Drone is not available");
            IEnumerable<Parcel> parcels = YieldParcel(); //get list of parcels
            for (Priorities highest = Priorities.Emergency; highest > 0; highest--) //for each priority, stating with the most urgent
            {
                IEnumerable<Parcel> relevant = (from parcel in parcels
                                                where parcel.PickUp == null
                                                where parcel.Priority == highest && parcel.Weight <= drone.MaxWeight //drone can carry the current parcel
                                                select parcel).OrderBy(x => LocationStaticClass.CalcDis(GetSenderLocation(x), drone.Location));
                //sort by increasing order of distance to the drone
                foreach (Parcel p in relevant)
                {
                    if (CanDeliver(drone, p)) //if drone can deliver parcel
                    {
                        DroneToList droneInBL = dronesBL.Find(x => x.Id == droneId);
                        droneInBL.Status = DroneStatuses.Delivering; //update status
                        droneInBL.IdOfParcel = p.Id; //update parcel id in BL
                                                     //update attribution of parcel in DAL
                        lock (dalAP)
                        {
                            dalAP.UpdateParcelsDrone(p.Id, drone.Id);
                            dalAP.ScheduleParcel(p.Id); //set attribution time of parcel
                        }
                        return;
                    }
                }
            }
            throw new CannotAttribute("No parcel to attribute was found (drone might not have enough battery, or there are no more parcels to deliver)");
        }
        private bool CanDeliver(Drone d, Parcel p) //checks if the drone has enough battery for delivering the parcel
        {
            double batteryNeeded = available * LocationStaticClass.CalcDis(d.Location, GetSenderLocation(p)); //battery needed for pickup 
            batteryNeeded += GetUsage(p.Weight) * LocationStaticClass.CalcDis(GetTargetLocation(p), GetSenderLocation(p)); //for delivery
            Station closest = CreateStation(GetClosestStation(GetTargetLocation(p))); //for getting to the closest charging station
            batteryNeeded += available * LocationStaticClass.CalcDis(GetTargetLocation(p), closest.Location);
            return batteryNeeded <= d.Battery; //drone has enough battery
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickUpAParcel(int droneId)
        {
            Drone drone;
            try
            {
                drone = SearchDrone(droneId);
            }
            catch (DO.DroneException exception)
            { throw new KeyDoesNotExist("Drone for delivery does not exist", exception); }
            if (drone.Status != DroneStatuses.Delivering) //drone is not delivering
                throw new CannotPickUp("Drone is not in delivery state");
            if (drone.Parcel.PickedUpAlready == true)//already picked up
                throw new CannotPickUp("Drone has already picked up a parcel");
            double usage = GetUsage(drone.Parcel.Weight);
            double distance = LocationStaticClass.CalcDis(drone.Location, drone.Parcel.PickUpLocation);
            if (drone.Battery < distance * usage) //not enough battery
                throw new NotEnoughBattery("not enough battery to get to sender");
            //update drone in BL
            DroneToList newDrone = dronesBL.Find(x => x.Id == droneId);
            newDrone.Battery = newDrone.Battery - distance * usage; //update battery
            newDrone.Location = drone.Parcel.PickUpLocation; //update location
            //update parcel pick up time
            lock (dalAP)
            {
                dalAP.PickUpParcel(drone.Parcel.Id);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverAParcel(int droneId)
        {
            Drone drone;
            try
            {
                drone = SearchDrone(droneId);
            }
            catch (DO.DroneException exception)
            { throw new KeyDoesNotExist("Drone for delivery does not exist", exception); }
            if (drone.Parcel.PickedUpAlready == false)//hasn't been picked up
                throw new CannotDeliver("Drone does not have a parcel attributed to deliver");
            if (drone.Status != DroneStatuses.Delivering) //drone is not delivering
                throw new CannotDeliver("Drone is not in delivery state");
            double usage = GetUsage(drone.Parcel.Weight);
            double distance = LocationStaticClass.CalcDis(drone.Location, drone.Parcel.Destination);
            if (drone.Battery < distance * usage) //not anough battery to pick up
                throw new NotEnoughBattery("Not enough battery to get to destination");
            //update in BL
            DroneToList newDrone = dronesBL.Find(x => x.Id == droneId);
            newDrone.Battery = newDrone.Battery - distance * usage; //update battery
            newDrone.Location = drone.Parcel.Destination; //update location
            newDrone.IdOfParcel = -1;
            newDrone.Status = DroneStatuses.Available;
            lock (dalAP)
            {
                dalAP.DeliverToCustomer(drone.Parcel.Id);
            }
        }
        internal double GetUsage(WeightCategories weight) //return battery consumption for requested weight
        {
            if (weight == WeightCategories.Light) return light;
            else if (weight == WeightCategories.Medium) return medium;
            else return heavy;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseAllCharging()
        {
            dronesBL.Where(drone => drone.Status == DroneStatuses.InMaintenance).ToList().ForEach(drone => ReleaseCharging(drone.Id));
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> ListDrone()
        {
            return from DroneToList drone in dronesBL
                   select new DroneToList
                   {
                       Id = drone.Id,
                       Model = drone.Model,
                       Location = LocationStaticClass.InitializeLocation(drone.Location),
                       Battery = drone.Battery,
                       IdOfParcel = drone.IdOfParcel,
                       MaxWeight = drone.MaxWeight,
                       Status = drone.Status
                   };
        }
        public void ActivateDroneSimulator(int droneId, Action update, Func<bool> stop) => new Simulator(this, droneId, update, stop);
    }
}
