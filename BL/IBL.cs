using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public interface IBL
        {
            public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots);
            public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging);
            public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
            public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority);
            public void UpdateDroneName(int droneId, string newName);
            public void UpdateStationInfo(int stationId, int name, int chargingSlots);//not sure
            public void UpdateCustomerInfo(int customerId, string name, string phone);//not sure
            public void DroneToCharge(int droneId);
            public void ReleaseCharging(int droneId, TimeSpan timeCharging);
            public void AttributeAParcel(int droneId);
            public void PickUpAParcel(int droneId);
            public void DeliverAParcel(int droneId);
            //with these we will among other things print a specific object
            public Station SearchStation(int stationId);
            public Drone SearchDrone(int droneId);
            public Customer SearchCustomer(int customerId);
            public Parcel SearchParcel(int parcelId);
            //with these we will among other things print a list
            public IEnumerable<Station> YieldStation();
            public IEnumerable<Station> YieldStationAvailable();
            public IEnumerable<Drone> YieldDrone();
            public IEnumerable<Customer> YieldCustomer();
            public IEnumerable<Parcel> YieldParcel();
            public IEnumerable<Parcel> YieldParcelNotAttributed();
            public IEnumerable<StationToList> ListStationAvailable();

        }
    }
}
