﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO    {
        public partial class BL
        {
            public IEnumerable<Station> OpenChargeSlots()
            {
                List<Station> open = new List<Station>();
                foreach (IDAL.DO.Station station in dalAP.YieldStation())
                {
                    if (station.ChargeSlots > 0)
                        open.Add(station);
                }
                return open;
            }
            public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
            {
                if (dalAP.YieldStation().Exists(x => x.Id == id))
                    return;
                Station tempStation = new Station() { Id = id, Name = name, Location = new Coordinates(longitude, latitude), ChargeSlots = chargeSlots };
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
            public double CalcDisFromStation(int id, double longitude, double latitude)
            {
                Station station = SearchStation(id);
                //deal with if doesn't exist
                return station.Location.CalcDis(new Coordinates(longitude, latitude));
            }
        }
    }
}
