using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BO;
using DO;

namespace BL
{
    internal partial class BL
    {
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            dalAP.AddStation(id, name, longitude, latitude, chargeSlots);
        }
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
            dalAP.DeleteStation(stationId);
            dalAP.AddStation(station.Id, station.Name, DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), station.OpenChargeSlots + station.Charging.Count);
        }
        private Station CreateStation(DO.Station old) //convert DO.Station to BL.Station
        {
            Station station = new Station();
            station.Id = old.Id;
            station.Location = new Location { Latitude = old.Latitude, Longitude = old.Latitude };
            station.Name = old.Name;
            station.OpenChargeSlots = old.ChargeSlots;
            station.Charging = new List<DroneInCharge>();
            foreach (DroneToList drone in dronesBL) //make list of charging drones
            {
                //if the drone is charging in the station
                if (drone.Status == DroneStatuses.InMaintenance && drone.Location == station.Location)
                    station.Charging.Add(new DroneInCharge { Battery = drone.Battery, Id = drone.Id });
            }
            return station;
        }
        public Station SearchStation(int stationId)
        {
            try
            {
                DO.Station station = dalAP.SearchStation(stationId);
                Station BLstation = CreateStation(station);
                return BLstation;
            }
            catch (StationException exception)
            {
                throw new KeyDoesNotExist("Station does not exists", exception);
            }
        }
        private IEnumerable<Station> YieldStation()
        {
            IEnumerable<DO.Station> stations = dalAP.YieldStation();
            foreach (DO.Station station in stations)
            {
                yield return CreateStation(station);
            }
        }
        public IEnumerable<StationToList> ListStation()
        {
            IEnumerable<DO.Station> stations = dalAP.YieldStation();
            foreach (DO.Station station in stations)
            {
                yield return new StationToList
                {
                    Id = station.Id,
                    Name = station.Name,
                    OpenChargeSlots = station.ChargeSlots,
                    UsedChargeSlots = CreateStation(station).Charging.Count()
                };
            }
        }
        //public IEnumerable<StationToList> ListStationAvailable()
        //{
        //    return from station in YieldStation()
        //           where station.OpenChargeSlots > 0 //station is available
        //           select new StationToList { Id = station.Id, Name = station.Name, OpenChargeSlots = station.OpenChargeSlots, UsedChargeSlots = station.Charging.Count };
        //}
        public IEnumerable<StationToList> ListStationAvailable()
        {
            return from station in dalAP.ListStationConditional(x => x.ChargeSlots > 0)
                   select new StationToList { Id = station.Id, Name = station.Name, OpenChargeSlots = station.ChargeSlots, UsedChargeSlots = SearchStation(station.Id).Charging.Count };
        }
        public IEnumerable<StationToList> ListStationConditional(Predicate<StationToList> predicate)
        {
            return from station in ListStation()
                   where predicate(station)
                   select station;
        }
    }
}

