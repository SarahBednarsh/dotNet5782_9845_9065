﻿using System;
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
                    dalAP.AddParcel(senderId, targetId, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)priority, -1);
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
            private Parcel CreateParcel(IDAL.DO.Parcel old) //convert IDAL.DO.Parcel to BL.Parcel
            {
                Parcel parcel = new Parcel();
                parcel.Id = old.Id;
                parcel.Sender = new CustomerInParcel { Id = old.SenderId, Name = SearchCustomer(old.SenderId).Name };
                parcel.Target = new CustomerInParcel { Id = old.TargetId, Name = SearchCustomer(old.TargetId).Name };
                parcel.Weight = (WeightCategories)old.Weight;
                parcel.Priority = (Priorities)old.Priority;
                Drone drone = null;
                if(old.Id != -1) //is parcel was already attributed
                    drone = SearchDrone(old.DroneId);
                parcel.Drone = new DroneInParcel { Battery = drone.Battery, Id = drone.Id, Location = drone.Location };
                parcel.Creation = old.Requested;
                parcel.Attribution = old.Scheduled;
                parcel.PickUp = old.PickedUp;
                parcel.Delivery = old.Delivered;
                return parcel;
            }
            private Location GetSenderLocation(Parcel p)
            {
                Customer sender = SearchCustomer(p.Sender.Id);
                return sender.Location;
            }
            private Location GetTargetLocation(Parcel p)
            {
                Customer target = SearchCustomer(p.Target.Id);
                return target.Location;
            }
            private ParcelInTransfer CreateParcelInTransfer(int parcelId) //create parcel to be put in drone
            {
                Parcel parcel = SearchParcel(parcelId);
                return new ParcelInTransfer { Id = parcel.Id, PickedUpAlready = parcel.PickUp > DateTime.Now, Priority = parcel.Priority,
                    Weight = parcel.Weight, Sender = parcel.Sender, Target = parcel.Target, PickUpLocation = SearchCustomer(parcel.Sender.Id).Location,
                    Destination = SearchCustomer(parcel.Target.Id).Location,
                    Distance = LocationStaticClass.CalcDis(SearchCustomer(parcel.Sender.Id).Location, SearchCustomer(parcel.Target.Id).Location) };
            }
            private IEnumerable<Parcel> YieldParcel() //create list of BL.Parcel
            {
                IEnumerable<IDAL.DO.Parcel> parcels = dalAP.YieldParcel();
                foreach (IDAL.DO.Parcel parcel in parcels)
                {
                    yield return CreateParcel(parcel);
                }
            }
            public IEnumerable<ParcelToList> ListParcel()
            {
                foreach (Parcel parcel in YieldParcel())
                    yield return new ParcelToList { Id = parcel.Id, Priority = parcel.Priority, SenderName = parcel.Sender.Name, TargetName = parcel.Target.Name, Weight = parcel.Weight };
            }
            public IEnumerable<ParcelToList> ListParcelNotAttributed()
            {
                return from parcel in YieldParcel()
                       where parcel.Attribution == DateTime.MinValue //parcel was not attributed yet
                       select new ParcelToList { Id = parcel.Id, Priority = parcel.Priority, SenderName = parcel.Sender.Name, TargetName=parcel.Target.Name, Weight=parcel.Weight };
            }
        }
    }
}
