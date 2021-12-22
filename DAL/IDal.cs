using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DalApi
{
    public interface IDal
    {
        #region adding
        /// <summary>
        /// Adds a station to the DAL database
        /// </summary>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        /// <summary>
        /// Adds a drone to the DAL database
        /// </summary>
        public void AddDrone(int id, string model, WeightCategories maxWeight);
        /// <summary>
        /// Adds a customer to the DAL database
        /// </summary>
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        /// <summary>
        /// Adds a parcel to the DAL database
        /// </summary>
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, int droneId);
        #endregion

        #region deleting
        /// <summary>
        /// Deletes a customer from the DAL database
        /// </summary>
        public void DeleteCustomer(int id);
        /// <summary>
        /// Deletes a drone from the DAL database
        /// </summary>
        public void DeleteDrone(int id);
        /// <summary>
        /// Deletes a parcel from the DAL database
        /// </summary>
        public void DeleteParcel(int id);
        /// <summary>
        /// Deletes a station from the DAL database
        /// </summary>
        public void DeleteStation(int id);
        #endregion

        #region updating
        /// <summary>
        /// Attributes a drone to a parcel by finding the drone, making sure it is available,
        /// finding the parcel and addig the drone ID to it
        /// </summary>
        public void UpdateParcelsDrone(int parcelId, int droneId);
        /// <summary>
        /// Schedules a parcel- updates the parcel scheduling time
        /// </summary>
        public void ScheduleParcel(int parcelId);
        /// <summary>
        /// Picks up a parcel- updates the parcel pickup time
        /// </summary>
        public void PickUpParcel(int parcelId);
        /// <summary>
        /// Delivers a parcel to customer- updates the parcel delivery time
        /// </summary>
        public void DeliverToCustomer(int parcelId);
        /// <summary>
        /// Sends a drone to charge- adds a drone charge to the log, and updates the drone and station accordingly
        /// </summary>
        public void DroneToCharge(int droneId, int stationId);
        /// <summary>
        /// Releases a drone from charging by using the drone charges to find the charging station and updating
        /// the station, drone and drone charges accordingly
        /// </summary>
        public void ReleaseCharging(int droneId);
        #endregion

        #region display specific object
        /// <summary>
        /// Returns a requested station from the DataBase
        /// </summary>
        public Station SearchStation(int stationId);
        /// <summary>
        /// Returns a requested drone from the DataBase
        /// </summary>
        public Drone SearchDrone(int droneId);
        /// <summary>
        /// Returns a requested cuustomer from the DataBase
        /// </summary>
        public Customer SearchCustomer(int customerId);
        /// <summary>
        /// Returns a requested parcel from the DataBase
        /// </summary>
        public Parcel SearchParcel(int parcelId);
        #endregion

        #region display list of objects
        /// <summary>
        /// Returns all stations
        /// </summary>
        public IEnumerable<Station> YieldStation();
        /// <summary>
        /// Returns all drones
        /// </summary>
        public IEnumerable<Drone> YieldDrone();
        /// <summary>
        /// Returns all customers
        /// </summary>
        public IEnumerable<Customer> YieldCustomer();
        /// <summary>
        /// Returns all parcels
        /// </summary>
        public IEnumerable<Parcel> YieldParcel();
        /// <summary>
        /// returns all statios with available charging slots
        /// </summary>
        public IEnumerable<Station> OpenChargeSlots();
        /// <summary>
        /// Returns a list of parcels without an attributed drone
        /// </summary>
        public IEnumerable<Parcel> ParcelsWithNoDrone();
        /// <summary>
        /// Returns all parcels that answer to a specific condition given as a predicate
        /// </summary>
        public IEnumerable<Parcel> ListParcelConditional(Predicate<Parcel> predicate);
        /// <summary>
        /// Returns all stations that answer to a specific condition given as a predicate
        /// </summary>
        public IEnumerable<Station> ListStationConditional(Predicate<Station> predicate);
        #endregion

        #region distance calculations
        /// <summary>
        /// Calculates the distance between a given location and a given station
        /// </summary>
        public double CalcDisFromStation(int id, double longitude, double latitude);
        /// <summary>
        /// Calculates the distance between a given location and a given customer
        /// </summary>
        public double CalcDisFromCustomer(int id, double longitude, double latitude);
        #endregion

        /// <summary>
        /// Returns the set battery consumptions we have 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<double> ReqPowerConsumption();
        /// <summary>
        /// Returns the beginning time of a droneCharge
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        DateTime? GetBeginningChargeTime(int droneId);
    }
}
