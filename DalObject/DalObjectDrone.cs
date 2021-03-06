using System;
using System.Collections.Generic;
using System.Text;
using DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal
{
    partial class DalObject
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategories maxWeight)
        {
            if (DataSource.Drones.Exists(x => x.Id == id))
                throw new DroneException("Drone to add exists.");
            Drone tempDrone = new Drone() { Id = id, Model = model, MaxWeight = maxWeight, };
            DataSource.Drones.Add(tempDrone);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            if (!DataSource.Drones.Exists(x => x.Id == id))
                throw new DroneException("Drone to delete does not exist.");
            DataSource.Drones.Remove(DataSource.Drones.Find(x => x.Id == id));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelsDrone(int parcelId, int droneId)
        {
            //Drone requestedDrone = DataSource.Drones.Find(x => x.Id == droneId);
            if (!DataSource.Drones.Exists(x => x.Id == droneId))
                throw new DroneException("Requested drone was not found");
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)
                throw new ParcelException("Parcel to update does not exist.");
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.DroneId = droneId;
            DataSource.Parcels[indexParcel] = tempParcel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneToCharge(int droneId, int stationId)
        {

            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == droneId);
            if (indexDrone == -1)
                throw new DroneException("Drone to charge does not exist.");
            int indexStation = DataSource.Stations.FindIndex(x => x.Id == stationId);
            if (indexStation == -1)
                throw new StationException("Station for charging does not exist.");
            Station tempStation = DataSource.Stations[indexStation];
            tempStation.ChargeSlots--;
            DataSource.Stations[indexStation] = tempStation;
            DataSource.DroneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = stationId, BeginTime = DateTime.Now });

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseCharging(int droneId)
        {
            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == droneId);
            if (indexDrone == -1)
                throw new DroneException("Drone to release does not exist.");
            Drone tempDrone = DataSource.Drones[indexDrone];
            DataSource.Drones[indexDrone] = tempDrone;
            int indexCharge = DataSource.DroneCharges.FindIndex(x => x.DroneId == droneId);
            if (indexCharge == -1)
                throw new DroneException("Drone was not in charging.");
            int stationId = DataSource.DroneCharges[indexCharge].StationId;
            int indexStation = DataSource.Stations.FindIndex(x => x.Id == stationId);
            Station tempStation = DataSource.Stations[indexStation];
            tempStation.ChargeSlots++;
            DataSource.Stations[indexStation] = tempStation;
            DataSource.DroneCharges.RemoveAt(indexCharge);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DateTime? GetBeginningChargeTime(int droneId)
        {
            int indexCharge = DataSource.DroneCharges.FindIndex(x => x.DroneId == droneId);
            if (indexCharge == -1)
                throw new DroneException("Drone was not in charging.");
            return DataSource.DroneCharges[indexCharge].BeginTime;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone SearchDrone(int droneId)
        {
            if (!DataSource.Drones.Exists(x => x.Id == droneId))
                throw new DroneException("Drone does not exist.");
            return DataSource.Drones.Find(x => x.Id == droneId);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> YieldDrone()
        {
            return new List<Drone>(DataSource.Drones);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
