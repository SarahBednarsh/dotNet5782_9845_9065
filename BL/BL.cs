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
            foreach (DroneToList drone in dronesBL)
            {
                var droneParcels = parcels.Where(p => p.DroneId == drone.Id && p.Delivered != DateTime.MinValue);
                if (droneParcels.Count() == 1)//not sure about == 1
                {
                    drone.Status = DroneStatuses.Delivering;
                    IDAL.DO.Parcel parcel = droneParcels.GetEnumerator().Current;
                    if (parcel.PickedUp == DateTime.MinValue)
                    {
                        IDAL.DO.Station closestStation= getClosestStation();
                        drone.location=Station.location;
                    }
                    else
                    {
                        drone.location=dalAP.YieldCustomer().Find(c=>c.Id==parcel.SenderId).location;
                    }
                    //make battery random between min to close charging station to max
                }
                else
                {
                    Random r = new Random();
                    if (r.Next(2)==1)//makes it be in maintenence
                    {
                        IEnumerable < IDAL.DO.Station> stations= dalAP.YieldStation();
                        int index = r.Next(stations.Count());
                        int counter = 0;
                        foreach (IDAL.DO.Station station in stations)
                        {
                            if (counter==index)
                            {
                                drone.location = station.location;
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
                                    drone.location = customer.location;
                                    break;
                                }
                                counter++;
                            }
                        }
                        drone.Battery=r.NextDouble()*                    }
                }
            }
        }
        private IDAL.DO.Station getClosestStation(IDAL.DO.Customer customer)
        {
            IEnumerable<IDAL.DO.Station> stations= dalAP.YieldStation();
            double minDistance= stations.GetEnumerator().Current.location.CalcDis(customer.location);//will fill in
            IDAL.DO.Station closest=stations.GetEnumerator().Current;
            foreach (IDAL.DO.Station station in stations)
            {
                if (minDistance > station.location.CalcDis(customer.location)) 
                { 
                    minDistance=station.location.CalcDis(customer.location);
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
