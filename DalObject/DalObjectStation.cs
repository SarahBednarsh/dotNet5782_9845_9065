using System;
using System.Collections.Generic;
using System.Text;
using DO;
using DalApi;

namespace Dal
{
    partial class DalObject
    {
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            if (DataSource.Stations.Exists(x => x.Id == id))
                throw new StationException("Station to add already exists");
            Station tempStation = new Station() { Id = id, Name = name, Longitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(latitude, "Latitude"), ChargeSlots = chargeSlots };
            DataSource.Stations.Add(tempStation);
        }
        public Station SearchStation(int stationId)
        {
            if (!DataSource.Stations.Exists(x => x.Id == stationId))
                throw new StationException("Station does not exist");
            return DataSource.Stations.Find(x => x.Id == stationId);
        }
        public void DeleteStation(int id)
        {
            if (!DataSource.Stations.Exists(x => x.Id == id))
                throw new CustomerException("Station to delete does not exist.");
            DataSource.Stations.Remove(DataSource.Stations.Find(x => x.Id == id));
        }
        public IEnumerable<Station> YieldStation()
        {
            return new List<Station>(DataSource.Stations);
        }
        public IEnumerable<Station> ListStationConditional(Predicate<Station> predicate)
        {
            List<Station> open = new List<Station>();
            foreach (Station station in DataSource.Stations)
            {
                if (predicate(station))
                    open.Add(station);
            }
            return open;
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
                throw new CustomerException("Station does not exist.");
            Station station = this.SearchStation(id);
            double clong = StaticSexagesimal.ParseDouble(station.Longitude);
            double clat = StaticSexagesimal.ParseDouble(station.Latitude);
            return StaticSexagesimal.CalcDis(clong, clat, longitude, latitude);
        }

        public IEnumerable<DroneCharge> YieldDroneCharges()
        {
            foreach (DroneCharge droneCharge in DataSource.DroneCharges)
                yield return droneCharge;
        }

    }
}
