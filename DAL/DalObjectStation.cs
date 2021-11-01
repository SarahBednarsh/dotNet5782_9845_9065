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
            Station tempStation = new Station() { Id = id, Name = name, Longitude = new Sexagesimal(longitude, "longitude"), Latitude = new Sexagesimal(latitude, "latitude"), ChargeSlots = chargeSlots };
            DataSource.Stations.Add(tempStation);
        }
        public Station SearchStation(int stationId)
        {
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
            Station station = this.SearchStation(id);
            double deltalLongitude = station.Longitude.ParseDouble() - longitude;
            double deltalLatitude = station.Latitude.ParseDouble() - latitude;
            return Math.Sqrt(Math.Pow(deltalLatitude, 2) + Math.Pow(deltalLongitude, 2));
        }

    }
}
