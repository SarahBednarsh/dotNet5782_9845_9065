using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO; 

namespace IDAL
{
    public interface IDal
    {
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void AddDrone(int id, string model, WeightCategories maxWeight);
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, int droneId);
        public void DeleteCustomer(int id);
        public void DeleteDrone(int id);
        public void DeleteParcel(int id);
        public void DeleteStation(int id);
        public void UpdateParcelsDrone(int parcelId, int droneId);
        public void ScheduleParcel(int parcelId);
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
        public IEnumerable<Parcel> ListParcelConditional(Predicate<IDAL.DO.Parcel> predicate);
        public IEnumerable<Station> ListStationConditional(Predicate<Station> predicate);
        public double CalcDisFromStation(int id, double longitude, double latitude);
        public double CalcDisFromCustomer(int id, double longitude, double latitude);
        public IEnumerable<double> ReqPowerConsumption();
    }
}
