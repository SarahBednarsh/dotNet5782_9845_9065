using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO; //is this allowed?

namespace IDAL
{
    public interface IDal
    {
        public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots);
        public void AddDrone(int id, string model, WeightCategories maxWeight);
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority,
                DateTime requested, int droneId);
        public void UpdateParcelsDrone(int parcelId, int droneId);
        public void PickUpParcel(int parcelId);
        public void DeliverToCustomer(int parcelId);
        public void DroneToCharge(int droneId, int stationId);
        public void ReleaseCharging(int droneId);
        public Station SearchStation(int stationId);
        public Drone SearchDrone(int droneId);
        public Customer SearchCustomer(int customerId);
        public Parcel SearchParcel(int parcelId);
        public IEnumerable<Station> YieldStation();
        public IEnumerable<Drone> YieldDrone();
        public IEnumerable<Customer> YieldCustomer();
        public IEnumerable<Parcel> YieldParcel();
        public IEnumerable<Station> OpenChargeSlots();
        public IEnumerable<Parcel> ParcelsWithNoDrone();
        public double CalcDisFromStation(int id, double longitude, double latitude);
        public double CalcDisFromCustomer(int id, double longitude, double latitude);
        public IEnumerable<double> ReqPowerConsumption();
    }
}
