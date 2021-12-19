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
                Battery = BoDrone.Battery,
                Latitude = BoDrone.Location.Latitude.ToString(),
                Longitude = BoDrone.Location.Longitude.ToString(),
                MaxWeight = (PL.WeightCategories)BoDrone.MaxWeight,
                Model = BoDrone.Model,
                ParcelId = BoDrone.Parcel == null ? "No parcel yet" : BoDrone.Parcel.Id.ToString(),
                Status = (PL.DroneStatuses)BoDrone.Status
            };
            return PoDrone;


        }
        static public PL.Parcel ParcelBotoPo(BO.Parcel BoParcel)
        {
            PL.Parcel PoParcel = new PL.Parcel()
            {
                Id = BoParcel.Id,
                SenderId = BoParcel.Sender.Id,
                TargetId = BoParcel.Target.Id,
                Weight = (PL.WeightCategories)BoParcel.Weight,
                Priority = (PL.Priorities)BoParcel.Priority,
                DroneId = BoParcel.Drone.Id,
                Creation = BoParcel.Creation,
                Attribution = BoParcel.Attribution,
                PickUP = BoParcel.PickUp,
                Delivery = BoParcel.Delivery,
            };
            return PoParcel;


        }
        static public PL.Customer CustomerBotoPo(BO.Customer BoCustomer)
        {
            PL.Customer PoCustomer = new PL.Customer()
            {
                Id = BoCustomer.Id,
                Name = BoCustomer.Name,
                PhoneNum = BoCustomer.PhoneNum,
                Latitude = BoCustomer.Location.Latitude.ToString(),
                Longitude = BoCustomer.Location.Longitude.ToString(),
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
                Id = BoStation.Id,
                Name=BoStation.Name,
                Latitude=BoStation.Location.Latitude.ToString(),
                Longitude=BoStation.Location.Longitude.ToString(),
                Charging=(from drone in BoStation.Charging
                          select drone.Id).ToList()
            };
            return PoStation;


        }
    }
}
