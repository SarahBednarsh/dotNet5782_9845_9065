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
            internal static double available = 0;
            internal static double light = 0;
            internal static double medium = 0;
            internal static double heavy = 0;
            internal static double chargingPace = 0;
        }
        /// <summary>
        /// initializes the datasource with random data
        /// </summary>
        internal static void Initialize()
        {

            Random r = new Random();
            int Id;
            for (int i = 0; i < 2; i++)
            {
                Id = r.Next(1000, 10000);
                while (DataSource.Stations.Exists(x => x.Id == Id))
                    Id = r.Next(1000, 10000);
                Station station = new Station() { Id = Id, Name = r.Next(), Longitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Latitude"), ChargeSlots = r.Next(9) + 1 };
                DataSource.Stations.Add(station);//so it is a realistic number of chargeslos, and it might be full eventually
            }
            for (int i = 0; i < 5; i++)
            {
                Id = r.Next(1000, 10000);
                while (DataSource.Drones.Exists(x => x.Id == Id))
                    Id = r.Next(1000, 10000);
                Drone drone = new Drone() { Id = Id, Model = "model" + i, MaxWeight = (WeightCategories)r.Next(1, 3) };
                DataSource.Drones.Add(drone);
            }
            string[] names = new string[10] { "Liorah", "Sarah", "Margalit", "Adi", "Bilbo Baggins", "Paul", "Joseph", "Yoram", "Devorah", "Simcha" };
            for (int i = 0; i < 10; i++)
            {
                Id = r.Next(100000000, 1000000000);
                while (DataSource.Customers.Exists(x => x.Id == Id))
                    Id = r.Next(100000000, 1000000000);
                Customer customer = new Customer() { Id = Id, Name = names[i], Phone = r.Next(520000000, 529999999).ToString(), Longitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Latitude") };
                DataSource.Customers.Add(customer);
            }
            DataSource.Config.RunningParcelNumber = r.Next(1000000, 1000000000);
            for (int i = 0; i < 10; i++)
            {
                DateTime? start = new DateTime(2020, i + 1, 1);
                int forScheduled = r.Next(1, 25);
                int forPickedUp = forScheduled + 1;
                DateTime? delivered;
                if (i % 2 == 0)
                    delivered = ((DateTime)start).AddDays(forPickedUp + 1);
                else
                    delivered = null;
                Parcel parcel = new Parcel()
                {
                    Id = ++DataSource.Config.RunningParcelNumber,
                    SenderId = Customers[i].Id,
                    TargetId = Customers[9 - i].Id,
                    Weight = (WeightCategories)r.Next(1, 3),
                    Priority = (Priorities)r.Next(1, 3),
                    Requested = DateTime.Today,
                    DroneId = Drones[i % 5].Id,
                    Scheduled = ((DateTime)start).AddDays(forScheduled),
                    PickedUp = ((DateTime)start).AddDays(forPickedUp),
                    Delivered = delivered
                };
                DataSource.Parcels.Add(parcel);
            }
        }
    }
}