using System;
using System.Collections.Generic;
using DO;
using DalApi;

namespace Dal
{
    internal class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<User> Users = new List<User>();
        internal class Config
        {
            internal static int RunningParcelNumber = 0;
            internal static double available = 0.0002;
            internal static double light = 0.0003;
            internal static double medium = 0.0004;
            internal static double heavy = 0.0005;
            internal static double chargingPace = 5;
            internal static string defaultPhoto = "/whitelogo-removebg-preview (3).png";
        }
        /// <summary>
        /// initializes the datasource with random data
        /// </summary>
        internal static void Initialize()
        {

            Random r = new Random();
            int Id;
            string[] stationNames = new string[2] { "station1", "station2" };
            for (int i = 0; i < 2; i++)//adding stations
            {
                Id = r.Next(1000, 10000);
                while (DataSource.Stations.Exists(x => x.Id == Id))
                    Id = r.Next(1000, 10000);
                Station station = new Station() { Id = Id, Name = stationNames[i], Longitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Latitude"), ChargeSlots = r.Next(9) + 1 };
                DataSource.Stations.Add(station);//so it is a realistic number of chargeslots, and it might be full eventually
            }
            for (int i = 0; i < 10; i++)//adding drones
            {
                Id = r.Next(1000, 10000);
                while (DataSource.Drones.Exists(x => x.Id == Id))
                    Id = r.Next(1000, 10000);
                Drone drone = new Drone() { Id = Id, Model = "model" + i, MaxWeight = (WeightCategories)r.Next(1, 3) };
                DataSource.Drones.Add(drone);
            }
            string[] names = new string[10] { "Liorah", "Sarah", "Margalit", "Adi", "Bilbo Baggins", "Paul", "Joseph", "Yoram", "Devorah", "Simcha" };
            for (int i = 0; i < 10; i++)//adding customers
            {
                Id = r.Next(100000000, 1000000000);
                while (DataSource.Customers.Exists(x => x.Id == Id))
                    Id = r.Next(100000000, 1000000000);
                Customer customer = new Customer() { Id = Id, Name = names[i], Phone = r.Next(520000000, 529999999).ToString(), Longitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(r.NextDouble() + r.Next(-999, 999), "Latitude") };
                DataSource.Customers.Add(customer);
            }
            DataSource.Config.RunningParcelNumber = r.Next(1000000, 1000000000);
            for (int i = 0; i < 12; i++)//adding parcels
            {
                DateTime? start = new DateTime(2020, i + 1, 1,r.Next(24),r.Next(60),r.Next(60));
                int forScheduled = r.Next() % 8 != 0 ? r.Next(1, 25) : 0;//sarah bug-nvm
                int forPickedUp = (r.Next() % 4 != 0 && forScheduled != 0) ? forScheduled + 1 : 0;
                DateTime? delivered = r.Next() % 4 != 0 && forPickedUp != 0 ? start + TimeSpan.FromDays(forPickedUp + 1) + TimeSpan.FromSeconds(r.Next(60 * 60 * 24)) : null;
                Parcel parcel = new Parcel()
                {
                    Id = ++DataSource.Config.RunningParcelNumber,
                    SenderId = Customers[i%10].Id,
                    TargetId = Customers[(12 - i)%10].Id,
                    Weight = (WeightCategories)r.Next(1, 3),
                    Priority = (Priorities)r.Next(1, 3),
                    Requested = start,
                    DroneId = forScheduled == 0 ? -1 : Drones[i % 5].Id,//like attribute
                    Scheduled = forScheduled != 0 ? start + TimeSpan.FromDays(forScheduled) + TimeSpan.FromSeconds(r.Next(60 * 60 * 24)) : null,//sarah- is it ok that they are not nullable? cause it wa oroblimatic when I did delivery
                    PickedUp = forPickedUp != 0 ? start + TimeSpan.FromDays(forPickedUp) + TimeSpan.FromSeconds(r.Next(60 * 60 * 24)) : null,
                    Delivered = delivered
                };
                DataSource.Parcels.Add(parcel);
            }
        }
    }
}