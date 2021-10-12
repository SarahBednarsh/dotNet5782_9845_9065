using System;

namespace DAL
{
    namespace IDAL
    {

        namespace DO
        {

            public struct Customer
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Phone { get; set; }
                public double Longitude { get; set; }
                public double Lattitude { get; set; }
                public override string ToString()
                {
                    return string.Format("Id: {0}, Name: {1}, Phone: {2}, Longitude: {3}, Latitude: {4}", Id, Name, Phone, Longitude, Lattitude);
                }
            }
            public struct Parcel
            {
                public int Id { get; set; }
                public int SenderId { get; set; }
                public int TargetId { get; set; }
                public WeightCategories Weight { get; set; }
                public Priorities Priority { get; set; }
                public DateTime Requested { get; set; }
                public int DroneId { get; set; }
                public DateTime Scheduled { get; set; }
                public DateTime PickedUp { get; set; }
                public DateTime Delivered { get; set; }
                public override string ToString()
                {
                    return string.Format("Id: {0}, Name: {1}, Phone: {2}, Longitude: {3}, Latitude: {4}", Id, Name, Phone, Longitude, Lattitude);
                }
            }
            public struct Drone
            {
                public int Id { get; set; }
                public string Model { get; set; }
                public WeightCategories MaxWeight { get; set; }
                public DroneStatuses Status { get; set; }
                public double Battery { get; set; }

            }
            public struct Station
            {
                public int Id { get; set; }
                public int Name { get; set; }
                public double Longitude { get; set; }
                public double Lattitude { get; set; }
                public int ChargeSlots { get; set; }
            }
            public struct DroneCharge
            {
                public int DroneId { get; set; }
                public int StationId { get; set; }
            }

            enum ACTIONS {stuff} // fill later and move to another module

            namespace DalObject
            {
                public class DataSource
                {
                    Drone[] Drones = new Drone[10];
                    Station[] Stations = new Station[5];
                    Customer[] Customers = new Customer[100];
                    Parcel[] Parcels = new Parcel[1000];
                    internal class Config
                    {
                        internal int FirstDroneIndex = 0;
                        internal int FirstStationIndex = 0;
                        internal int FirstCustomerIndex = 0;
                        internal int FirstParcelIndex = 0;
                        public Config()
                        {
                            Config.Initialize();
                        }
                        static void Initialize()
                        {
                            //fill later - page 7
                        }
                    }
                    public class DalObject
                    {
                        public void UpdateDrone(int id)// also copy for each entity and change parameters
                        {
                            //fill implementation
                        }
                        // top of page 8 - ???
                        public void AddDrone()
                        {
                            //fill implementation
                        }

                    }
                       

                }
            }
        }
        
        public class Library1
        {
        }
    }
}
