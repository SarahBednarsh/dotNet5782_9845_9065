using System;
using System.Collections.Generic;
using DalObject;//do we need this?
using IDAL.DO;
namespace ConsoleUI
{
    public enum Actions { Exit, Add, Update, View, List }
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
                //int a = Console.Read();
                //Console.WriteLine(a);
                option = (Actions)Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine((int)option);
                Data specific;//will be used to decide what action to do in a specific category
                switch (option)
                {
                    case Actions.Exit:
                        Console.WriteLine("Bye bye!");
                        break;
                    case Actions.Add:
                        int id; string name; string phone; double longitude; double latitude;
                        Console.WriteLine("Enter 1 to add a station, 2 to add a drone, Enter 3 to add a customer, Enter 4 to add a parcel:");
                       // specific = (Data)Console.Read();
                        specific = (Data)Convert.ToInt32(Console.ReadLine());
                        switch (specific)
                        {
                            case Data.Station://public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
                                Console.WriteLine("Enter ID:");
                                //id = Console.Read();
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter name:");
                                //int numName = Console.Read();
                                int numName = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter longitude:");
                                //longitude = Console.Read();
                                longitude = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter latitude:");
                                //latitude = Console.Read();
                                latitude = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter amount of charge slots:");
                                int chargeSlots = Convert.ToInt32(Console.ReadLine());
                                project.AddStation(id, numName, longitude, latitude, chargeSlots);
                                break;
                            case Data.Drone://public void AddDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
                                Console.WriteLine("Enter ID:");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter model:");
                                string model = Console.ReadLine();
                                Console.WriteLine("Enter maximum weight- 1 for light, 2 for medium, 3 for heavy:");
                                WeightCategories maxWeight = (WeightCategories)Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter battey:");
                                double battery = Convert.ToDouble(Console.ReadLine());
                                project.AddDrone(id, model, maxWeight, DroneStatuses.Available, battery);
                                break;
                            case Data.Customer://public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
                                Console.WriteLine("Enter ID:");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter name:");
                                name = Console.ReadLine();
                                Console.WriteLine("Enter phone number:");
                                phone = Console.ReadLine();
                                Console.WriteLine("Enter longitude:");
                                longitude = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter latitude:");
                                latitude = Convert.ToDouble(Console.ReadLine());
                                project.AddCustomer(id, name, phone, longitude, latitude);
                                break;
                            case Data.Parcel://public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority,DateTime requested, int droneId)
                                Console.WriteLine("Enter sender ID:");
                                int senderId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter target ID:");
                                int targetId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter weight of parcel- 1 for light, 2 for medium, 3 for heavy:");
                                WeightCategories weight = (WeightCategories)Console.Read();
                                Console.WriteLine("Enter priority of parcel- 1 for normal, 2 for quick, 3 for emergency:");
                                Priorities priority = (Priorities)Console.Read();
                                Console.WriteLine("Enter drone ID:");
                                int droneId = Convert.ToInt32(Console.ReadLine());
                                project.AddParcel(senderId, targetId, weight, priority, DateTime.Now, droneId);
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    case Actions.Update:
                        Console.WriteLine("Enter 1 to attribute a parcel to drone, 2 pick up a parcel, Enter 3 to ship a parcel, Enter 4 to send a drone to charge:");
                        UpdateOption updateoption = (UpdateOption)Convert.ToInt32(Console.ReadLine());
                        switch (updateoption)
                        {
                            case UpdateOption.Attribute://public void UpdateParcelsDrone(int parcelId, int droneId)
                                Console.WriteLine("Enter parcel ID:");
                                int parcelId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter drone ID:");
                                int droneId = Convert.ToInt32(Console.ReadLine());
                                project.UpdateParcelsDrone(parcelId, droneId);
                                break;
                            case UpdateOption.Pickup:
                                Console.WriteLine("Enter parcel ID to be picked up:");
                                parcelId = Convert.ToInt32(Console.ReadLine());
                                project.PickUpParcel(parcelId);
                                break;
                            case UpdateOption.Ship:
                                Console.WriteLine("Enter parcel ID to deliver:");
                                parcelId = Convert.ToInt32(Console.ReadLine());
                                project.DeliverToCostumer(parcelId);
                                break;
                            case UpdateOption.SendToCharge:
                                Console.WriteLine("Enter drone to charge:");
                                droneId = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Select from the following list a station:");
                                //List<Station> open = project.OpenChargeSlots();//put it straight into the function
                                Console.WriteLine(string.Join("\r\n", project.OpenChargeSlots()));
                                int stationId = Convert.ToInt32(Console.ReadLine());
                                project.DroneToCharge(droneId, stationId);
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    case Actions.View:
                        Console.WriteLine("Enter 1 to display a station, Enter 2 to display a drone, Enter 3 to display a customer, Enter 4 to display a parcel:");
                        specific = (Data)Console.Read();
                        switch (specific)
                        {
                            case Data.Station:
                                Console.WriteLine("Enter station ID:");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(project.SearchStation(id));
                                break;
                            case Data.Drone:
                                Console.WriteLine("Enter drone ID:");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(project.SearchDrone(id));
                                break;
                            case Data.Customer:
                                Console.WriteLine("Enter customer ID:");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(project.SearchCustomer(id));
                                break;
                            case Data.Parcel:
                                Console.WriteLine("Enter parcel ID:");
                                id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine(project.SearchParcel(id));
                                break;
                            default:
                                Console.WriteLine("ERROR");
                                break;
                        }
                        break;
                    case Actions.List:
                        Console.WriteLine("Enter 1 to display the list of stations, Enter 2 to display the list of drones, Enter 3 to display the list of customers, Enter 4 to display the list of parcels, Enter 5 to display parcels who aren't attributed to a drone, Enter 6 to display stations with available charging slots:");
                        specific = (Data)Console.Read();
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
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            } while (option != 0);
        }
    }
}
