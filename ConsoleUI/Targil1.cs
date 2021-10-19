﻿using System;
using DalObject;//do we need this?
using IDAL.DO;
namespace ConsoleUI
{
    public enum Actions { Exit, Add, Update, View, List }
    public enum Data { Station = 1, Drone, Customer, Parcel }
    public enum UpdateOption { Attribute = 1, Pickup, Ship, SendToCharge }
    class Targil1
    {
        static void Main(string[] args)
        {
            DalObject.DalObject project = new DalObject.DalObject();
            Console.WriteLine("Enter 1 for adding an entity" +
                "Enter 2 for updating an entity " +
                "Enter 3 for displaying an entity" +
                "Enter 4 for displaying a list of entities" +
                "Enter 0 for Exit" +
                "");
            Actions option = (Actions)Console.Read();
            Data specific;//will be used to decide what action to do in a specific category
            switch (option)
            {
                case Actions.Exit:
                    Console.WriteLine("Bye bye!");
                    break;
                case Actions.Add:
                    int id; string name; string phone; double longitude; double latitude;
                    Console.WriteLine("Enter 1 to add a station" +
                "Enter 2 to add a drone" +
                "Enter 3 to add a customer" +
                "Enter 4 to add a parcel" +
                "");
                    specific = (Data)Console.Read();
                    switch (specific)
                    {
                        case Data.Station://public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
                            Console.WriteLine("Enter ID:");
                            id = Console.Read();
                            Console.WriteLine("Enter name:");
                            int numName = Console.Read();
                            Console.WriteLine("Enter longitude:");
                            longitude = Console.Read();
                            Console.WriteLine("Enter latitude:");
                            latitude = Console.Read();
                            Console.WriteLine("Enter amount of charge slots:");
                            int chargeSlots = Console.Read();
                            project.AddStation(id, numName, longitude, latitude, chargeSlots);
                            break;
                        case Data.Drone://public void AddDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
                            Console.WriteLine("Enter ID:");
                            id = Console.Read();
                            Console.WriteLine("Enter model:");
                            string model = Console.ReadLine();
                            Console.WriteLine("Enter maximum weight- 1 for light, 2 for medium, 3 for heavy:");
                            WeightCategories maxWeight = (WeightCategories)Console.Read();
                            Console.WriteLine("Enter battey:");
                            double battery = Console.Read();
                            project.AddDrone(id, model, maxWeight, DroneStatuses.Available, battery);
                            break;
                        case Data.Customer://public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
                            Console.WriteLine("Enter ID:");
                            id = Console.Read();
                            Console.WriteLine("Enter name:");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter phone number:");
                            phone = Console.ReadLine();
                            Console.WriteLine("Enter longitude:");
                            longitude = Console.Read();
                            Console.WriteLine("Enter latitude:");
                            latitude = Console.Read();
                            project.AddCustomer(id, name, phone, longitude, latitude);
                            break;
                        case Data.Parcel://public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority,DateTime requested, int droneId)
                            Console.WriteLine("Enter sender ID:");
                            int senderId = Console.Read();
                            Console.WriteLine("Enter target ID:");
                            int targetId = Console.Read();
                            Console.WriteLine("Enter weight of parcel- 1 for light, 2 for medium, 3 for heavy:");
                            WeightCategories weight = (WeightCategories)Console.Read();
                            Console.WriteLine("Enter priority of parcel- 1 for normal, 2 for quick, 3 for emergency:");
                            Priorities priority = (Priorities)Console.Read();
                            Console.WriteLine("Enter drone ID:");
                            int droneId = Console.Read();
                            project.AddParcel(senderId, targetId, weight, priority, DateTime.Now, droneId);
                            break;
                        default:
                            break;
                    }
                    break;
                case Actions.Update:
                    Console.WriteLine("Enter 1 to attribute a parcel to drone" +
                "Enter 2 pick up a parcel" +
                "Enter 3 to ship a parcel" +
                "Enter 4 to send a drone to charge" +
                "");
                    UpdateOption updateoption = (UpdateOption)Console.Read();
                    switch (updateoption)
                    {
                        case UpdateOption.Attribute://public void UpdateParcelsDrone(int parcelId, int droneId)
                            Console.WriteLine("Enter parcel ID:");
                            int parcelId = Console.Read();
                            Console.WriteLine("Enter drone ID:");
                            int droneId = Console.Read();
                            project.UpdateParcelsDrone(parcelId, droneId);
                            break;
                        case UpdateOption.Pickup:
                            Console.WriteLine("Enter parcel ID to be picked up:");
                            parcelId = Console.Read();
                            project.PickUpParcel(parcelId);
                            break;
                        case UpdateOption.Ship:
                            Console.WriteLine("Enter parcel ID to deliver:");
                            parcelId = Console.Read();
                            project.DeliverToCostumer(parcelId);
                            break;
                        case UpdateOption.SendToCharge:
                            Console.WriteLine("Enter drone to charge:");
                            droneId = Console.Read();
                            Console.WriteLine("Select from the following list a station:");
                            //print list of availables
                            int stationId = Console.Read();
                            project.DroneToCharge(droneId, stationId);
                            break;
                        default:
                            break;
                    }


                    break;
                case Actions.View:
                    Console.WriteLine("Enter 1 to display a station" +
               "Enter 2 to display a drone" +
               "Enter 3 to display a customer" +
               "Enter 4 to display a parcel" +
               "");
                    break;
                case Actions.List:
                    Console.WriteLine("Enter 1 to display the list of stations" +
               "Enter 2 to display the list of drones" +
               "Enter 3 to display the list of customers" +
               "Enter 4 to display the list of parcels" +
               "Enter 5 to display parcels who aren't attributed to a drone" +
               "Enter 6 to display stations with available charging slots" +
               "");
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
    }
}
