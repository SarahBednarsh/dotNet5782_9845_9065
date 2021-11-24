using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority)
            {
                try
                {
                    dalAP.AddParcel(senderId, targetId, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)priority, DateTime.MinValue,???);
                }
                catch (ParcelException exception)
                {
                    throw new KeyAlreadyExists("Parcel alreday exists", exception);
                }
            }
            
            public Parcel SearchParcel(int parcelId)
            {
                try
                {
                    IDAL.DO.Parcel parcel = dalAP.SearchParcel(parcelId);
                    //if equals default exception
                    Parcel BLparcel = CreateParcel(parcel);
                    return BLparcel;
                }
                catch (ParcelException exception)
                {
                    throw new KeyDoesNotExist("Parcel does not exists", exception);
                }
            }
            private Parcel CreateParcel(IDAL.DO.Parcel old)
            {
                Parcel parcel = new Parcel();
                parcel.Id = old.Id;
                parcel.Sender = new CustomerInParcel { Id = old.SenderId, CustomerName = SearchCustomer(old.SenderId).Name };
                parcel.Target = new CustomerInParcel { Id = old.TargetId, CustomerName = SearchCustomer(old.TargetId).Name };
                parcel.Weight = (WeightCategories)old.Weight;
                parcel.Priority = (Priorities)old.Priority;
                Drone drone = SearchDrone(old.DroneId);
                parcel.Drone = new DroneInParcel { Battery = drone.Battery, Id = drone.Id, Location = drone.Location };
                parcel.Creation = DateTime.Now;
                parcel.Attribution = DateTime.MinValue;
                parcel.PickUp = DateTime.MinValue;
                parcel.Delivery = DateTime.MinValue;
                return parcel;
            }
            public IEnumerable<Parcel> YieldParcel()
            {
                IEnumerable<IDAL.DO.Parcel> parcels = dalAP.YieldParcel();
                List<Parcel> newParcels = new List<Parcel>();
                foreach (IDAL.DO.Parcel parcel in parcels)
                {
                    newParcels.Add(CreateParcel(parcel));
                }
                return newParcels;
            }
            public IEnumerable<ParcelToList> YieldParcelNotAttributed()
            {
                return from parcel in YieldParcel()
                       where parcel.Attribution < DateTime.Now
                       select new ParcelToList { Id = parcel.Id, Priority = parcel.Priority, Sender = parcel.Sender.CustomerName };
            }
        }
    }
}
