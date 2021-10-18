using System;
namespace ConsoleUI
{
    public enum Actions { Exit, Add, Update, View, List }
    public enum Data { Station = 1, Drone, Customer, Parcel }
    class Targil1
    {
        static void Main(string[] args)
        {
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
                    Console.WriteLine("Enter 1 to add a station" +
                "Enter 2 to add a drone" +
                "Enter 3 to add a customer" +
                "Enter 4 to add a parcel" +
                "");
                    specific = (Data)Console.Read();
                    switch (specific)
                    {
                        case Data.Station:
                            Console.WriteLine("enter ");
                            break;
                        case Data.Drone:
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
