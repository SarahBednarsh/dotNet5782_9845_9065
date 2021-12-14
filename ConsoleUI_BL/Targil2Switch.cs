using System;
using BO;
using BlApi;
namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private static void SwitchAdd(IBL bl)
        {
            Data specific;
            Console.WriteLine("Enter 1 to add a station, " +
                              "Enter 2 to add a drone, " +
                              "Enter 3 to add a customer, " +
                              "Enter 4 to add a parcel:");
            Int32.TryParse(Console.ReadLine(), out int input);
            specific = (Data)input;
            switch (specific)
            {
                case Data.Station:
                    AddStation(bl);
                    break;
                case Data.Drone:
                    AddDrone(bl);
                    break;
                case Data.Customer:
                    AddCustomer(bl);
                    break;
                case Data.Parcel:
                    AddParcel(bl);
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
        private static void SwitchUpdate(IBL bl)
        {
            UpdateOption updateoption;
            Console.WriteLine("Enter 1 to change a drone's name, " +
                              "Enter 2 to change station details, " +
                              "Enter 3 to change customer details, " +
                              "Enter 4 to send a drone to charge, " +
                              "Enter 5 to release a drone from charging, " +
                              "Enter 6 to attribute a parcel to drone, " +
                              "Enter 7 to pick up a parcel, " +
                              "Enter 8 to deliver a parcel:");
            Int32.TryParse(Console.ReadLine(), out int input);
            updateoption = (UpdateOption)input;
            switch (updateoption)
            {
                //DroneName = 1, Station, Customer, SendToCharge, EndCharge, Attribute, Pickup, Deliver
                case UpdateOption.DroneName:
                    UpdateDroneModel(bl);
                    break;
                case UpdateOption.Station:
                    UpdateStation(bl);
                    break;
                case UpdateOption.Customer:
                    UpdateCustomer(bl);
                    break;
                case UpdateOption.SendToCharge:
                    UpdateSendToCharge(bl);
                    break;
                case UpdateOption.EndCharge:
                    UpdateEndCharge(bl);
                    break;
                case UpdateOption.Attribute:
                    UpdateAttribute(bl);
                    break;
                case UpdateOption.Pickup:
                    UpdatePickup(bl);
                    break;
                case UpdateOption.Deliver:
                    UpdateDeliver(bl);
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
        private static void SwitchView(IBL bl)
        {
            Data specific;
            Console.WriteLine("Enter 1 to display a station, " +
                              "Enter 2 to display a drone, " +
                              "Enter 3 to display a customer, " +
                              "Enter 4 to display a parcel:");
            Int32.TryParse(Console.ReadLine(), out int input);
            specific = (Data)input;
            switch (specific)
            {
                case Data.Station:
                    ViewStation(bl);
                    break;
                case Data.Drone:
                    ViewDrone(bl);
                    break;
                case Data.Customer:
                    ViewCustomer(bl);
                    break;
                case Data.Parcel:
                    ViewParcel(bl);
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
        private static void SwitchList(IBL bl)
        {
            Data specific;
            Console.WriteLine("Enter 1 to display list of stations, " +
                              "Enter 2 to display list of drones, " +
                              "Enter 3 to display list of customers, " +
                              "Enter 4 to display list of parcels, " +
                              "Enter 5 to display list of parcels that are not attributed, " +
                              "Enter 6 to display list of stations with availabe charge slots:");
            Int32.TryParse(Console.ReadLine(), out int input);
            specific = (Data)input;
            switch (specific)
            {
                case Data.Station:
                    ListStations(bl);
                    break;
                case Data.Drone:
                    ListDrones(bl);
                    break;
                case Data.Customer:
                    ListCustomers(bl);
                    break;
                case Data.Parcel:
                    ListParcels(bl);
                    break;
                case Data.ParcelNotAttributed:
                    ListParcelsNotAttributed(bl);
                    break;
                case Data.StationsWithAvailableChargers:
                    ListAvailableStations(bl);
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }
    }
}
