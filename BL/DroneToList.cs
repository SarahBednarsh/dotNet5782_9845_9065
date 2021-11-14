using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public IDAL.DO.Drone DroneInfo { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public IDAL.DO.Sexagesimal Longitude { get; set; }
            public IDAL.DO.Sexagesimal Latitude { get; set; }
            public DroneToList(IDAL.DO.Drone drone)
            {
                DroneInfo = drone;
                Status = DroneStatuses.Available;
            }
        }
    }
}
