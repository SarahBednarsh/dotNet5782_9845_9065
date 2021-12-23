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
        static public PL.Drone DroneBotoPo(BO.Drone BoDrone)
        {
            PL.Drone PoDrone = new PL.Drone()
            {
                Id = BoDrone.Id,
                Battery = (int)BoDrone.Battery,
                Latitude = BoDrone.Location.Latitude.ToString(),
                Longitude = BoDrone.Location.Longitude.ToString(),
                MaxWeight = (PL.WeightCategories)BoDrone.MaxWeight,
                Model = BoDrone.Model,
                DroneParcelId = BoDrone.Parcel == null ? "No parcel yet" : BoDrone.Parcel.Id.ToString(),
                Status = (PL.DroneStatuses)BoDrone.Status
            };
            return PoDrone;
        }
        static public PL.Parcel ParcelBotoPo(BO.Parcel BoParcel)
        {
            PL.Parcel PoParcel = new PL.Parcel()
            {
                ParcelId = BoParcel.Id,
                ParcelSender = new CustomerInParcel { Id=BoParcel.Sender.Id, Name = BoParcel.Sender.Name },
                ParcelTarget = new CustomerInParcel { Id = BoParcel.Target.Id, Name = BoParcel.Target.Name },
                ParcelWeight = (PL.WeightCategories)BoParcel.Weight,
                ParcelPriority = (PL.Priorities)BoParcel.Priority,
                ParcelDroneId = BoParcel.Drone==null? "No drone yet": BoParcel.Drone.Id.ToString(),
                Creation = BoParcel.Creation,// == null ? DateTime.MinValue : BoParcel.Creation,
                Attribution = BoParcel.Attribution,// == null ? DateTime.MinValue : BoParcel.Attribution,
                PickUp = BoParcel.PickUp,// == null ? DateTime.MinValue : BoParcel.PickUp,
                Delivery = BoParcel.Delivery// == null ? DateTime.MinValue : BoParcel.Delivery,
            };
            return PoParcel;
        }
        static public PL.ParcelToList ParcelToListBotoPo(BO.ParcelToList BoParcel)
        {
            return new PL.ParcelToList()
            {
                PTLId = BoParcel.Id,
                PTLSenderName = BoParcel.SenderName,
                PTLTargetName = BoParcel.TargetName,
                PTLWeight = (PL.WeightCategories)BoParcel.Weight,
                PTLPriority = (PL.Priorities)BoParcel.Priority,
            };
        }
        static public PL.Customer CustomerBotoPo(BO.Customer BoCustomer)
        {
            PL.Customer PoCustomer = new PL.Customer()
            {
                CustomerId = BoCustomer.Id,
                CustomerName = BoCustomer.Name,
                CustomerPhoneNum = BoCustomer.PhoneNum,
                CustomerLatitude = BoCustomer.Location.Latitude.ToString(),
                CustomerLongitude = BoCustomer.Location.Longitude.ToString(),
                AtCustomer = (from customer in BoCustomer.AtCustomer
                              select customer.Id).ToList(),
                ToCustomer = (from customer in BoCustomer.ToCustomer
                              select customer.Id).ToList()
            };
            return PoCustomer;


        }
        static public PL.Station StationBotoPo(BO.Station BoStation)
        {
            PL.Station PoStation = new PL.Station()
            {
                StationId = BoStation.Id,
                StationName=BoStation.Name,
                StationLatitude=BoStation.Location.Latitude.ToString(),
                StationLongitude=BoStation.Location.Longitude.ToString(),
                OpenChargeSlots=BoStation.OpenChargeSlots,
                Charging=(from drone in BoStation.Charging
                          select drone.Id).ToList()
            };
            return PoStation;


        }
    }
}
