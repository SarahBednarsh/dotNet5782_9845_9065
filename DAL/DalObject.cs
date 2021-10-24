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

        //the following functions recieve input for an object and create it
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
        /// <summary>
        /// Attributes a drone to a parcel by finding the drone, making sure it is available,
        /// finding the parcel and addig the drone ID to it
        /// </summary>
        public void UpdateParcelsDrone(int parcelId, int droneId)
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
        /// <summary>
        /// picks up a parcel- first updates the parcel pickup town, then finds the drone attributed to the 
        /// parcel and updates it
        /// </summary>
        public void PickUpParcel(int parcelId)
        {
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)//if parcel doesn't exist
                return;
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.PickedUp = DateTime.Now;
            DataSource.Parcels[indexParcel] = tempParcel;

            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == tempParcel.DroneId);
            Drone tempDrone = DataSource.Drones[indexDrone];
            tempDrone.Status = DroneStatuses.Delivering;
            DataSource.Drones[indexDrone] = tempDrone;
        }
        /// <summary>
        /// delivers a parcel to customer - updates the parcel delivery time and changes the drone to be availble
        /// </summary>
        /// <param name="parcelId"></param>
        public void DeliverToCustomer(int parcelId)
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
        /// <summary>
        /// sends a drone to charge -adds a drone charge to the log, and updates the drone and station accordingly
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
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
        /// <summary>
        /// releases a drone from charging by using the drone charges to find the charging station and updating
        /// the station, drone and drone charges accordingly
        /// </summary>
        /// <param name="droneId"></param>
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
        
        //the following functions return a requested object
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

        //the following functions create a new object and return it
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
        public List<Parcel> YieldParcel()
        {
            return new List<Parcel>(DataSource.Parcels);
        }
        /// <summary>
        /// creates and returns a list of parcels without an attributed drone
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// creates and returns a list of open charge slots
        /// </summary>
        /// <returns></returns>
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
