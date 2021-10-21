﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public DroneCharge(int droneId, int stationId)
            {
                DroneId = droneId;
                StationId = stationId;
            }
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public override string ToString()
            {
                return string.Format("DroneId: {0}, StationId: {1}", DroneId, StationId);
            }
        }
    }
}