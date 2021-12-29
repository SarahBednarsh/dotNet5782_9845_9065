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
        public static PL.Drone DroneBotoPo(BO.Drone boDrone)
        {
            if (boDrone == null)
                return null;
            return new PL.Drone()
            {
                Id = boDrone.Id,
                Battery = (int)boDrone.Battery,
                Latitude = boDrone.Location.Latitude.ToString(),
                Longitude = boDrone.Location.Longitude.ToString(),
                MaxWeight = (PL.WeightCategories)boDrone.MaxWeight,
                Model = boDrone.Model,
                Parcel = Adapter.ParcelInTransferBotoPo(boDrone.Parcel),
                Status = (PL.DroneStatuses)boDrone.Status
            };
        }
        public static PL.DroneToList DroneToListBotoPo(BO.DroneToList boDrone)
        {
            if (boDrone == null)
                return null;
            return new PL.DroneToList()
            {
                Id = boDrone.Id,
                Battery = (int)boDrone.Battery,
                Latitude = boDrone.Location.Latitude.ToString(),
                Longitude = boDrone.Location.Longitude.ToString(),
                MaxWeight = (PL.WeightCategories)boDrone.MaxWeight,
                Model = boDrone.Model,
                ParcelId = boDrone.IdOfParcel == -1 ? "No parcel yet" : boDrone.IdOfParcel.ToString(),
                Status = (PL.DroneStatuses)boDrone.Status
            };
        }
        public static PL.DroneInParcel DroneInParcelBotoPo(BO.DroneInParcel boDroneInParcel)
        {
            if (boDroneInParcel == null)
                return new PL.DroneInParcel() { Id = "No drone yet" };
            return new PL.DroneInParcel()
            {
                Id =/* boDroneInParcel == null ? "No drone yet" : */boDroneInParcel.Id.ToString(),
                Battery = (int)boDroneInParcel.Battery,
                Longitude = boDroneInParcel.Location.Longitude.ToString(),
                Latitude = boDroneInParcel.Location.Latitude.ToString()
            };
        }
        public static PL.DroneInCharge DroneInChargeBotoPo(BO.DroneInCharge boDrone)
        {
            if (boDrone == null)
                return null;
            return new PL.DroneInCharge()
            {
                Id = boDrone.Id,
                Battery = (int)boDrone.Battery
            };
        }
        public static PL.Parcel ParcelBotoPo(BO.Parcel boParcel)
        {
            if (boParcel == null)
                return null;
            return new PL.Parcel()
            {
                Id = boParcel.Id,
                Sender = new CustomerInParcel { Id = boParcel.Sender.Id, Name = boParcel.Sender.Name },
                Target = new CustomerInParcel { Id = boParcel.Target.Id, Name = boParcel.Target.Name },
                Weight = (PL.WeightCategories)boParcel.Weight,
                Priority = (PL.Priorities)boParcel.Priority,
                Drone = Adapter.DroneInParcelBotoPo(boParcel.Drone),
                Creation = boParcel.Creation,// == null ? DateTime.MinValue : BoParcel.Creation,
                Attribution = boParcel.Attribution,// == null ? DateTime.MinValue : BoParcel.Attribution,
                PickUp = boParcel.PickUp,// == null ? DateTime.MinValue : BoParcel.PickUp,
                Delivery = boParcel.Delivery// == null ? DateTime.MinValue : BoParcel.Delivery,
            };
        }
        static public PL.ParcelToList ParcelToListBotoPo(BO.ParcelToList boParcel)
        {
            if (boParcel == null)
                return null;
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
            if (boParcel == null)
                return null;
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
        static public PL.ParcelAtCustomer ParcelAtCustomerBotoPo(BO.ParcelAtCustomer boParcel)
        {
            if (boParcel == null)
                return null;
            return new PL.ParcelAtCustomer()
            {
                Id = boParcel.Id,
                Weight = (WeightCategories)boParcel.Weight,
                Priority = (Priorities)boParcel.Priority,
                State = (States)boParcel.State,
                Customer = Adapter.CustomerInParcelBotoPo(boParcel.Customer)
            };
        }
        static public PL.Customer CustomerBotoPo(BO.Customer boCustomer)
        {
            if (boCustomer == null)
                return null;
            return new PL.Customer()
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

        static public PL.CustomerToList CustomerToListBotoPo(BO.CustomerToList boCustomer)
        {
            if (boCustomer == null)
                return null;
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
            if (boCustomer == null)
                return null;
            return new PL.CustomerInParcel()
            {
                Id = boCustomer.Id,
                Name = boCustomer.Name
            };
        }
        public static PL.Station StationBotoPo(BO.Station boStation)
        {
            if (boStation == null)
                return null;
            return new PL.Station()
            {
                Id = boStation.Id,
                Name = boStation.Name,
                Latitude = boStation.Location.Latitude.ToString(),
                Longitude = boStation.Location.Longitude.ToString(),
                OpenChargeSlots = boStation.OpenChargeSlots,
                Charging = (from drone in boStation.Charging
                            select drone.Id).ToList()
            };

        }
        public static PL.StationToList StationToListBotoPo(BO.StationToList boStation)
        {
            if (boStation == null)
                return null;
            return new PL.StationToList()
            {
                Id = boStation.Id,
                Name = boStation.Name,
                OpenChargeSlots = boStation.OpenChargeSlots,
                UsedChargeSlots = boStation.UsedChargeSlots
            };
        }
        public static PL.User UserBotoPo(BO.User boUser)
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
