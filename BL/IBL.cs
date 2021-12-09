using System;
using System.Collections.Generic;
using System.Text;
using BL;
using BO;
namespace BlApi
{
    public interface IBL
    {
        /// <summary>
        /// Adds a Station to the DataBase
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="chargeSlots"></param>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        /// <summary>
        /// Adds a Drone to the DataBase
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationIdForCharging"></param>
        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging);
        /// <summary>
        /// Adds a Customer to the DataBase
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        /// <summary>
        /// Adds a Parcel to the DataBase
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="targetId"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority);
        /// <summary>
        /// Updates the model of a Drone
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="newModel"></param>
        public void UpdateDroneModel(int droneId, string newModel);
        /// <summary>
        /// Updates relevant properties of a Station
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="name"></param>
        /// <param name="chargingSlots"></param>
        public void UpdateStationInfo(int stationId, string name, int chargingSlots);
        /// <summary>
        /// Updates relevant properties of a Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomerInfo(int customerId, string name, string phone);
        /// <summary>
        /// Sends a Drone to charge
        /// </summary>
        /// <param name="droneId"></param>
        public void DroneToCharge(int droneId);
        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="timeCharging"></param>
        public void ReleaseCharging(int droneId);
        /// <summary>
        /// Attributes a Parcel awaiting for attribution to the requested Drone
        /// </summary>
        /// <param name="droneId"></param>
        public void AttributeAParcel(int droneId);
        /// <summary>
        /// Picks up a Parcel attributed to the requsted Drone
        /// </summary>
        /// <param name="droneId"></param>
        public void PickUpAParcel(int droneId);
        /// <summary>
        /// Delivers a parcel the was picked up by the requested Drone
        /// </summary>
        /// <param name="droneId"></param>
        public void DeliverAParcel(int droneId);
        /// <summary>
        /// Returns a requested Station from the DataBase
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public Station SearchStation(int stationId);
        /// <summary>
        /// Returns a requested Drone from the DataBase
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Drone SearchDrone(int droneId);
        /// <summary>
        /// Returns a requested Customer from the DataBase
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer SearchCustomer(int customerId);
        /// <summary>
        /// Returns a requested Parcel from the DataBase
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public Parcel SearchParcel(int parcelId);
        /// <summary>
        /// Returns all Stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationToList> ListStation();
        /// <summary>
        /// Returns all Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerToList> ListCustomer();
        /// <summary>
        /// Returns all Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneToList> ListDrone();
        /// <summary>
        /// Returns all Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelToList> ListParcel();
        /// <summary>
        /// Returns all Parcels the haven't been attributed to a Drone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParcelToList> ListParcelNotAttributed();
        /// <summary>
        /// Returns all Stations that have available charging slots
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationToList> ListStationAvailable();
        public IEnumerable<DroneToList> ListDroneConditional(Predicate<DroneToList> predicate);
        public IEnumerable<ParcelToList> ListParcelConditional(Predicate<ParcelToList> predicate);
        public IEnumerable<StationToList> ListStationConditional(Predicate<StationToList> predicate);
        public IEnumerable<CustomerToList> ListCustomerConditional(Predicate<CustomerToList> predicate);

    }
}
