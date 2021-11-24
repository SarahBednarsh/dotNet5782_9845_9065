using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace DalObject
{
    public partial class DalObject
    {
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority,
                DateTime requested, int droneId)
        {
            Parcel temp = new Parcel() { Id = ++DataSource.Config.RunningParcelNumber, SenderId = senderId, TargetId = targetId, Weight = weight, Priority = priority, Requested = requested, DroneId = droneId };
            DataSource.Parcels.Add(temp);
            return DataSource.Config.RunningParcelNumber;
        }
        public void DeleteParcel(int id)
        {
            if (!DataSource.Parcels.Exists(x => x.Id == id))
                throw new ParcelException("Customer to delete does not exist.");
            DataSource.Parcels.Remove(DataSource.Parcels.Find(x => x.Id == id));
        }

        /// <summary>
        /// picks up a parcel- first updates the parcel pickup town, then finds the drone attributed to the 
        /// parcel and updates it
        /// </summary>
        public void PickUpParcel(int parcelId)
        {
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)//if parcel doesn't exist
                throw new ParcelException("Customer to pick up does not exist.");
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
                throw new ParcelException("Customer to deliver does not exist.");
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.Delivered = DateTime.Now;
            DataSource.Parcels[indexParcel] = tempParcel;

            int indexDrone = DataSource.Drones.FindIndex(x => x.Id == tempParcel.DroneId);
            Drone tempDrone = DataSource.Drones[indexDrone];
            DataSource.Drones[indexDrone] = tempDrone;
        }
        public Parcel SearchParcel(int parcelId)
        {
            if (!DataSource.Parcels.Exists(x => x.Id == parcelId))
                throw new ParcelException("Customer does not exist.");
            return DataSource.Parcels.Find(x => x.Id == parcelId);
        }
        public IEnumerable<Parcel> YieldParcel()
        {
            return new List<Parcel>(DataSource.Parcels);
        }

        /// <summary>
        /// creates and returns a list of parcels without an attributed drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> ParcelsWithNoDrone()
        {
            List<Parcel> noDrone = new List<Parcel>();
            foreach (Parcel parcel in DataSource.Parcels)
            {
                if (parcel.DroneId == 0)
                    noDrone.Add(parcel);
            }
            return noDrone;
        }
    }
}
