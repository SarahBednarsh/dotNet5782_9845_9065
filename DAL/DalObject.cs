using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        public DroneStatuses Delivering { get; private set; }

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
            Drone requestedDrone = DataSource.Drones.Find(x => x.Id == droneId);
            if (requestedDrone.Status != DroneStatuses.Available)
                return;

            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)
                return;
            Parcel tempParcel = DataSource.Parcels[indexParcel];

            tempParcel.DroneId = droneId;
            DataSource.Parcels[indexParcel] = tempParcel;
        }
        // top of page 8 - ???
        public void PickUpParcel(int parcelId/*, int droneId*/)
        {
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            //if (indexParcel == -1)
            //    return;
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.PickedUp = DateTime.Now;
            DataSource.Parcels[indexParcel] = tempParcel;

            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == tempParcel.DroneId);
            Drone tempDrone = DataSource.Drones[indexDrone];
            tempDrone.Status = DroneStatuses.Delivering;
            DataSource.Drones[indexDrone] = tempDrone;
        }
        public void DeliverToCostumer(int parcelId)
        {
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)
                return;
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.Delivered = DateTime.Now;
            DataSource.Parcels[indexParcel] = tempParcel;

            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == tempParcel.DroneId);
            Drone tempDrone = DataSource.Drones[indexDrone];
            tempDrone.Status = DroneStatuses.Available;
            DataSource.Drones[indexDrone] = tempDrone;
        }
        public void DroneToCharge(int droneId, int stationId)
        {
            DataSource.DroneCharges.Add(new DroneCharge(droneId, stationId));

            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == droneId);
            Drone tempDrone = DataSource.Drones[indexDrone];
            tempDrone.Status = DroneStatuses.InMaintenance;
            DataSource.Drones[indexDrone] = tempDrone;

            int indexStation = DataSource.Stations.FindIndex(x => x.Id == stationId);
            Station tempStation = DataSource.Stations[indexStation];
            tempStation.ChargeSlots--;
            DataSource.Stations[indexStation] = tempStation;
        }
        public void ReleaseCharging(int droneId)
        {
            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == droneId);
            Drone tempDrone = DataSource.Drones[indexDrone];
            tempDrone.Status = DroneStatuses.Available;
            DataSource.Drones[indexDrone] = tempDrone;

            int indexCharge = DataSource.DroneCharges.FindIndex(x => x.DroneId == droneId);

            int stationId = DataSource.DroneCharges[indexCharge].StationId;
            int indexStation = DataSource.Stations.FindIndex(x => x.Id == stationId);
            Station tempStation = DataSource.Stations[indexStation];
            tempStation.ChargeSlots++;
            DataSource.Stations[indexStation] = tempStation;

            DataSource.DroneCharges.RemoveAt(indexCharge);
        }
        public Station SearchStation(int stationId)
        {
            return DataSource.Stations.Find(x => x.Id == stationId);
        }
        public Drone SearchDrone(int droneId)
        {
            return DataSource.Drones.Find(x => x.Id == droneId);
        }
        public Customer SearchCustomer(int customerId)
        {
            return DataSource.Customers.Find(x => x.Id == customerId);
        }
        public Parcel SearchParcel(int parcelId)
        {
            return DataSource.Parcels.Find(x => x.Id == parcelId);
        }
        public List<Station> YieldStation()
        {
            return new List<Station>(DataSource.Stations);
        }
        public List<Drone> YieldDrone()
        {
            return new List<Drone>(DataSource.Drones);
        }
        public List<Customer> YieldCustomer()
        {
            return new List<Customer>(DataSource.Customers);
        }
        public List<Parcel> YearchParcel()
        {
            return new List<Parcel>(DataSource.Parcels);
        }
        public List<Parcel> ParcelsWithNoDrone()
        {
            List<Parcel> noDrone = new List<Parcel>();
            foreach (Parcel parcel in DataSource.Parcels)
            {
                if (parcel.DroneId == 0)
                    noDrone.Add(parcel);
            }
            return noDrone;
        }
        public List<Station> OpenChargeSlots()
        {
            List<Station> open = new List<Station>();
            foreach (Station station in DataSource.Stations)
            {
                if (station.ChargeSlots > 0)
                    open.Add(station);
            }
            return open;
        }


    }
}
