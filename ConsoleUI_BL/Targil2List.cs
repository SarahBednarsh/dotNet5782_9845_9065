using System;
using System.Collections.Generic;
using BO;
using BlApi;

namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private static void ListStations(IBL bl)
        {
            IEnumerable<StationToList> stations = bl.ListStation();
            foreach(StationToList station in stations)
                Console.WriteLine(string.Format("{0} \n", station));
        }
        private static void ListDrones(IBL bl)
        {
            IEnumerable<DroneToList> drones = bl.ListDrone();
            foreach (DroneToList drone in drones)
                Console.WriteLine(string.Format("{0} \n", drone));
        }
        private static void ListCustomers(IBL bl)
        {
            IEnumerable<CustomerToList> customers = bl.ListCustomer();
            foreach (CustomerToList customer in customers)
                Console.WriteLine(string.Format("{0} \n", customer));
        }
        private static void ListParcels(IBL bl)
        {
            IEnumerable<ParcelToList> parcels = bl.ListParcel();
            foreach (ParcelToList parcel in parcels)
                Console.WriteLine(string.Format("{0} \n", parcel));
        }
        private static void ListParcelsNotAttributed(IBL bl)
        {
            IEnumerable<ParcelToList> parcelsNotAttributed = bl.ListParcelNotAttributed();
            foreach (ParcelToList parcel in parcelsNotAttributed)
                Console.WriteLine(string.Format("{0} \n", parcel));
        }
        private static void ListAvailableStations(IBL bl)
        {
            IEnumerable<StationToList> stationsAvailable = bl.ListStationAvailable();
            foreach (StationToList station in stationsAvailable)
                Console.WriteLine(string.Format("{0} \n", station));
        }
    }
}
