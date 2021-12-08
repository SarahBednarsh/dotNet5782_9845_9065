using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public DateTime? BeginTime;
            public override string ToString()
            {
                return string.Format("DroneId: {0}, StationId: {1}", DroneId, StationId);
            }
        }
    }
}
