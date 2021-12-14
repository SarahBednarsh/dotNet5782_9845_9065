using System;
using BO;
using BlApi;

namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private static void ViewStation(IBL bl)
        {
            Console.WriteLine("Enter station ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine(bl.SearchStation(id));
        }
        private static void ViewDrone(IBL bl)
        {
            Console.WriteLine("Enter drone ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine(bl.SearchDrone(id));
        }
        private static void ViewCustomer(IBL bl)
        {
            Console.WriteLine("Enter customer ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine(bl.SearchCustomer(id));
        }
        private static void ViewParcel(IBL bl)
        {
            Console.WriteLine("Enter parcel ID:");
            Int32.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine(bl.SearchParcel(id));
        }
    }
}
