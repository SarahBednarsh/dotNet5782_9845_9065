using System;
using System.Collections.Generic;
using System.Text;
using DalObject;
using IDAL;
using IDAL.DO;

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
        internal double chargingPace = 0;

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
            IEnumerable<Parcel> parcels = dalAP.YieldParcel();
            IEnumerable<Drone> drones = dalAP.YieldDrone();
            foreach(Parcel parcel in parcels)
            {
                if(dalAP.SearchDrone(parcel.DroneId) /*exists*/)

            }
        }

    }
}
