using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; }
            public DroneStatuses Status { get; set; }
            public IDAL.DO.Sexagesimal Longitude { get; set; }//should delete
            public IDAL.DO.Sexagesimal Latitude { get; set; }//should delete
            public IDAL.DO.Coordinates Location  { get; set; }
            public int IdOfParcel;
            public DroneToList(IDAL.DO.Drone drone)
            {
                Id = drone.Id;
                Model = drone.Model;
                MaxWeight = drone.MaxWeight;
                Status = DroneStatuses.Available;
                IdOfParcel = -1;
            }
        }
    }
}
