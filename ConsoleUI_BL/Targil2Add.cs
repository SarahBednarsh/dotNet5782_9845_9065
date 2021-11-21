using System;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private static void AddStation(IBL.BO.IBL bl)
        {
            int id;
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out id);
            int numName;
            Console.WriteLine("Enter name (number):");
            Int32.TryParse(Console.ReadLine(), out numName);//handle entering a string (I think tryparse should be used)
            double longitude;
            Console.WriteLine("Enter longitude:");
            Double.TryParse(Console.ReadLine(), out longitude);
            double latitude;
            Console.WriteLine("Enter latitude:");
            Double.TryParse(Console.ReadLine(), out latitude);
            int chargeSlots;
            Console.WriteLine("Enter amount of charge slots:");
            Int32.TryParse(Console.ReadLine(), out chargeSlots);
            bl.AddStation(id, numName, longitude, latitude, chargeSlots);
        }
        private static void AddDrone(IBL.BO.IBL bl)
        {
            int id;
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out id);
            Console.WriteLine("Enter model:");
            string model = Console.ReadLine();
            int input;
            Console.WriteLine("Enter maximum weight- 1 for light, 2 for medium, 3 for heavy:");
            Int32.TryParse(Console.ReadLine(), out input);
            WeightCategories maxWeight = (WeightCategories)input;
            int stationID;
            Console.WriteLine("Enter station for initial charging:");
            Int32.TryParse(Console.ReadLine(), out stationID);
            bl.AddDrone(id, model, maxWeight, stationID);
        }
        private static void AddCustomer(IBL.BO.IBL bl)
        {
            int id;
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out id);
            string name;
            Console.WriteLine("Enter name:");
            name = Console.ReadLine();
            string phone;
            Console.WriteLine("Enter phone number:");
            phone = Console.ReadLine();
            double longitude;
            Console.WriteLine("Enter longitude:");
            Double.TryParse(Console.ReadLine(), out longitude);
            double latitude;
            Console.WriteLine("Enter latitude:");
            Double.TryParse(Console.ReadLine(), out latitude);
            bl.AddCustomer(id, name, phone, longitude, latitude);
        }
        private static void AddParcel(IBL.BO.IBL bl)
        {
            int senderId;
            Console.WriteLine("Enter sender ID:");
            Int32.TryParse(Console.ReadLine(), out senderId);
            int targetId;
            Console.WriteLine("Enter target ID:");
            Int32.TryParse(Console.ReadLine(), out targetId);
            int input;
            Console.WriteLine("Enter weight of parcel- 1 for light, 2 for medium, 3 for heavy:");
            Int32.TryParse(Console.ReadLine(), out input);
            WeightCategories weight = (WeightCategories)input;
            Console.WriteLine("Enter priority of parcel- 1 for normal, 2 for quick, 3 for emergency:");
            Int32.TryParse(Console.ReadLine(), out input);
            Priorities priority = (Priorities)input;
            bl.AddParcel(senderId, targetId, weight, priority);
        }
    }
}
