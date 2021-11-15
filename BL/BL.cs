using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalObject;
using IDAL;
using IDAL.DO;
using IBL.BO;

namespace IBL
{
    //should this be in namespace BO?
    public partial class BL : IBL
    {
        internal IDal dalAP; // DAL access point
        internal static double available = 0;
        internal static double light = 0;
        internal static double medium = 0;
        internal static double heavy = 0;
        internal static double chargingPace = 0;

        public BL()
        {
            dalAP = new DalObject.DalObject();
            IEnumerator<double> info = dalAP.ReqPowerConsumption().GetEnumerator();
            available = info.Current;
            info.MoveNext();
            light = info.Current;
            info.MoveNext();
            medium = info.Current;
            info.MoveNext();
            heavy = info.Current;
            info.MoveNext();
            chargingPace = info.Current;
            IEnumerable<IDAL.DO.Parcel> parcels = dalAP.YieldParcel();
            IEnumerable<IDAL.DO.Drone> drones = dalAP.YieldDrone();
            List<DroneToList> dronesBL = new List<DroneToList>();
            foreach(IDAL.DO.Drone drone in drones)
            {
                dronesBL.Add(new DroneToList(drone));
            }
            Random r = new Random();
            foreach (DroneToList drone in dronesBL)
            {
                var droneParcels = parcels.Where(p => p.DroneId == drone.Id && p.Delivered != DateTime.MinValue);
                if (droneParcels.Count() == 1)//not sure about == 1
                {
                    drone.Status = DroneStatuses.Delivering;
                    IDAL.DO.Parcel parcel = droneParcels.GetEnumerator().Current;
                    if (parcel.PickedUp == DateTime.MinValue)//wasn't picked up
                    {
                        IDAL.DO.Coordinates closestStationLocation = getClosestStation(dalAP.SearchCustomer(parcel.SenderId).Location).Location;
                        drone.Location=closestStationLocation;
                    }
                    else
                    {
                        drone.Location=dalAP.YieldCustomer().Where(c=>c.Id==parcel.SenderId).GetEnumerator().Current.Location;
                    }
                    drone.Battery = r.Next(0, 20);
                }
                else
                {
                    if (r.Next(2)==1)//makes it be in maintenence
                    {
                        IEnumerable < IDAL.DO.Station> stations= dalAP.YieldStation();
                        int index = r.Next(stations.Count());
                        int counter = 0;
                        foreach (IDAL.DO.Station station in stations)
                        {
                            if (counter==index)
                            {
                                drone.Location = station.Location;
                                break;
                            }
                            counter++;
                        }
                        drone.Battery = r.NextDouble() / 5;
                    }
                    else//drone is available
                    {
                        IEnumerable<IDAL.DO.Customer> customers = dalAP.YieldCustomer();
                        int numCustomerWithDeliveredParcel = 0;
                        foreach (IDAL.DO.Customer customer in customers)
                            if (hadAParcelDelivered(customer))
                                numCustomerWithDeliveredParcel++;
                        int index = r.Next(numCustomerWithDeliveredParcel);//customers that had parcels delivered to them
                        int counter = 0;
                        foreach (IDAL.DO.Customer customer in customers)
                        {
                            if (hadAParcelDelivered(customer))
                            {
                                if (counter == index)
                                {
                                    drone.Location = customer.Location;
                                    break;
                                }
                                counter++;
                            }
                        }
                        int batteryForTravel = (int)(drone.Location.CalcDis(getClosestStation(drone.Location).Location) * available);
                        drone.Battery = batteryForTravel + r.Next(0, 100 - batteryForTravel) + r.NextDouble();
                    }
                }
            }
        }
        private IDAL.DO.Station getClosestStation(Coordinates loc)
        {
            IEnumerable<IDAL.DO.Station> stations= dalAP.YieldStation();
            double minDistance= stations.GetEnumerator().Current.Location.CalcDis(loc);//will fill in
            IDAL.DO.Station closest=stations.GetEnumerator().Current;
            foreach (IDAL.DO.Station station in stations)
            {
                if (minDistance > station.Location.CalcDis(loc)) 
                { 
                    minDistance=station.Location.CalcDis(loc);
                    closest = station;
                }
            }
            return closest;
        }
        private bool hadAParcelDelivered(IDAL.DO.Customer customer)
        {
            foreach(IDAL.DO.Parcel parcel in dalAP.YieldParcel())
            {
                if ((parcel.Delivered != DateTime.MinValue) && (parcel.TargetId == customer.Id))
                    return true;
            }
            return false;
        }
    }
}
