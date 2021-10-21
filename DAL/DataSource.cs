using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public class DataSource
    {
        /*internal static Drone[] Drones = new Drone[10];
        internal static Station[] Stations = new Station[5];
        internal static Customer[] Customers = new Customer[100];
        internal static Parcel[] Parcels = new Parcel[1000];*/
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal class Config
        {
            /*internal int FirstDroneIndex = 0;
            internal int FirstStationIndex = 0;
            internal int FirstCustomerIndex = 0;
            internal int FirstParcelIndex = 0;*/
            internal static int RunningParcelNumber = 0;
        }
        internal static void Initialize() //not sure about access permissions
        {
           
            Random r = new Random();
            for (int i = 0; i < 2; i++)
            {
                DataSource.Stations.Add(new Station(r.Next(1000,10000), r.Next(), r.NextDouble(), r.NextDouble(), r.Next(1,10)));//so it is a realistic number of chargeslos, and it might be full eventually
            }
            for (int i = 0; i < 5; i++)
            {
                DataSource.Drones.Add(new Drone(r.Next(100,1000),"model"+i,(WeightCategories)r.Next(1,3), (DroneStatuses)r.Next(1, 3),r.NextDouble()));
            }
            string[] names = new string[10] { "Liorah", "Sarah", "Margalit", "Adi","Bilbo Baggins","Paul","Joseph","Yoram","Devorah","Simcha" };
            for (int i = 0; i < 10; i++)
            {
                DataSource.Customers.Add(new Customer(r.Next(100000000, 1000000000), names[i], r.Next(520000000, 529999999).ToString(), r.NextDouble(), r.NextDouble()));
            }
            for (int i = 0; i < 10; i++)
            {
                DataSource.Parcels.Add(new Parcel(++DataSource.Config.RunningParcelNumber, Customers[i].Id, Customers[9 - i].Id, (WeightCategories)r.Next(1, 3), (Priorities)r.Next(1, 3), DateTime.Today, Drones[i % 5].Id));
            }
            //Liorah is this how we're supposed to do it? what do you put in the string categories?





            //fill later - page 7

        }
    }
}