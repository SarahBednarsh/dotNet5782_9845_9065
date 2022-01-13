using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalApi;
using DO;
using BO;
using BlApi;
using WeightCategories = BO.WeightCategories;
using System.Runtime.CompilerServices;

namespace BL
{
    sealed internal partial class BL : IBL
    {
        #region singleton
        private static IBL instance = null;
        private static object LOCK = new object(); //an object used to make sure the singleton is thread safe
        internal static IBL Instance
        {
            get
            {
                IBL localRef = instance;
                if (localRef == null)
                {
                    lock (LOCK)
                    {
                        if (instance == null)
                            instance = new BL();
                    }
                }
                return instance;
            }
        }
        #endregion

        internal IDal dalAP; // DAL access point
        internal static double available = 0;
        internal static double light = 0;
        internal static double medium = 0;
        internal static double heavy = 0;
        internal static double chargingPace = 0; //battery percentage per second
        internal List<DroneToList> dronesBL;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BL()
        {

            dalAP = DalFactory.GetDal("list");

            lock (dalAP)
            {
                IEnumerator<double> info = dalAP.ReqPowerConsumption().GetEnumerator();
                info.MoveNext();
                available = info.Current;
                info.MoveNext();
                light = info.Current;
                info.MoveNext();
                medium = info.Current;
                info.MoveNext();
                heavy = info.Current;
                info.MoveNext();
                chargingPace = info.Current;
                IEnumerable<DO.Parcel> parcels = dalAP.YieldParcel();
                IEnumerable<DO.Drone> drones = dalAP.YieldDrone();
                dronesBL = new List<DroneToList>();
                foreach (DO.Drone drone in drones) //initialize list of drones
                {
                    dronesBL.Add(new DroneToList { Id = drone.Id, Model = drone.Model, MaxWeight = (WeightCategories)drone.MaxWeight, Status = DroneStatuses.Available, IdOfParcel = -1 });
                }
                Random r = new Random();
                try
                {
                    List<DroneToList> tmp = new List<DroneToList>();
                    foreach (DroneToList drone in dronesBL)
                    {
                        //get all the parcels that were not delivered yet but attributed to a drone
                        var droneParcels = parcels.Where(p => p.DroneId == drone.Id && p.Delivered == null);
                        if (droneParcels.Count() > 0)
                        {
                            drone.IdOfParcel = droneParcels.FirstOrDefault().Id;//need to add the attribution somewhere
                            drone.Status = DroneStatuses.Delivering;
                            DO.Parcel parcel = droneParcels.FirstOrDefault();
                            if (parcel.PickedUp == null)//wasn't picked up
                            {
                                DO.Customer customer = dalAP.SearchCustomer(parcel.SenderId);
                                DO.Station closestS = GetClosestStation(LocationStaticClass.InitializeLocation(customer.Longitude, customer.Latitude));
                                Location closestStationLocation = LocationStaticClass.InitializeLocation(closestS.Longitude, closestS.Latitude);
                                drone.Location = closestStationLocation;
                            }
                            else //parcel was picked up
                            {
                                //get the sender of the parcel
                                DO.Customer customer = dalAP.YieldCustomer().Where(c => c.Id == parcel.SenderId).FirstOrDefault();
                                drone.Location = LocationStaticClass.InitializeLocation(customer.Longitude, customer.Latitude);
                            }
                            DO.Station closest = GetClosestStation(drone.Location);
                            Location closestLoc = LocationStaticClass.InitializeLocation(closest.Longitude, closest.Latitude);
                            //randomly chose battery between minimum for travel and full charge
                            int batteryForTravel = (int)(LocationStaticClass.CalcDis(drone.Location,
                                LocationStaticClass.InitializeLocation((dalAP.SearchCustomer(dalAP.SearchParcel(drone.IdOfParcel).TargetId)).Longitude, dalAP.SearchCustomer(dalAP.SearchParcel(drone.IdOfParcel).TargetId).Latitude)) * available) + (int)(LocationStaticClass.CalcDis(drone.Location, closestLoc) * available);

                            drone.Battery = batteryForTravel + r.Next(0, 100 - batteryForTravel) + r.NextDouble();

                        }
                        else //drone is not delivering
                        {
                            IEnumerable<DO.Customer> customers = dalAP.YieldCustomer();
                            int numCustomerWithDeliveredParcel = 0;
                            foreach (DO.Customer customer in customers)
                                if (HadAParcelDelivered(customer))
                                    numCustomerWithDeliveredParcel++;

                            if (r.Next(2) == 1 || numCustomerWithDeliveredParcel == 0)//makes it be in maintenence
                            {
                                drone.Status = DroneStatuses.InMaintenance;
                                IEnumerable<DO.Station> stations = dalAP.YieldStation();
                                int index = r.Next(stations.Count());
                                int counter = 0;
                                //set location of the drone to a random station
                                foreach (DO.Station station in stations)
                                {
                                    if (counter == index)
                                    {
                                        if (station.ChargeSlots != 0)
                                        {
                                            dalAP.DroneToCharge(drone.Id, station.Id);

                                        }
                                        else
                                        {
                                            drone.Status = DroneStatuses.Available; //as if it was released from charging
                                        }
                                        drone.Location = LocationStaticClass.InitializeLocation(station.Longitude, station.Latitude);
                                        break;
                                    }
                                    counter++;
                                }
                                drone.Battery = r.NextDouble() * 20;
                            }
                            else//drone is available
                            {
                                drone.Status = DroneStatuses.Available;


                                //set drone location at a random customer that has a parcel delivered
                                int index = r.Next(numCustomerWithDeliveredParcel);//customers that had parcels delivered to them
                                int counter = 0;
                                foreach (DO.Customer customer in customers)
                                {
                                    if (HadAParcelDelivered(customer))
                                    {
                                        if (counter == index)
                                        {
                                            drone.Location = LocationStaticClass.InitializeLocation(customer.Longitude, customer.Latitude);
                                            break;
                                        }
                                        counter++;
                                    }
                                }
                                //set battery
                                DO.Station closest = GetClosestStation(drone.Location);
                                Location closestLoc = LocationStaticClass.InitializeLocation(closest.Longitude, closest.Latitude);
                                int batteryForTravel = (int)(LocationStaticClass.CalcDis(drone.Location, closestLoc) * available);
                                drone.Battery = batteryForTravel + r.Next(0, 100 - batteryForTravel) + r.NextDouble();
                            }
                        }
                        tmp.Add(drone);
                    }
                    dronesBL = tmp;
                }
                catch (Exception ex)
                {
                    throw new InternalError("problem with drone initialization", ex);
                }
            }
        }
        private DO.Station GetClosestStation(Location loc)
        {
            lock (dalAP)
            {
                IEnumerable<DO.Station> stations = dalAP.YieldStation();
                Location location = LocationStaticClass.InitializeLocation(stations.FirstOrDefault().Longitude, stations.FirstOrDefault().Latitude);
                double minDistance = LocationStaticClass.CalcDis(location, loc);//will fill in
                DO.Station closest = stations.FirstOrDefault();
                foreach (DO.Station station in stations) //find station with minimal distance
                {
                    location = LocationStaticClass.InitializeLocation(station.Longitude, station.Latitude);
                    double dis = LocationStaticClass.CalcDis(location, loc);
                    if (minDistance > dis)
                    {
                        minDistance = dis;
                        closest = station;
                    }
                }
                return closest;
            }
        }
        private bool HadAParcelDelivered(DO.Customer customer)
        {
            bool hadDelivered = false;
            lock (dalAP)
            {
                foreach (DO.Parcel parcel in dalAP.YieldParcel())
                {
                    if ((parcel.Delivered != null) && (parcel.TargetId == customer.Id)) //had delivered
                        hadDelivered = true;
                }
            }
            return hadDelivered;
        }

    }
}
