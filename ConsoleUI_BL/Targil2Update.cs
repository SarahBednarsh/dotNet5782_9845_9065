using System;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private static void UpdateDroneName(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter drone ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Enter new name:");
            string name = Console.ReadLine();
            bl.UpdateDroneName(id, name);
        }
        private static void UpdateStation(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter station number:");
            Int32.TryParse(Console.ReadLine(), out int num);
            Console.WriteLine("Enter station name (if not interested - enter -1):");
            string name = Console.ReadLine();
            Int32.TryParse(name, out int intName);
            if (intName == -1)
                name = "";
            Console.WriteLine("Enter number of charging slots (if not interested - enter -1):");
            Int32.TryParse(Console.ReadLine(), out int slots);
            bl.UpdateStationInfo(num, name, slots);
        }
        private static void UpdateCustomer(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Enter station name (if not interested - enter -1):");
            string name = Console.ReadLine();
            Int32.TryParse(name, out int intName);
            if (intName == -1)
                name = "";
            Console.WriteLine("Enter phone number (if not interested - enter -1):");
            string phone = Console.ReadLine();
            Int32.TryParse(name, out int intPhone);
            if (intPhone == -1)
                phone = "";
            bl.UpdateCustomerInfo(id, name, phone);
        }
        private static void UpdateSendToCharge(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            bl.DroneToCharge(id);
        }
        private static void UpdateEndCharge(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Enter charging time:");
            Int32.TryParse(Console.ReadLine(), out int timeCharging);
            bl.ReleaseCharging(id, timeCharging);
        }
        private static void UpdateAttribute(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter drone ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            bl.AttributeAParcel(id);
        }
        private static void UpdatePickup(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter drone ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            bl.PickUpAParcel(id);
        }
        private static void UpdateDeliver(IBL.BO.IBL bl)
        {
            Console.WriteLine("Enter drone ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            bl.DeliverAParcel(id);
        }
    }
}
