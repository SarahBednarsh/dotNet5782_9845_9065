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

        //the following functions recieve input for an object and create it
        public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
        {
            Station tempStation = new Station() { Id = id, Name = name, Longitude = new Sexagesimal(longitude, "longitude"), Latitude = new Sexagesimal(latitude, "latitude"), ChargeSlots = chargeSlots };
            DataSource.Stations.Add(tempStation);
        }
        public void AddDrone(int id, string model, WeightCategories maxWeight)
        {
            Drone tempDrone = new Drone() { Id = id, Model = model, MaxWeight = maxWeight, };
            DataSource.Drones.Add(tempDrone);

        }
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            Customer tempCustomer = new Customer() { Id = id, Name = name, Phone = phone, Longitude = new Sexagesimal(longitude, "longitude"), Latitude = new Sexagesimal(latitude, "latitude") };
            DataSource.Customers.Add(tempCustomer);
        }
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority,
                DateTime requested, int droneId)
        {
            Parcel temp = new Parcel() { Id = ++DataSource.Config.RunningParcelNumber, SenderId = senderId, TargetId = targetId, Weight = weight, Priority = priority, Requested = requested, DroneId = droneId };
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
            DataSource.Drones[indexDrone] = tempDrone;
        }
        /// <summary>
        /// sends a drone to charge -adds a drone charge to the log, and updates the drone and station accordingly
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        public void DroneToCharge(int droneId, int stationId)
        {
            DataSource.DroneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = stationId });

            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == droneId);
            Drone tempDrone = DataSource.Drones[indexDrone];
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
        public IEnumerable<Parcel>/*List<Parcel>*/ ParcelsWithNoDrone()
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
        public double CalcDisFromStation(int id, double longitude, double latitude)
        {
            Station station = this.SearchStation(id);
            double deltalLongitude = station.Longitude.ParseDouble() - longitude;
            double deltalLatitude = station.Latitude.ParseDouble() - latitude;
            return Math.Sqrt(Math.Pow(deltalLatitude, 2) + Math.Pow(deltalLongitude, 2));
        }
        public double CalcDisFromCustomer(int id, double longitude, double latitude)
        {
            Customer customer = this.SearchCustomer(id);
            double deltalLongitude = customer.Longitude.ParseDouble() - longitude;
            double deltalLatitude = customer.Latitude.ParseDouble() - latitude;
            return Math.Sqrt(Math.Pow(deltalLatitude, 2) + Math.Pow(deltalLongitude, 2));
        }

    }
}
