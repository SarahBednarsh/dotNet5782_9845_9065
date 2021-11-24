using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority)
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
            public Parcel createParcel(IDAL.DO.Parcel old)
            {
                Parcel parcel = new Parcel();
                parcel.Id = old.Id;
                parcel.Sender = new CustomerInParcel { Id = old.SenderId, CustomerName = SearchCustomer(old.SenderId).Name };
                parcel.Target = new CustomerInParcel { Id = old.TargetId, CustomerName = SearchCustomer(old.TargetId).Name };
                parcel.Weight = (IBL.BO.WeightCategories)old.Weight;
                parcel.Priority = (IBL.BO.WeightCategories)old.Priority;




                return parcel;
            }
            public IEnumerable<Parcel> YieldParcel()
            {

            }
            public IEnumerable<Parcel> YieldParcelNotAttributed()
            {

            }
        }
    }
}
