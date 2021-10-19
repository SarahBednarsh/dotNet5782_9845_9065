using System;
using DalObject;//do we need this?
using IDAL.DO;
namespace ConsoleUI
{
    public enum Actions { Exit, Add, Update, View, List }
    public enum Data { Station = 1, Drone, Customer, Parcel }
    
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
                    int id; string name; string phone;double longitude;double latitude;
                    Console.WriteLine("Enter 1 to add a station" +
                "Enter 2 to add a drone" +
                "Enter 3 to add a customer" +
                "Enter 4 to add a parcel" +
                "");
                    specific = (Data)Console.Read();
                    switch (specific)
                    {
                        case Data.Station://public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
                            Console.WriteLine("Enter id:");
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
                        case Data.Drone://public void AddDrone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
                            Console.WriteLine("Enter id:");
                            id = Console.Read();
                            Console.WriteLine("Enter model:");
                            string model = Console.ReadLine();
                            Console.WriteLine("Enter maximum weight- 1 for light, 2 for medium, 3 for heavy:");
                            WeightCategories maxWeight = (WeightCategories)Console.Read();
                            Console.WriteLine("Enter battey:");
                            double battery = Console.Read();
                            //Console.WriteLine("Enter latitude:");
                            //double latitude = Console.Read();
                            //project.AddCustomer(id, name, phone, longitude, latitude);
                            break;
                        case Data.Customer:
                            break;
                        case Data.Parcel:
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
