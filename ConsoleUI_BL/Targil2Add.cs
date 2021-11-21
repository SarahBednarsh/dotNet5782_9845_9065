using System;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private static void AddStation(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Enter name (number):");
            Int32.TryParse(Console.ReadLine(), out int numName);//handle entering a string (I think tryparse should be used)
            Console.WriteLine("Enter longitude:");
            Double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("Enter latitude:");
            Double.TryParse(Console.ReadLine(), out double latitude);
            Console.WriteLine("Enter amount of charge slots:");
            Int32.TryParse(Console.ReadLine(), out int chargeSlots);
            bl.AddStation(id, numName, longitude, latitude, chargeSlots);
        }
        private static void AddDrone(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Enter model:");
            string model = Console.ReadLine();
            Console.WriteLine("Enter maximum weight- 1 for light, 2 for medium, 3 for heavy:");
            Int32.TryParse(Console.ReadLine(), out int input);
            WeightCategories maxWeight = (WeightCategories)input;
            Console.WriteLine("Enter station for initial charging:");
            Int32.TryParse(Console.ReadLine(), out int stationID);
            bl.AddDrone(id, model, maxWeight, stationID);
        }
        private static void AddCustomer(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter phone number:");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter longitude:");
            Double.TryParse(Console.ReadLine(), out double longitude);
            Console.WriteLine("Enter latitude:");
            Double.TryParse(Console.ReadLine(), out double latitude);
            bl.AddCustomer(id, name, phone, longitude, latitude);
        }
        private static void AddParcel(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter sender ID:");
            Int32.TryParse(Console.ReadLine(), out int senderId);
            Console.WriteLine("Enter target ID:");
            Int32.TryParse(Console.ReadLine(), out int targetId);
            Console.WriteLine("Enter weight of parcel- 1 for light, 2 for medium, 3 for heavy:");
            Int32.TryParse(Console.ReadLine(), out int input);
            WeightCategories weight = (WeightCategories)input;
            Console.WriteLine("Enter priority of parcel- 1 for normal, 2 for quick, 3 for emergency:");
            Int32.TryParse(Console.ReadLine(), out input);
            Priorities priority = (Priorities)input;
            bl.AddParcel(senderId, targetId, weight, priority);
        }
    }
}
