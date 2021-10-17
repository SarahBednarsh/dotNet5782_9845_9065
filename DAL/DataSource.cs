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
        internal static List<Drone> Drones;
        internal static List<Station> Stations;
        internal static List<Customer> Customers;
        internal static List<Parcel> Parcels;
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
            //fill later - page 7
        }
    }
}