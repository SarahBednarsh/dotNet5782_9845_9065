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
                return;
            Station tempStation = new Station() { Id = id, Name = name, Longitude = new Sexagesimal(longitude, "Longitude"), Latitude = new Sexagesimal(latitude, "Latitude"), ChargeSlots = chargeSlots };
            DataSource.Stations.Add(tempStation);
        }
        public Station SearchStation(int stationId)
        {
            //if (!DataSource.Stations.Exists(x => x.Id == stationId))
               // return new Station(Id = 0);
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
            Station station = SearchStation(id);
            //deal with if doesn't exist
            double deltalLongitude = station.Longitude.ParseDouble() - longitude;
            double deltalLatitude = station.Latitude.ParseDouble() - latitude;
            return Math.Sqrt(Math.Pow(deltalLatitude, 2) + Math.Pow(deltalLongitude, 2));
        }

    }
}
