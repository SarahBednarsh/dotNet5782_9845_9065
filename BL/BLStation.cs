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
                //not sure why it says in instructions about drone list, think we should ignore
            }
            public void UpdateStationInfo(int stationId, int name, int chargingSlots)
            {
                Station station = SearchStation(stationId);
                if (name != -1)
                    station.Name = name;
                if (chargingSlots != -1)
                {
                    if (chargingSlots - station.Charging.Count < 0)
                        throw;
                    station.OpenChargeSlots = chargingSlots - station.Charging.Count;
                }
                dalAP.DeleteStation(stationId);
                dalAP.AddStation(station.Id, station.Name, IDAL.DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), IDAL.DO.StaticSexagesimal.ParseDouble(station.Location.Longitude), station.OpenChargeSlots + station.Charging.Count);
            }
            private Station CreateStation(IDAL.DO.Station old)
            {
                Station station = new Station();
                station.Id = old.Id;
                station.Location = new Location { Latitude = old.Latitude, Longitude = old.Latitude };
                station.Name = old.Name;
                station.OpenChargeSlots = old.ChargeSlots;
                foreach(DroneToList drone in dronesBL)
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
                List<Station> newStations = new List<Station>();
                foreach (IDAL.DO.Station station in stations)
                {
                    newStations.Add(CreateStation(station));
                }
                return newStations;
            }
            public IEnumerable<StationToList> ListStation()//station to list- no its not
            {
                IEnumerable<IDAL.DO.Station> stations = dalAP.YieldStation();
                List<StationToList> newStations = new List<StationToList>();
                foreach (IDAL.DO.Station station in stations)
                {
                    newStations.Add(new StationToList {  Id = station.Id, Name = station.Name, OpenChargeSlots = station.ChargeSlots, UsedChargeSlots = CreateStation(station).Charging.Count()});
                }
                return newStations;
            }
            public IEnumerable<StationToList> ListStationAvailable()
            {
                return from station in YieldStation()
                       where station.OpenChargeSlots > 0
                       select new StationToList { Id = station.Id, Name = station.Name, OpenChargeSlots = station.OpenChargeSlots, UsedChargeSlots = station.Charging.Count };
            }
        }
    }
}
