//using DalApi;
//using DO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Dal
//{
//    internal sealed partial class DalXml : IDal
//    {
//        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
//        {
//            throw new NotImplementedException();
//        }

//        public void AddDrone(int id, string model, WeightCategories maxWeight)
//        {
//            throw new NotImplementedException();
//        }

//        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, int droneId)
//        {
//            throw new NotImplementedException();
//        }

//        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
//        {
//            throw new NotImplementedException();
//        }

//        public double CalcDisFromCustomer(int id, double longitude, double latitude)
//        {
//            throw new NotImplementedException();
//        }

//        public double CalcDisFromStation(int id, double longitude, double latitude)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteCustomer(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteDrone(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteParcel(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeleteStation(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void DeliverToCustomer(int parcelId)
//        {
//            throw new NotImplementedException();
//        }

//        public void DroneToCharge(int droneId, int stationId)
//        {
//            throw new NotImplementedException();
//        }

//        public DateTime? GetBeginningChargeTime(int droneId)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Parcel> ListParcelConditional(Predicate<Parcel> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Station> ListStationConditional(Predicate<Station> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Station> OpenChargeSlots()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Parcel> ParcelsWithNoDrone()
//        {
//            throw new NotImplementedException();
//        }

//        public void PickUpParcel(int parcelId)
//        {
//            throw new NotImplementedException();
//        }

//        public void ReleaseCharging(int droneId)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<double> ReqPowerConsumption()
//        {
//            throw new NotImplementedException();
//        }

//        public void ScheduleParcel(int parcelId)
//        {
//            throw new NotImplementedException();
//        }

//        public Customer SearchCustomer(int customerId)
//        {
//            throw new NotImplementedException();
//        }

//        public Drone SearchDrone(int droneId)
//        {
//            throw new NotImplementedException();
//        }

//        public Parcel SearchParcel(int parcelId)
//        {
//            throw new NotImplementedException();
//        }

//        public Station SearchStation(int stationId)
//        {
//            throw new NotImplementedException();
//        }

//        public void UpdateParcelsDrone(int parcelId, int droneId)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Customer> YieldCustomer()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Drone> YieldDrone()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Parcel> YieldParcel()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Station> YieldStation()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
