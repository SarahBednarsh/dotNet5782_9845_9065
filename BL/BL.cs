using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalObject;
using IDAL;
using IDAL.DO;

namespace IBL
{
    namespace BO
    {
        public partial class BL : IBL
        {
            internal IDal dalAP; // DAL access point
            internal static double available = 0;
            internal static double light = 0;
            internal static double medium = 0;
            internal static double heavy = 0;
            internal static double chargingPace = 0; //meters per second
            internal List<DroneToList> dronesBL;
            public BL()
            {
                dalAP = new DalObject.DalObject();
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
                IEnumerable<IDAL.DO.Parcel> parcels = dalAP.YieldParcel();
                IEnumerable<IDAL.DO.Drone> drones = dalAP.YieldDrone();
                dronesBL = new List<DroneToList>();
                foreach (IDAL.DO.Drone drone in drones) //initialize list of drones
                {
                    //DroneToList tmp = new DroneToList() { Id = drone.Id, Model = drone.Model, MaxWeight = (WeightCategories)drone.MaxWeight, Status = DroneStatuses.Available, IdOfParcel = -1 };
                    dronesBL.Add(new DroneToList { Id = drone.Id, Model = drone.Model, MaxWeight = (WeightCategories)drone.MaxWeight, Status = DroneStatuses.Available, IdOfParcel = -1 });
                }
                Random r = new Random();
                try
                {
                    List<DroneToList> tmp = new List<DroneToList>();
                    foreach (DroneToList drone in dronesBL)
                    {
                        //get all the parcels that were not delivered yet but attributed to a drone
                        var droneParcels = parcels.Where(p => p.DroneId == drone.Id && p.Delivered != DateTime.MinValue);
                        if (droneParcels.Count() > 0)//not sure about == 1
                        {
                            drone.IdOfParcel = droneParcels.FirstOrDefault().Id;//sarah-write explanation- need to add the attribution somewhere

                            drone.Status = DroneStatuses.Delivering;
                            IDAL.DO.Parcel parcel = droneParcels.FirstOrDefault();
                            if (parcel.PickedUp == DateTime.MinValue)//wasn't picked up
                            {
                                IDAL.DO.Customer customer = dalAP.SearchCustomer(parcel.SenderId);
                                IDAL.DO.Station closestS = GetClosestStation(LocationStaticClass.InitializeLocation(customer.Longitude, customer.Latitude));
                                Location closestStationLocation = LocationStaticClass.InitializeLocation(closestS.Longitude, closestS.Latitude);
                                drone.Location = closestStationLocation;
                            }
                            else //parcel was picked up
                            {
                                //get the sender of the parcel
                                IDAL.DO.Customer customer = dalAP.YieldCustomer().Where(c => c.Id == parcel.SenderId).FirstOrDefault();
                                drone.Location = LocationStaticClass.InitializeLocation(customer.Longitude, customer.Latitude);
                            }
                            IDAL.DO.Station closest = GetClosestStation(drone.Location);
                            Location closestLoc = LocationStaticClass.InitializeLocation(closest.Longitude, closest.Latitude);
                            //randomly chose battery between minimum for travel and full charge
                            int batteryForTravel = (int)(LocationStaticClass.CalcDis(drone.Location,
                                LocationStaticClass.InitializeLocation((dalAP.SearchCustomer(dalAP.SearchParcel(drone.IdOfParcel).TargetId)).Longitude, dalAP.SearchCustomer(dalAP.SearchParcel(drone.IdOfParcel).TargetId).Latitude)) * available) + (int)(LocationStaticClass.CalcDis(drone.Location, closestLoc) * available);//sarah-remove search customer

                            drone.Battery = batteryForTravel + r.Next(0, 100 - batteryForTravel) + r.NextDouble();

                        }
                        else //drone is not delivering
                        {
                            if (r.Next(2) == 1)//makes it be in maintenence
                            {
                                drone.Status = DroneStatuses.InMaintenance;
                                IEnumerable<IDAL.DO.Station> stations = dalAP.YieldStation();
                                int index = r.Next(stations.Count() + 1);//sarah-makes it start from 1
                                int counter = 0;
                                //set location of the drone to a random station
                                foreach (IDAL.DO.Station station in stations)
                                {
                                    counter++;
                                    if (counter == index)
                                    {
                                        drone.Location = LocationStaticClass.InitializeLocation(station.Longitude, station.Latitude);
                                        break;
                                    }
                                }
                                drone.Battery = r.NextDouble() * 20;
                            }
                            else//drone is available
                            {
                                drone.Status = DroneStatuses.Available;
                                IEnumerable<IDAL.DO.Customer> customers = dalAP.YieldCustomer();
                                int numCustomerWithDeliveredParcel = 0;
                                foreach (IDAL.DO.Customer customer in customers)
                                    if (HadAParcelDelivered(customer))
                                        numCustomerWithDeliveredParcel++;
                                //set drone location at a random customer that has a parcel delivered
                                int index = r.Next(numCustomerWithDeliveredParcel + 1);//customers that had parcels delivered to them
                                int counter = 0;
                                foreach (IDAL.DO.Customer customer in customers)
                                {
                                    if (HadAParcelDelivered(customer))
                                    {
                                        counter++;
                                        if (counter == index)
                                        {
                                            drone.Location = LocationStaticClass.InitializeLocation(customer.Longitude, customer.Latitude);
                                            break;
                                        }
                                    }
                                }
                                //set battery
                                IDAL.DO.Station closest = GetClosestStation(drone.Location);
                                Location closestLoc = LocationStaticClass.InitializeLocation(closest.Longitude, closest.Latitude);
                                int batteryForTravel = (int)(LocationStaticClass.CalcDis(drone.Location, closestLoc) * available);
                                drone.Battery = batteryForTravel + r.Next(0, 100 - batteryForTravel) + r.NextDouble();
                            }
                        }
                        tmp.Add(drone);
                    }
                    dronesBL = tmp;
                }
                catch (Exception exception)//some kind of exception was thrown
                {
                    Console.WriteLine("Problem with initializing the drones:");
                    Console.WriteLine(exception.Message);
                }
            }

            private IDAL.DO.Station GetClosestStation(Location loc)
            {
                IEnumerable<IDAL.DO.Station> stations = dalAP.YieldStation();
                Location location = LocationStaticClass.InitializeLocation(stations.FirstOrDefault().Longitude, stations.FirstOrDefault().Latitude);
                double minDistance = LocationStaticClass.CalcDis(location, loc);//will fill in
                IDAL.DO.Station closest = stations.FirstOrDefault();
                foreach (IDAL.DO.Station station in stations) //find station with minimal distance
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
            private bool HadAParcelDelivered(IDAL.DO.Customer customer)
            {
                bool hadDelivered = false;
                foreach (IDAL.DO.Parcel parcel in dalAP.YieldParcel())
                {
                    if ((parcel.Delivered != DateTime.MinValue) && (parcel.TargetId == customer.Id)) //had delivered
                        hadDelivered = true;
                }
                return hadDelivered;
            }

        }
    }
}
