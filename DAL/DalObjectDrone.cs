using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        public void AddDrone(int id, string model, WeightCategories maxWeight)
        {
            Drone tempDrone = new Drone() { Id = id, Model = model, MaxWeight = maxWeight, };
            DataSource.Drones.Add(tempDrone);

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
        public Drone SearchDrone(int droneId)
        {
            return DataSource.Drones.Find(x => x.Id == droneId);
        }
        public IEnumerable<Drone> YieldDrone()
        {
            return new List<Drone>(DataSource.Drones);
        }
        public IEnumerable<double> ReqPowerConsumption()
        {
            yield return DataSource.Config.available;
            yield return DataSource.Config.light;
            yield return DataSource.Config.medium;
            yield return DataSource.Config.heavy;
            yield return DataSource.Config.chargingPace;
        }

    }
}
