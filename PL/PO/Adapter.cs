using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace PL
{
    internal class Adapter
    {
        static public PL.DroneToList DroneBotoPo(BO.Drone boDrone)
        {
            return new PL.DroneToList()
            {
                Id = boDrone.Id,
                Battery = (int)boDrone.Battery,
                Latitude = boDrone.Location.Latitude.ToString(),
                Longitude = boDrone.Location.Longitude.ToString(),
                MaxWeight = (PL.WeightCategories)boDrone.MaxWeight,
                Model = boDrone.Model,
                ParcelId = boDrone.Parcel == null ? "No parcel yet" : boDrone.Parcel.Id.ToString(),
                Status = (PL.DroneStatuses)boDrone.Status
            };
        }
        static public PL.DroneInParcel DroneInParcelBotoPo(BO.DroneInParcel boDroneInParcel)
        {
            return new PL.DroneInParcel()
            {
                Id = boDroneInParcel.Id,
                Battery = (int)boDroneInParcel.Battery,
                Longitude = boDroneInParcel.Location.Longitude.ToString(),
                Latitude = boDroneInParcel.Location.Latitude.ToString()
            };
        }
        static public PL.Parcel ParcelBotoPo(BO.Parcel boParcel)
        {
            return new PL.Parcel()
            {
                Id = boParcel.Id,
                Sender = new CustomerInParcel { Id = boParcel.Sender.Id, Name = boParcel.Sender.Name },
                Target = new CustomerInParcel { Id = boParcel.Target.Id, Name = boParcel.Target.Name },
                Weight = (PL.WeightCategories)boParcel.Weight,
                Priority = (PL.Priorities)boParcel.Priority,
                DroneId = boParcel.Drone == null ? "No drone yet" : boParcel.Drone.Id.ToString(),
                Creation = boParcel.Creation,// == null ? DateTime.MinValue : BoParcel.Creation,
                Attribution = boParcel.Attribution,// == null ? DateTime.MinValue : BoParcel.Attribution,
                PickUp = boParcel.PickUp,// == null ? DateTime.MinValue : BoParcel.PickUp,
                Delivery = boParcel.Delivery// == null ? DateTime.MinValue : BoParcel.Delivery,
            };
        }
        static public PL.ParcelToList ParcelToListBotoPo(BO.ParcelToList boParcel)
        {
            return new PL.ParcelToList()
            {
                Id = boParcel.Id,
                SenderName = boParcel.SenderName,
                TargetName = boParcel.TargetName,
                Weight = (PL.WeightCategories)boParcel.Weight,
                Priority = (PL.Priorities)boParcel.Priority,
            };
        }
        static public PL.ParcelInTransfer ParcelInTransferBotoPo(BO.ParcelInTransfer boParcel)
        {
            return new PL.ParcelInTransfer()
            {
                Id = boParcel.Id,
                PickedUpAlready = boParcel.PickedUpAlready,
                Weight = (WeightCategories)boParcel.Weight,
                Priority = (Priorities)boParcel.Priority,
                Sender = Adapter.CustomerInParcelBotoPo(boParcel.Sender),
                Target = Adapter.CustomerInParcelBotoPo(boParcel.Target),
                PickUpLongitude = boParcel.PickUpLocation.Longitude.ToString(),
                PickUpLatitude = boParcel.PickUpLocation.Latitude.ToString(),
                DestinationLongitude = boParcel.Destination.Longitude.ToString(),
                DestinationLatitude = boParcel.Destination.Latitude.ToString(),
                Distance = boParcel.Distance
            };
        }
        static public PL.Customer CustomerBotoPo(BO.Customer boCustomer)
        {
            return new PL.Customer()
            {
                Id = boCustomer.Id,
                Name = boCustomer.Name,
                PhoneNum = boCustomer.PhoneNum,
                Latitude = boCustomer.Location.Latitude.ToString(),
                Longitude = boCustomer.Location.Longitude.ToString(),
                AtCustomer = (from customer in boCustomer.AtCustomer
                              select customer.Id).ToList(),
                ToCustomer = (from customer in boCustomer.ToCustomer
                              select customer.Id).ToList()
            };
        }

        static public PL.CustomerToList CustomerToListBotoPo(BO.CustomerToList boCustomer)
        {
            return new PL.CustomerToList()
            {
                Id = boCustomer.Id,
                Name = boCustomer.Name,
                PhoneNum = boCustomer.PhoneNum,
                Delivered = boCustomer.Delivered,
                Sent = boCustomer.Sent,
                Got = boCustomer.Got,
                OnTheirWay = boCustomer.OnTheirWay
            };
        }
        static public PL.CustomerInParcel CustomerInParcelBotoPo(BO.CustomerInParcel boCustomer)
        {
            return new PL.CustomerInParcel()
            {
                Id = boCustomer.Id,
                Name = boCustomer.Name
            };
        }
        static public PL.Station StationBotoPo(BO.Station BoStation)
        {
            return new PL.Station()
            {
                Id = BoStation.Id,
                Name = BoStation.Name,
                Latitude = BoStation.Location.Latitude.ToString(),
                Longitude = BoStation.Location.Longitude.ToString(),
                OpenChargeSlots = BoStation.OpenChargeSlots,
                Charging = (from drone in BoStation.Charging
                            select drone.Id).ToList()
            };

        }
        static public PL.StationToList StationToListBotoPo(BO.StationToList boStation)
        {
            return new PL.StationToList()
            {
                Id = boStation.Id,
                Name = boStation.Name,
                OpenChargeSlots = boStation.OpenChargeSlots,
                UsedChargeSlots = boStation.UsedChargeSlots
            };
        }
    }
}
