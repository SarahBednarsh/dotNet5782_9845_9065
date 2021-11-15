using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        interface IBL
        {
            public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots);
            public Station SearchStation(int stationId);
            public IEnumerable<Station> YieldStation();
            public IEnumerable<Station> OpenChargeSlots();
            public double CalcDisFromStation(int id, double longitude, double latitude);
            public void AddDrone(int id, string model, WeightCategories maxWeight);
            public void UpdateParcelsDrone(int parcelId, int droneId);
            public void DroneToCharge(int droneId, int stationId);
            public void ReleaseCharging(int droneId);
            public Drone SearchDrone(int droneId);
            public IEnumerable<Drone> YieldDrone();
            public IEnumerable<double> ReqPowerConsumption();
        }
    }
}
