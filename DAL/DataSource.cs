using System;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal class Config
        {
            internal static int RunningParcelNumber = 0;
            internal static bool available = true;
            internal static bool light = false;
            internal static bool medium = false;
            internal static bool heavy = false;
            internal static double chargingPace = 0;//should this be static??
        }
        /// <summary>
        /// initializes the datasource with random data
        /// </summary>
        internal static void Initialize()
        {

            Random r = new Random();
            for (int i = 0; i < 2; i++)
            {
                Station station = new Station() { Id = r.Next(1000, 10000), Name = r.Next(), Longitude = new Sexagesimal(r.NextDouble() + r.Next(-999, 999), "Longitude"), Latitude = new Sexagesimal(r.NextDouble() + r.Next(-999, 999), "Latitude"), ChargeSlots = r.Next(1, 10) };
                DataSource.Stations.Add(station);//so it is a realistic number of chargeslos, and it might be full eventually
            }
            for (int i = 0; i < 5; i++)
            {
                Drone drone = new Drone() { Id = r.Next(100, 1000), Model = "model" + i, MaxWeight = (WeightCategories)r.Next(1, 3) };
                DataSource.Drones.Add(drone);
            }
            string[] names = new string[10] { "Liorah", "Sarah", "Margalit", "Adi", "Bilbo Baggins", "Paul", "Joseph", "Yoram", "Devorah", "Simcha" };
            for (int i = 0; i < 10; i++)
            {
                Customer customer = new Customer() { Id = r.Next(100000000, 1000000000), Name = names[i], Phone = r.Next(520000000, 529999999).ToString(), Longitude = new Sexagesimal(r.NextDouble() + r.Next(-999, 999), "Longitude"), Latitude = new Sexagesimal(r.NextDouble() + r.Next(-999, 999), "Latitude") };
                DataSource.Customers.Add(customer);
            }
            DataSource.Config.RunningParcelNumber = r.Next(1000000, 1000000000);
            for (int i = 0; i < 10; i++)//sarah update
            {
                DateTime start = new DateTime(2020, i + 1, 1);
                int forScheduled = r.Next(1, 25);
                int forPickedUp = forScheduled + 1;
                int forDelivered = forPickedUp + 1;
                Parcel parcel = new Parcel()
                {
                    Id = ++DataSource.Config.RunningParcelNumber,
                    SenderId = Customers[i].Id,
                    TargetId = Customers[9 - i].Id,
                    Weight = (WeightCategories)r.Next(1, 3),
                    Priority = (Priorities)r.Next(1, 3),
                    Requested = DateTime.Today,
                    DroneId = Drones[i % 5].Id,
                    Scheduled = start.AddDays(forScheduled),
                    Delivered = start.AddDays(forDelivered),
                    PickedUp = start.AddDays(forPickedUp)
                };
                DataSource.Parcels.Add(parcel);
            }
        }
    }
}