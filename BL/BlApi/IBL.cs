using System;
using System.Collections.Generic;
using System.Text;
using BL;
using BO;
namespace BlApi
{
    public interface IBL
    {
        #region adding
        /// <summary>
        /// Adds a Station to the DataBase
        /// </summary>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        /// <summary>
        /// Adds a Drone to the DataBase
        /// </summary>
        public void AddDrone(int id, string model, WeightCategories maxWeight, int stationIdForCharging);
        /// <summary>
        /// Adds a Customer to the DataBase
        /// </summary>
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        /// <summary>
        /// Adds a Parcel to the DataBase
        /// </summary>
        public void AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority);
        public void AddUser(int id, string userName, string photo, string email, string password, bool isManager);
        #endregion

        #region updating
        /// <summary>
        /// Updates the model of a Drone
        /// </summary>
        public void UpdateDroneModel(int droneId, string newModel);
        /// <summary>
        /// Updates relevant properties of a Station
        /// </summary>
        public void UpdateStationInfo(int stationId, string name, int chargingSlots);
        /// <summary>
        /// Updates relevant properties of a Customer
        /// </summary>
        public void UpdateCustomerInfo(int customerId, string name, string phone);
        /// <summary>
        /// Sends a Drone to charge
        /// </summary>
        public void DroneToCharge(int droneId);
        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        /// <param name="droneId"></param>
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
        public void DeliverAParcel(int droneId);
        /// <summary>
        /// Releases from charging all the drones that are in maintenance
        /// </summary>
        public void ReleaseAllCharging();
        /// <summary>
        /// Recovers password for user
        /// Generates a new password and sends email
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="lengthForNewPassword"></param>
        void RecoverPassword(string userName, int lengthForNewPassword);

        #endregion

        #region display specific object
        /// <summary>
        /// Returns a requested Station from the DataBase
        /// </summary>
        /// <param name="stationId"></param>
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
        /// Returns a requested user from the database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User SearchUser(string userName);
        /// <summary>
        /// returns an object of type drone to list with the same ID
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public DroneToList SearchDroneToList(int droneId);

        #endregion

        #region display list of objects
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
        /// <summary>
        /// List parcels sent from a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<ParcelToList> ListParcelFromCustomer(int customerId);
        //public IEnumerable<DroneToList> ListDroneConditional(Predicate<DroneToList> predicate);
        //public IEnumerable<ParcelToList> ListParcelConditional(Predicate<ParcelToList> predicate);
        //public IEnumerable<StationToList> ListStationConditional(Predicate<StationToList> predicate);
        //public IEnumerable<CustomerToList> ListCustomerConditional(Predicate<CustomerToList> predicate);
        #endregion

        #region deleting
        void DeleteParcel(int parcelId);
        void DeleteDrone(int droneId);
        void DeleteCustomer(int customerId);
        void DeleteStation(int stationId);
        void DeleteUser(int id);
        #endregion
        bool UserInfoCorrect(string userName, string password, bool isManager);
        void ActivateDroneSimulator(int droneId, Action update, Func<bool> stop);
        IEnumerable<ParcelToList> ListParcelCreatedInTimeRange(DateTime begin, DateTime end);
    }
}
