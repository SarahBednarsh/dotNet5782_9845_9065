using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace DalObject
{
    public partial class DalObject
    {
        public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
        {
            if (DataSource.Stations.Exists(x => x.Id == id))
                throw new StationException("Station to add alredast exists.");
            Station tempStation = new Station() { Id = id, Name = name, Longitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(latitude, "Latitude"), ChargeSlots = chargeSlots };
            DataSource.Stations.Add(tempStation);
        }
        public void DeleteStation(int id)
        {
            if (!DataSource.Stations.Exists(x => x.Id == id))
                throw new StationException("Station to delete does not exist.");
            DataSource.Stations.Remove(DataSource.Stations.Find(x => x.Id == id));
        }
        public Station SearchStation(int stationId)
        {
            if (!DataSource.Stations.Exists(x => x.Id == stationId))
                throw new StationException("Station does not exist.");
            return DataSource.Stations.Find(x => x.Id == stationId);
        }
        public IEnumerable<Station> YieldStation()
        {
            return new List<Station>(DataSource.Stations);
        }
        public IEnumerable<Station> OpenChargeSlots()
        {
            List<Station> open = new List<Station>();
            foreach (Station station in DataSource.Stations)
            {
                if (station.ChargeSlots > 0)
                    open.Add(station);
            }
            return open;
        }
        public double CalcDisFromStation(int id, double longitude, double latitude)
        {
            if (!DataSource.Stations.Exists(x => x.Id == id))
                throw new StationException("Station does not exist.");
            Station station = SearchStation(id);
            double slong = StaticSexagesimal.ParseDouble(station.Longitude);
            double slat = StaticSexagesimal.ParseDouble(station.Latitude);
            return StaticSexagesimal.CalcDis(slong, slat, longitude, latitude);
        }

    }
}
