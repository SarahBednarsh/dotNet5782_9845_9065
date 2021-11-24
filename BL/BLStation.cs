using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
            {
                dalAP.AddStation(id, name, longitude, latitude, chargeSlots);
                //not sure why it says in instructions about drone list
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


                return station;
            }
            public Station SearchStation(int stationId) { }
            public IEnumerable<Station> YieldStation()//station to list
            {

            }
            public IEnumerable<Station> YieldStationAvailable() { }
        }
    }
}
