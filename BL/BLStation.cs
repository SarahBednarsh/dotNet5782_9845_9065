using System;
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
            public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
            {
                dalAP.AddStation(id, name, longitude, latitude, chargeSlots);
            }
            public void UpdateStationInfo(int stationId, int name, int chargingSlots)
            {
                Station station;
                try
                {
                    station = SearchStation(stationId);
                }
                catch (IDAL.DO.StationException exception)
                {
                    throw new KeyDoesNotExist("The station requested does not exist", exception);
                }
                if (name != -1)
                    station.Name = name;
                if (chargingSlots != -1) //new number of charging slots was requested
                {
                    if (chargingSlots - station.Charging.Count < 0) //more charging drones than requested slots
                        throw new NotEnoughChargingSlots("Not enough charging slots for drones already charging");
                    station.OpenChargeSlots = chargingSlots - station.Charging.Count;
                }
                //update
                dalAP.DeleteStation(stationId);
                dalAP.AddStation(station.Id, station.Name, IDAL.DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), IDAL.DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), station.OpenChargeSlots + station.Charging.Count);
            }
            private Station CreateStation(IDAL.DO.Station old) //convert IDAL.DO>Station to BL.Station
            {
                Station station = new Station();
                station.Id = old.Id;
                station.Location = new Location { Latitude = old.Latitude, Longitude = old.Latitude };
                station.Name = old.Name;
                station.OpenChargeSlots = old.ChargeSlots;
                foreach (DroneToList drone in dronesBL) //make list of charging drones
                {
                    if (drone.Status == DroneStatuses.InMaintenance && drone.Location == station.Location)
                        station.Charging.Add(new DroneInCharge { Battery = drone.Battery, Id = drone.Id });
                }
                return station;
            }
            public Station SearchStation(int stationId)
            {
                try
                {
                    IDAL.DO.Station station = dalAP.SearchStation(stationId);
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
                IEnumerable<IDAL.DO.Station> stations = dalAP.YieldStation();
                foreach (IDAL.DO.Station station in stations)
                {
                   yield return CreateStation(station);
                }
            }
            public IEnumerable<StationToList> ListStation()
            {
                IEnumerable<IDAL.DO.Station> stations = dalAP.YieldStation();
                foreach (IDAL.DO.Station station in stations)
                {
                    yield return new StationToList { Id = station.Id, Name = station.Name, OpenChargeSlots = station.ChargeSlots, UsedChargeSlots = CreateStation(station).Charging.Count() };
                }
            }
            public IEnumerable<StationToList> ListStationAvailable()
            {
                return from station in YieldStation()
                       where station.OpenChargeSlots > 0 //station is available
                       select new StationToList { Id = station.Id, Name = station.Name, OpenChargeSlots = station.OpenChargeSlots, UsedChargeSlots = station.Charging.Count };
            }
        }
    }
}
