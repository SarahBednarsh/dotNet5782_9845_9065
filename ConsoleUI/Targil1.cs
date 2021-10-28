﻿using System;
using System.Collections.Generic;
using DalObject;
using IDAL.DO;
namespace ConsoleUI
{
    public enum Actions { Exit, Add, Update, View, List , Calc}
    public enum Data { Station = 1, Drone, Customer, Parcel, ParcelNotAttributed, StationsWithAvailableChargers }
    public enum UpdateOption { Attribute = 1, Pickup, Ship, SendToCharge }
    class Targil1
    {
        static void Main(string[] args)
        {
            DalObject.DalObject project = new DalObject.DalObject();
            Actions option;
            do
            {
                Console.WriteLine("Enter 1 for adding an entity\n" +
                    "Enter 2 for updating an entity\n" +
                    "Enter 3 for displaying an entity\n" +
                    "Enter 4 for displaying a list of entities\n" +
                    "Enter 0 for Exit");
                int input;
                Int32.TryParse(Console.ReadLine(), out input);
                option = (Actions)input;
                Data specific;//will be used to decide what action to do in a specific category
                switch (option)
                {
                    case Actions.Exit:
                        Console.WriteLine("Bye bye!");
                        break;
                    case Actions.Add:
                        int id; string name; string phone; double longitude; double latitude;
                        Console.WriteLine("Enter 1 to add a station, 2 to add a drone, Enter 3 to add a customer, Enter 4 to add a parcel:");
                        Int32.TryParse(Console.ReadLine(), out input);
                        specific = (Data)input;
                        switch (specific)
                        {
                            case Data.Station:
                                Console.WriteLine("Enter ID:");
                                Int32.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine("Enter name (number):");
                                int numName;
                                Int32.TryParse(Console.ReadLine(), out numName);//handle entering a string (I think tryparse should be used)
                                Console.WriteLine("Enter longitude:");
                                Double.TryParse(Console.ReadLine(), out longitude);
                                Console.WriteLine("Enter latitude:");
                                Double.TryParse(Console.ReadLine(), out latitude);
                                Console.WriteLine("Enter amount of charge slots:");
                                int chargeSlots;
                                Int32.TryParse(Console.ReadLine(), out chargeSlots);
                                project.AddStation(id, numName, longitude, latitude, chargeSlots);
                                break;
                            case Data.Drone:
                                Console.WriteLine("Enter ID:");
                                Int32.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine("Enter model:");
                                string model = Console.ReadLine();
                                Console.WriteLine("Enter maximum weight- 1 for light, 2 for medium, 3 for heavy:");
                                Int32.TryParse(Console.ReadLine(), out input);
                                WeightCategories maxWeight = (WeightCategories)input;
                                Console.WriteLine("Enter battey:");
                                double battery;
                                Double.TryParse(Console.ReadLine(), out battery);
                                project.AddDrone(id, model, maxWeight, DroneStatuses.Available, battery);
                                break;
                            case Data.Customer:
                                Console.WriteLine("Enter ID:");
                                Int32.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine("Enter name:");
                                name = Console.ReadLine();
                                Console.WriteLine("Enter phone number:");
                                phone = Console.ReadLine();
                                Console.WriteLine("Enter longitude:");
                                Double.TryParse(Console.ReadLine(), out longitude);
                                Console.WriteLine("Enter latitude:");
                                Double.TryParse(Console.ReadLine(), out latitude);
                                project.AddCustomer(id, name, phone, longitude, latitude);
                                break;
                            case Data.Parcel:
                                Console.WriteLine("Enter sender ID:");
                                int senderId;
                                Int32.TryParse(Console.ReadLine(), out senderId);
                                Console.WriteLine("Enter target ID:");
                                int targetId;
                                Int32.TryParse(Console.ReadLine(), out targetId);
                                Console.WriteLine("Enter weight of parcel- 1 for light, 2 for medium, 3 for heavy:");
                                Int32.TryParse(Console.ReadLine(), out input);
                                WeightCategories weight = (WeightCategories)input;
                                Console.WriteLine("Enter priority of parcel- 1 for normal, 2 for quick, 3 for emergency:");
                                Int32.TryParse(Console.ReadLine(), out input);
                                Priorities priority = (Priorities)input;
                                Console.WriteLine("Enter drone ID:");
                                int droneId;
                                Int32.TryParse(Console.ReadLine(), out droneId);
                                project.AddParcel(senderId, targetId, weight, priority, DateTime.Now, droneId);
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    case Actions.Update:
                        Console.WriteLine("Enter 1 to attribute a parcel to drone, 2 pick up a parcel, Enter 3 to ship a parcel, Enter 4 to send a drone to charge:");
                        Int32.TryParse(Console.ReadLine(), out input);
                        UpdateOption updateoption = (UpdateOption)input;
                        switch (updateoption)
                        {
                            case UpdateOption.Attribute:
                                Console.WriteLine("Enter parcel ID:");
                                int parcelId;
                                Int32.TryParse(Console.ReadLine(), out parcelId);
                                Console.WriteLine("Enter drone ID:");
                                int droneId;
                                Int32.TryParse(Console.ReadLine(), out droneId);
                                project.UpdateParcelsDrone(parcelId, droneId);
                                break;
                            case UpdateOption.Pickup:
                                Console.WriteLine("Enter parcel ID to be picked up:");
                                Int32.TryParse(Console.ReadLine(), out parcelId);
                                project.PickUpParcel(parcelId);
                                break;
                            case UpdateOption.Ship:
                                Console.WriteLine("Enter parcel ID to deliver:");
                                Int32.TryParse(Console.ReadLine(), out parcelId);
                                project.DeliverToCustomer(parcelId);
                                break;
                            case UpdateOption.SendToCharge:
                                Console.WriteLine("Enter drone to charge:");
                                Int32.TryParse(Console.ReadLine(), out droneId);
                               Console.WriteLine("Select from the following list a station:");
                                Console.WriteLine(string.Join("\r\n", project.OpenChargeSlots()));
                                int stationId;
                                Int32.TryParse(Console.ReadLine(), out stationId);
                                project.DroneToCharge(droneId, stationId);
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    case Actions.View:
                        Console.WriteLine("Enter 1 to display a station, Enter 2 to display a drone, Enter 3 to display a customer, Enter 4 to display a parcel:");
                        Int32.TryParse(Console.ReadLine(), out input);
                        specific = (Data)input;
                        switch (specific)
                        {
                            case Data.Station:
                                Console.WriteLine("Enter station ID:");
                                Int32.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(project.SearchStation(id));
                                break;
                            case Data.Drone:
                                Console.WriteLine("Enter drone ID:");
                                Int32.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(project.SearchDrone(id));
                                break;
                            case Data.Customer:
                                Console.WriteLine("Enter customer ID:");
                                Int32.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(project.SearchCustomer(id));
                                break;
                            case Data.Parcel:
                                Console.WriteLine("Enter parcel ID:");
                                Int32.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(project.SearchParcel(id));
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    case Actions.List:
                        Console.WriteLine("Enter 1 to display the list of stations, Enter 2 to display the list of drones, Enter 3 to display the list of customers, Enter 4 to display the list of parcels, Enter 5 to display parcels who aren't attributed to a drone, Enter 6 to display stations with available charging slots:");
                        Int32.TryParse(Console.ReadLine(), out input);
                        specific = (Data)input;
                        switch (specific)
                        {
                            case Data.Station:
                                Console.WriteLine(string.Join("\r\n", project.YieldStation()));
                                break;
                            case Data.Drone:
                                Console.WriteLine(string.Join("\r\n", project.YieldDrone()));
                                break;
                            case Data.Customer:
                                Console.WriteLine(string.Join("\r\n", project.YieldCustomer()));
                                break;
                            case Data.Parcel:
                                Console.WriteLine(string.Join("\r\n", project.YieldParcel()));
                                break;
                            case Data.ParcelNotAttributed:
                                Console.WriteLine(string.Join("\r\n", project.ParcelsWithNoDrone()));
                                break;
                            case Data.StationsWithAvailableChargers:
                                Console.WriteLine(string.Join("\r\n", project.OpenChargeSlots()));
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    case Actions.Calc:
                        Console.WriteLine("Enter 1 to display distance from a customer, 2 to display distance from station\n");
                        Int32.TryParse(Console.ReadLine(), out input);
                        specific = (Data)input;
                        Console.WriteLine("Enter ID:");
                        Int32.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("Enter longitude:");
                        Double.TryParse(Console.ReadLine(), out longitude);
                        Console.WriteLine("Enter latitude:");
                        Double.TryParse(Console.ReadLine(), out latitude);
                        Console.WriteLine("Distance is: ");
                        switch (specific)
                        {
                            case Data.Station:
                                Console.WriteLine(project.CalcDisFromStation(id, longitude, latitude));
                                break;
                            case Data.Drone:
                                Console.WriteLine(project.CalcDisFromDrone(id, longitude, latitude));
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            } while (option != 0);
        }
    }
}
