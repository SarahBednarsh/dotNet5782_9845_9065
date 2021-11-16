using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO  
    {
        public partial class BL
        {
            public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots) { }
            public void UpdateStationInfo(int stationId) { }//not sure
            public Station SearchStation(int stationId) { }
            public IEnumerable<Station> YieldStation() { }
        }
    }
}
