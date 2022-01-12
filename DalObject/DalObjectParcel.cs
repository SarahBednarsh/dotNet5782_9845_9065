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
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, int droneId)
        {
            Parcel temp = new Parcel() { Id = DataSource.Config.RunningParcelNumber++, SenderId = senderId, TargetId = targetId, Weight = weight, Priority = priority, Requested = DateTime.Now, DroneId = droneId, Scheduled = null, Delivered = null, PickedUp = null };
            DataSource.Parcels.Add(temp);
            return DataSource.Config.RunningParcelNumber - 1;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            if (!DataSource.Parcels.Exists(x => x.Id == id))
                throw new ParcelException("Customer to delete does not exist.");
            DataSource.Parcels.Remove(DataSource.Parcels.Find(x => x.Id == id));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ScheduleParcel(int parcelId)
        {
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)//if parcel doesn't exist
                throw new ParcelException("Parcel to schedule does not exist");
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.Scheduled = DateTime.Now;
            DataSource.Parcels[indexParcel] = tempParcel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickUpParcel(int parcelId)
        {
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)//if parcel doesn't exist
                throw new ParcelException("Parcel to pick up does not exist.");
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.PickedUp = DateTime.Now;
            DataSource.Parcels[indexParcel] = tempParcel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverToCustomer(int parcelId)
        {
            int indexParcel = DataSource.Parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)
                throw new ParcelException("Parcel to deliver does not exist.");
            Parcel tempParcel = DataSource.Parcels[indexParcel];
            tempParcel.Delivered = DateTime.Now;
            DataSource.Parcels[indexParcel] = tempParcel;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel SearchParcel(int parcelId)
        {
            if (!DataSource.Parcels.Exists(x => x.Id == parcelId))
                throw new ParcelException("Parcel does not exist.");
            return DataSource.Parcels.Find(x => x.Id == parcelId);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> YieldParcel()
        {
            return new List<Parcel>(DataSource.Parcels);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> ListParcelConditional(Predicate<DO.Parcel> predicate)
        {
            List<Parcel> noDrone = new List<Parcel>();
            foreach (Parcel parcel in DataSource.Parcels)
            {
                if (predicate(parcel))
                    noDrone.Add(parcel);
            }
            return noDrone;
        }
    }
}
