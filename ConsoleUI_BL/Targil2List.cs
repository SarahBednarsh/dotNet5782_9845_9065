using System;
using System.Collections.Generic;
using IBL.BO;
using IBL;

namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private static void ListStations(IBL.BO.IBL bl)
        {
            IEnumerable<Station> stations = bl.YieldStation();
            foreach(Station station in stations)
                Console.WriteLine(station);
        }
        private static void ListDrones(IBL.BO.IBL bl)
        {
            IEnumerable<Drone> drones = bl.YieldDrone();
            foreach (Drone drone in drones)
                Console.WriteLine(drone);
        }
        private static void ListCustomers(IBL.BO.IBL bl)
        {
            IEnumerable<Customer> customers = bl.YieldCustomer();
            foreach (Customer customer in customers)
                Console.WriteLine(customer);
        }
        private static void ListParcels(IBL.BO.IBL bl)
        {
            IEnumerable<Parcel> parcels = bl.YieldParcel();
            foreach (Parcel parcel in parcels)
                Console.WriteLine(parcel);
        }
        private static void ListParcelsNotAttributed(IBL.BO.IBL bl)
        {
            IEnumerable<Parcel> parcelsNotAttributed = bl.YieldParcelNotAttributed();
            foreach (Parcel parcel in parcelsNotAttributed)
                Console.WriteLine(parcel);
        }
        private static void ListAvailableStations(IBL.BO.IBL bl)
        {
            IEnumerable<Station> stationsAvailable = bl.YieldStationAvailable();
            foreach (Station station in stationsAvailable)
                Console.WriteLine(station);
        }
    }
}
