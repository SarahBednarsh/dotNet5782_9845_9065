using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority)
            {
                dalAP.AddParcel(senderId, targetId, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)priority,DateTime.MinValue,???);
            }
            public void DeliverAParcel(int droneId)
            {
                
            }
            public Parcel SearchParcel(int parcelId)
            {
                IDAL.DO.Parcel parcel = dalAP.SearchParcel(parcelId);
                //if equals default exception
                Parcel BLparcel = createParcel(parcel);
                return BLparcel;
            }
            private Parcel createParcel(IDAL.DO.Parcel old)
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
                    newParcels.Add(createParcel(parcel));
                }
                return newParcels;
            }
            public IEnumerable<Parcel> YieldParcelNotAttributed()
            {
                IEnumerable<IDAL.DO.Parcel> parcels = dalAP.YieldParcel();
                List<Parcel> newParcels = new List<Parcel>();
                Parcel tmpParcel;
                foreach (IDAL.DO.Parcel parcel in parcels)
                {
                    tmpParcel = createParcel(parcel);
                    if (tmpParcel.Attribution<=DateTime.Now)
                    newParcels.Add(tmpParcel);
                }
                return newParcels;

            }
            public string printParcel(int parcelId)
            {
                Parcel parcel = SearchParcel(parcelId);
                return parcel.ToString();
            }
        }
    }
}
