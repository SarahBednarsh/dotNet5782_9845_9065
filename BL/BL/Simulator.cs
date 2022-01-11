using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DalApi;
using BO;
using System.Threading;
using static System.Math;
using DO;
using Parcel = BO.Parcel;

namespace BL
{
    internal class Simulator
    {
        enum ChargingStages { Initial, Traveling, Charging, Waiting }
        private const double VELOCITY = 40;
        private const int DELAY_IN_MSEC = 500;
        private const double DISTANCE_ACCURACY = 0.01;
        private ChargingStages chargingStage = ChargingStages.Charging;
        private Location targetLocation;
        private double distanceFromTarget = 0;
        private DroneToList drone;
        private double batteryUsage;

        public Simulator(BL bl, int droneId, Action update, Func<bool> stop)
        {
            lock (bl)
            { drone = bl.GetReferenceDroneToList(droneId); }
            do
            {
                switch (drone)
                {
                    case DroneToList { Status: DroneStatuses.Available }:
                        handleAvailable(bl);
                        break;
                    case DroneToList { Status: DroneStatuses.Delivering }:
                        handleDelivering(bl);
                        break;
                    case DroneToList { Status: DroneStatuses.InMaintenance }:
                        handleInMaintenance(bl);
                        break;
                    default:
                        break;
                }
                update();
            } while (!stop());
        }


        private void handleAvailable(BL bl)
        {
            if (!Delay())
                return;
            lock (bl)
            {
                try { bl.AttributeAParcel(drone.Id); }
                catch (CannotAttribute)
                {
                    if (drone.Battery == 100)
                        return;
                    drone.Status = DroneStatuses.InMaintenance;
                    chargingStage = ChargingStages.Initial;
                }
            }
        }
        private void handleDelivering(BL bl)
        {
            if (!Delay())
                return;
            lock (bl)
            {
                Parcel parcel = bl.SearchParcel(drone.IdOfParcel);
                bool pickedUp = parcel.PickUp is not null;
                targetLocation = pickedUp ? bl.GetTargetLocation(parcel) : bl.GetSenderLocation(parcel);
                if(pickedUp)
                { 
                    switch (parcel.Weight)
                    {
                        case BO.WeightCategories.Heavy:
                            batteryUsage = BL.heavy;
                            break;
                        case BO.WeightCategories.Medium:
                            batteryUsage = BL.medium;
                            break;
                        case BO.WeightCategories.Light:
                            batteryUsage = BL.light;
                            break;
                        default:
                            batteryUsage = BL.available;
                            break;
                    }
                }
                else
                    batteryUsage = BL.available;
                Travel();
                if (distanceFromTarget == 0)
                {
                    if (!pickedUp)
                        bl.PickUpAParcel(drone.Id);
                    else
                    {
                        bl.DeliverAParcel(drone.Id);
                        batteryUsage = bl.GetUsage(parcel.Weight);
                    }
                }
            }
        }
        private void handleInMaintenance(BL bl)
        {
            if (!Delay())
                return;
            switch (chargingStage)
            {
                case ChargingStages.Initial:
                    try
                    {
                        lock (bl)
                            lock (bl.dalAP)
                            {
                                Location currentLoc = drone.Location;
                                double currentBattery = drone.Battery;
                                drone.Status = DroneStatuses.Available;
                                bl.DroneToCharge(drone.Id);
                                drone.Location = currentLoc;
                                drone.Battery = currentBattery;
                                chargingStage = ChargingStages.Traveling;
                                int stationId = bl.dalAP.YieldDroneCharges().Where(x => x.DroneId == drone.Id).FirstOrDefault().StationId;
                                Location stationLoc = bl.SearchStation(stationId).Location;
                                targetLocation = stationLoc;
                                distanceFromTarget = LocationStaticClass.CalcDis(drone.Location, stationLoc);
                                batteryUsage = BL.available;
                            }
                    }
                    catch (Exception ex) when (ex is CannotSendToCharge || ex is NotEnoughBattery)
                    {
                        drone.Status = DroneStatuses.InMaintenance;
                        chargingStage = ChargingStages.Waiting;
                    }
                    break;
                case ChargingStages.Traveling:
                    Travel();
                    if (distanceFromTarget == 0)
                    {
                        chargingStage = ChargingStages.Charging;
                    }
                    break;
                case ChargingStages.Charging:
                    double timePassed = (double)DELAY_IN_MSEC / 1000;
                    drone.Battery += BL.chargingPace * timePassed;
                    drone.Battery = Min(drone.Battery, 100);
                    if (drone.Battery == 100)
                        lock (bl)
                        { bl.ReleaseCharging(drone.Id); }
                    break;
                case ChargingStages.Waiting:
                    chargingStage = ChargingStages.Initial;
                    break;
                default:
                    break;
            }

        }
        private static bool Delay()
        {
            try { Thread.Sleep(DELAY_IN_MSEC); }
            catch (ThreadInterruptedException)
            { return false; }
            return true;
        }
        private void Travel()
        {
            distanceFromTarget = LocationStaticClass.CalcDis(drone.Location, targetLocation);
            if (distanceFromTarget < DISTANCE_ACCURACY)
            {
                distanceFromTarget = 0;
                drone.Location = targetLocation;
                return;
            }
            double timePassed = (double)DELAY_IN_MSEC / 1000;
            double distanceChange = VELOCITY * timePassed;
            double change = Min(distanceChange, distanceFromTarget);
            double proportionalChange = change / distanceFromTarget;
            drone.Battery = Max(0.0, drone.Battery - change * batteryUsage);
            double droneLat = StaticSexagesimal.ParseDouble(drone.Location.Latitude);
            double droneLong = StaticSexagesimal.ParseDouble(drone.Location.Longitude);
            double targetLat = StaticSexagesimal.ParseDouble(targetLocation.Latitude);
            double targetLong = StaticSexagesimal.ParseDouble(targetLocation.Longitude);
            double lat = droneLat + (targetLat - droneLat) * proportionalChange;
            double lon = droneLong + (targetLong - droneLong) * proportionalChange;
            drone.Location = LocationStaticClass.InitializeLocation(lon, lat);
            distanceFromTarget = LocationStaticClass.CalcDis(drone.Location, targetLocation);
        }
    }
}
