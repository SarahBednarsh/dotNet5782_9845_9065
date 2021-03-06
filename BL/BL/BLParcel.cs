using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
using BO;
using Parcel = BO.Parcel;
using Priorities = BO.Priorities;
using WeightCategories = BO.WeightCategories;
using Drone = BO.Drone;
using Station = BO.Station;
using Customer = BO.Customer;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority)
        {
            try
            {
                lock (dalAP)
                {
                    dalAP.SearchCustomer(senderId);
                    dalAP.SearchCustomer(targetId);
                    dalAP.AddParcel(senderId, targetId, (DO.WeightCategories)weight, (DO.Priorities)priority, -1);
                }
            }
            catch (ParcelException exception)
            {
                throw new KeyAlreadyExists("Parcel alreday exists", exception);
            }
            catch (CustomerException exception)
            {
                throw new KeyDoesNotExist("Sender or target don't exist", exception);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int parcelId)
        {

            IEnumerable<DroneToList> carryingParcel = ListDrone().Where(x => x.IdOfParcel == parcelId);
            if (carryingParcel.Count() > 0)
                throw new CannotDelete("Parcel is alreday on the way");
            try
            {
                lock (dalAP)
                {
                    dalAP.DeleteParcel(parcelId);
                }
            }
            catch (ParcelException exception)
            {
                throw new KeyDoesNotExist("No such parcel", exception);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel SearchParcel(int parcelId)
        {
            try
            {
                lock (dalAP)
                {
                    DO.Parcel parcel = dalAP.SearchParcel(parcelId);
                    //if equals default exception
                    Parcel BLparcel = CreateParcel(parcel);
                    return BLparcel;
                }
            }
            catch (ParcelException exception)
            {
                throw new KeyDoesNotExist("Parcel does not exists", exception);
            }
        }
        private Parcel CreateParcel(DO.Parcel old) //convert DO.Parcel to BL.Parcel
        {
            Parcel parcel = new Parcel();
            parcel.Id = old.Id;
            lock (dalAP)
            {
                parcel.Sender = new CustomerInParcel { Id = old.SenderId, Name = dalAP.SearchCustomer(old.SenderId).Name };
                parcel.Target = new CustomerInParcel { Id = old.TargetId, Name = dalAP.SearchCustomer(old.TargetId).Name };
            }
            parcel.Weight = (WeightCategories)old.Weight;
            parcel.Priority = (Priorities)old.Priority;
            Drone drone = null;
            if (old.DroneId != -1) //if parcel was already attributed
            {
                drone = SearchDrone(old.DroneId);
                parcel.Drone = new DroneInParcel { Battery = drone.Battery, Id = drone.Id, Location = drone.Location };
            }
            parcel.Creation = old.Requested;
            parcel.Attribution = old.Scheduled;
            parcel.PickUp = old.PickedUp;
            parcel.Delivery = old.Delivered;
            return parcel;
        }
        internal Location GetSenderLocation(Parcel p)
        {
            Customer sender = SearchCustomer(p.Sender.Id);
            return sender.Location;
        }
        internal Location GetTargetLocation(Parcel p)
        {
            Customer target = SearchCustomer(p.Target.Id);
            return target.Location;
        }
        private ParcelInTransfer CreateParcelInTransfer(int parcelId) //create parcel to be put in drone
        {
            lock (dalAP)
            {
                DO.Parcel parcel = dalAP.SearchParcel(parcelId);

                double distance = 0;
                if (parcel.PickedUp == null)
                    distance = LocationStaticClass.CalcDis(LocationStaticClass.InitializeLocation(dalAP.SearchCustomer(parcel.SenderId).Longitude, dalAP.SearchCustomer(parcel.SenderId).Latitude), LocationStaticClass.InitializeLocation(dalAP.SearchCustomer(parcel.TargetId).Longitude, dalAP.SearchCustomer(parcel.TargetId).Latitude));
                else
                {
                    Location target = LocationStaticClass.InitializeLocation(dalAP.SearchCustomer(parcel.TargetId).Longitude, dalAP.SearchCustomer(parcel.TargetId).Latitude);
                    Location current = (from drone in dronesBL
                                        where drone.IdOfParcel == parcelId
                                        select drone).FirstOrDefault().Location;
                    distance = LocationStaticClass.CalcDis(current, target);
                }
                return new ParcelInTransfer
                {
                    Id = parcel.Id,
                    PickedUpAlready = parcel.PickedUp != null,
                    Priority = (Priorities)parcel.Priority,
                    Weight = (WeightCategories)parcel.Weight,
                    Sender = new CustomerInParcel { Id = parcel.SenderId, Name = dalAP.SearchCustomer(parcel.SenderId).Name },
                    Target = new CustomerInParcel { Id = parcel.TargetId, Name = dalAP.SearchCustomer(parcel.TargetId).Name },
                    PickUpLocation = LocationStaticClass.InitializeLocation(dalAP.SearchCustomer(parcel.SenderId).Longitude, dalAP.SearchCustomer(parcel.SenderId).Latitude),
                    Destination = LocationStaticClass.InitializeLocation(dalAP.SearchCustomer(parcel.TargetId).Longitude, dalAP.SearchCustomer(parcel.TargetId).Latitude),
                    Distance = distance
                };
            }
        }
        private IEnumerable<Parcel> YieldParcel() //create list of BL.Parcel
        {
            lock (dalAP)
            {
                return from parcel in dalAP.YieldParcel()
                       select CreateParcel(parcel);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ListParcel()
        {
            foreach (Parcel parcel in YieldParcel())
                yield return new ParcelToList { Id = parcel.Id, Priority = parcel.Priority, SenderName = parcel.Sender.Name, TargetName = parcel.Target.Name, Weight = parcel.Weight };
        }
        public IEnumerable<ParcelToList> ListParcelCreatedInTimeRange(DateTime begin, DateTime end)
        {
            lock (dalAP)
            {
                return from parcel in dalAP.ListParcelConditional(x => x.Requested >= begin && x.Requested < end)
                       select new ParcelToList { Id = parcel.Id, Priority = (Priorities)parcel.Priority, SenderName = SearchCustomer(parcel.SenderId).Name, TargetName = SearchCustomer(parcel.TargetId).Name, Weight = (WeightCategories)parcel.Weight };
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ListParcelFromCustomer(int customerId)
        {
            lock (dalAP)
            {
                return from parcel in dalAP.ListParcelConditional(x => x.SenderId == customerId)
                       select new ParcelToList { Id = parcel.Id, Priority = (Priorities)parcel.Priority, SenderName = SearchCustomer(parcel.SenderId).Name, TargetName = SearchCustomer(parcel.TargetId).Name, Weight = (WeightCategories)parcel.Weight };
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ListParcelNotAttributed()
        {
            lock (dalAP)
            {
                return from parcel in dalAP.ListParcelConditional(x => x.Scheduled == null)
                       select new ParcelToList { Id = parcel.Id, Priority = (Priorities)parcel.Priority, SenderName = SearchCustomer(parcel.SenderId).Name, TargetName = SearchCustomer(parcel.TargetId).Name, Weight = (WeightCategories)parcel.Weight };
            }
        }
    }
}
