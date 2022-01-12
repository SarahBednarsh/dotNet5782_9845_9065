using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BO;
using DO;
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
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            if (longitude < 29.489 || longitude > 33.154 || latitude < 34.361 || latitude > 35.475)
            { throw new FormatException("The location is not in Isreal"); }
            if (chargeSlots < 1)
                throw new FormatException("Charging slots amount must be positive");
            lock (dalAP)
            {
                dalAP.AddStation(id, name, longitude, latitude, chargeSlots);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int stationId)
        {
            lock (dalAP)
            {
                Station station = SearchStation(stationId);
                if (station.Charging.Count() > 0)
                    throw new CannotDelete("There are drones charging, cannot delete");
                try
                {
                    dalAP.DeleteCustomer(stationId);
                }
                catch (StationException exception)
                {
                    throw new KeyDoesNotExist("No such station", exception);
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStationInfo(int stationId, string name, int chargingSlots)
        {
            Station station;
            try
            {
                station = SearchStation(stationId);
            }
            catch (DO.StationException exception)
            {
                throw new KeyDoesNotExist("The station requested does not exist", exception);
            }
            if (name != "")
                station.Name = name;
            if (chargingSlots != -1) //new number of charging slots was requested
            {
                if (chargingSlots - station.Charging.Count < 0) //more charging drones than requested slots
                    throw new NotEnoughChargingSlots("Not enough charging slots for drones already charging");
                station.OpenChargeSlots = chargingSlots - station.Charging.Count;
            }
            //update
            lock (dalAP)
            {
                dalAP.DeleteStation(stationId);
                dalAP.AddStation(station.Id, station.Name, DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), station.OpenChargeSlots + station.Charging.Count);
            }
        }
        private Station CreateStation(DO.Station old) //convert DO.Station to BL.Station
        {
            Station station = new Station();
            station.Id = old.Id;
            station.Location = new Location { Latitude = old.Latitude, Longitude = old.Longitude };
            station.Name = old.Name;
            station.OpenChargeSlots = old.ChargeSlots;
            lock (dalAP)
            {
                station.Charging = (from DroneCharge droneCharge in dalAP.YieldDroneCharges()
                                    where droneCharge.StationId == station.Id
                                    let drone = dronesBL.Find(x => x.Id == droneCharge.DroneId)
                                    select new DroneInCharge { Battery = drone.Battery, Id = drone.Id }).ToList();
            }
            return station;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station SearchStation(int stationId)
        {
            try
            {
                lock (dalAP)
                {
                    DO.Station station = dalAP.SearchStation(stationId);
                    Station BLstation = CreateStation(station);
                    return BLstation;
                }
            }
            catch (StationException exception)
            {
                throw new KeyDoesNotExist("Station does not exists", exception);
            }
        }
        private IEnumerable<Station> YieldStation()
        {
            lock (dalAP)
            {
                return from station in dalAP.YieldStation()
                       select CreateStation(station);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> ListStation()
        {
            lock (dalAP)
            {
                return from station in dalAP.YieldStation()
                       select new StationToList
                       {
                           Id = station.Id,
                           Name = station.Name,
                           OpenChargeSlots = station.ChargeSlots,
                           UsedChargeSlots = CreateStation(station).Charging.Count()
                       };
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> ListStationAvailable()
        {
            lock (dalAP)
            {
                return from station in dalAP.ListStationConditional(x => x.ChargeSlots > 0)
                       select new StationToList { Id = station.Id, Name = station.Name, OpenChargeSlots = station.ChargeSlots, UsedChargeSlots = SearchStation(station.Id).Charging.Count };
            }
        }
    }
}

