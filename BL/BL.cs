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
                var droneParcels = parcels.Where(p => p.DroneId == drone.DroneInfo.Id && p.Delivered != DateTime.MinValue);
                if (droneParcels.Count() == 1)//not sure about == 1
                {
                    drone.Status = DroneStatuses.Delivering;
                    IDAL.DO.Parcel parcel = droneParcels.GetEnumerator().Current;
                    if (parcel.PickedUp == DateTime.MinValue)
                    {
                       IDAL.DO.Station closestStation= getClosestStation
                        drone.lattitude=
                    }
                }
            }
        }
        private IDAL.DO.Station getClosestStation(IDAL.DO.Customer customer)
        {
            IEnumerable<IDAL.DO.Station> stations= dalAP.YieldStation();
            double minDistance= stations.GetEnumerator().Current.location.CalcDis(customer.location);//will fill in
            IDAL.DO.Station closest=stations.GetEnumerator().Current;
            foreach (IDAL.DO.Station in stations)
            {
                if (minDistance > stations.GetEnumerator().Current.location.CalcDis(customer.location)) 
                { 
                    minDistance=stations.GetEnumerator().Current.location.CalcDis(customer.location);
                    closest=stations.GetEnumerator().Current;
                }
            }
            return closest;
        }
    }
}
