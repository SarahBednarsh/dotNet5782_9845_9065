using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using PO;
using CustomerInParcel = PO.CustomerInParcel;
using Priorities = PO.Priorities;
using States = PO.States;
using User = PO.User;
using WeightCategories = PO.WeightCategories;

namespace PL
{
    internal class Adapter
    {
        public static PO.Drone DroneBotoPo(BO.Drone boDrone)
        {
            if (boDrone == null)
                return null;
            return new PO.Drone()
            {
                Id = boDrone.Id,
                Battery = (int)boDrone.Battery,
                Latitude = boDrone.Location.Latitude.ToString(),
                Longitude = boDrone.Location.Longitude.ToString(),
                MaxWeight = (PO.WeightCategories)boDrone.MaxWeight,
                Model = boDrone.Model,
                Parcel = Adapter.ParcelInTransferBotoPo(boDrone.Parcel),
                Status = (PO.DroneStatuses)boDrone.Status
            };
        }
        public static PO.DroneToList DroneToListBotoPo(BO.DroneToList boDrone)
        {
            if (boDrone == null)
                return null;
            return new PO.DroneToList()
            {
                Id = boDrone.Id,
                Battery = (int)boDrone.Battery,
                Latitude = boDrone.Location.Latitude.ToString(),
                Longitude = boDrone.Location.Longitude.ToString(),
                MaxWeight = (PO.WeightCategories)boDrone.MaxWeight,
                Model = boDrone.Model,
                ParcelId = boDrone.IdOfParcel == -1 ? "No parcel yet" : boDrone.IdOfParcel.ToString(),
                Status = (PO.DroneStatuses)boDrone.Status
            };
        }
        public static PO.DroneInParcel DroneInParcelBotoPo(BO.DroneInParcel boDroneInParcel)
        {
            if (boDroneInParcel == null)
                return new PO.DroneInParcel() { Id = "No drone yet" };
            return new PO.DroneInParcel()
            {
                Id =/* boDroneInParcel == null ? "No drone yet" : */boDroneInParcel.Id.ToString(),
                Battery = (int)boDroneInParcel.Battery,
                Longitude = boDroneInParcel.Location.Longitude.ToString(),
                Latitude = boDroneInParcel.Location.Latitude.ToString()
            };
        }
        public static PO.DroneInCharge DroneInChargeBotoPo(BO.DroneInCharge boDrone)
        {
            if (boDrone == null)
                return null;
            return new PO.DroneInCharge()
            {
                Id = boDrone.Id,
                Battery = (int)boDrone.Battery
            };
        }
        public static PO.Parcel ParcelBotoPo(BO.Parcel boParcel)
        {
            if (boParcel == null)
                return null;
            return new PO.Parcel()
            {
                Id = boParcel.Id,
                Sender = new CustomerInParcel { Id = boParcel.Sender.Id, Name = boParcel.Sender.Name },
                Target = new CustomerInParcel { Id = boParcel.Target.Id, Name = boParcel.Target.Name },
                Weight = (PO.WeightCategories)boParcel.Weight,
                Priority = (PO.Priorities)boParcel.Priority,
                Drone = Adapter.DroneInParcelBotoPo(boParcel.Drone),
                Creation = boParcel.Creation,
                Attribution = boParcel.Attribution,
                PickUp = boParcel.PickUp,
                Delivery = boParcel.Delivery
            };
        }
        static public PO.ParcelToList ParcelToListBotoPo(BO.ParcelToList boParcel)
        {
            if (boParcel == null)
                return null;
            return new PO.ParcelToList()
            {
                Id = boParcel.Id,
                SenderName = boParcel.SenderName,
                TargetName = boParcel.TargetName,
                Weight = (PO.WeightCategories)boParcel.Weight,
                Priority = (PO.Priorities)boParcel.Priority,
            };
        }
        static public PO.ParcelInTransfer ParcelInTransferBotoPo(BO.ParcelInTransfer boParcel)
        {
            if (boParcel == null)
                return null;
            return new PO.ParcelInTransfer()
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
        static public PO.ParcelAtCustomer ParcelAtCustomerBotoPo(BO.ParcelAtCustomer boParcel)
        {
            if (boParcel == null)
                return null;
            return new PO.ParcelAtCustomer()
            {
                Id = boParcel.Id,
                Weight = (WeightCategories)boParcel.Weight,
                Priority = (Priorities)boParcel.Priority,
                State = (States)boParcel.State,
                Customer = Adapter.CustomerInParcelBotoPo(boParcel.Customer)
            };
        }
        static public PO.Customer CustomerBotoPo(BO.Customer boCustomer)
        {
            if (boCustomer == null)
                return null;
            return new PO.Customer()
            {
                Id = boCustomer.Id,
                Name = boCustomer.Name,
                PhoneNum = boCustomer.PhoneNum,
                Latitude = boCustomer.Location.Latitude.ToString(),
                Longitude = boCustomer.Location.Longitude.ToString(),
                AtCustomer = (from parcel in boCustomer.AtCustomer
                              select Adapter.ParcelAtCustomerBotoPo(parcel)).ToList(),
                ToCustomer = (from parcel in boCustomer.ToCustomer
                              select Adapter.ParcelAtCustomerBotoPo(parcel)).ToList()
            };
        }

        static public PO.CustomerToList CustomerToListBotoPo(BO.CustomerToList boCustomer)
        {
            if (boCustomer == null)
                return null;
            return new PO.CustomerToList()
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
        static public PO.CustomerInParcel CustomerInParcelBotoPo(BO.CustomerInParcel boCustomer)
        {
            if (boCustomer == null)
                return null;
            return new PO.CustomerInParcel()
            {
                Id = boCustomer.Id,
                Name = boCustomer.Name
            };
        }
        public static PO.Station StationBotoPo(BO.Station boStation)
        {
            if (boStation == null)
                return null;
            return new PO.Station()
            {
                Id = boStation.Id,
                Name = boStation.Name,
                Latitude = boStation.Location.Latitude.ToString(),
                Longitude = boStation.Location.Longitude.ToString(),
                OpenChargeSlots = boStation.OpenChargeSlots,
                Charging = (from drone in boStation.Charging
                            select DroneInChargeBotoPo(drone)).ToList()
            };

        }
        public static PO.StationToList StationToListBotoPo(BO.StationToList boStation)
        {
            if (boStation == null)
                return null;
            return new PO.StationToList()
            {
                Id = boStation.Id,
                Name = boStation.Name,
                OpenChargeSlots = boStation.OpenChargeSlots,
                UsedChargeSlots = boStation.UsedChargeSlots
            };
        }
        public static PO.User UserBotoPo(BO.User boUser)
        {
            if (boUser == null)
                return null;
            return new User()
            {
                Id = boUser.Id,
                UserName = boUser.UserName,
                Email = boUser.Email,
                Photo = boUser.Photo,
                Salt = boUser.Salt,
                HashedPassword = boUser.HashedPassword,
                IsManager = boUser.IsManager
            };
        }
    }
}
