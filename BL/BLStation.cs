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
           
            public void UpdateStationInfo(int stationId, string name, int chargingSlots) { }//not sure
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
