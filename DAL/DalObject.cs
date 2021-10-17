using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
        {
            Station tempStation = new Station(id, name, longitude, latitude, chargeSlots);
            DataSource.Stations.Add(tempStation);
        }
        
        public void AddDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
        {
            Drone tempDrone = new Drone(id, model, maxWeight, status, battery);
            DataSource.Drones.Add(tempDrone);

        }
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            Customer tempCustomer = new Customer(id, name, phone, longitude, latitude);
            DataSource.Customers.Add(tempCustomer);
        }
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority,
                DateTime requested, int droneId)
        {
            Parcel temp = new Parcel(++DataSource.Config.RunningParcelNumber, senderId, targetId, weight, priority, requested, droneId);
            DataSource.Parcels.Add(temp);
            return DataSource.Config.RunningParcelNumber;
        }
        public void UpdateParcelsDrone(int parcelId, int droneId)// also copy for each entity and change parameters
        {
            Parcel tempParcel = DataSource.Parcels.Find(x => x.Id == parcelId);
            tempParcel.DroneId = droneId;
            Drone tempDrone = DataSource.Drones.Find(x => x.Id == droneId);
            tempDrone.Status = DroneStatuses.Delievering;
        }
        // top of page 8 - ???
    }
}
