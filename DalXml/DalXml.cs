using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal sealed partial class DalXml : IDal
    {
        #region singleton
        private static DalXml instance = null;
        static DalXml() { }
        internal static DalXml Instance
        {
            get
            {
                DalXml localRef = instance;
                if (localRef == null)
                {
                    object Lock = new object();
                    lock (Lock)
                    {
                        if (instance == null)
                            instance = new DalXml();
                    }
                }
                return instance;
            }
        }
        #endregion

        #region xml file paths
        private readonly string dronesPath = @"DronesXml.xml"; //XElement
        private readonly string stationsPath = @"StationsXml.xml"; //XMLSerializer
        private readonly string customersPath = @"CustomersXml.xml"; //XMLSerializer
        private readonly string parcelsPath = @"ParcelsXml.xml"; //XMLSerializer
        private readonly string droneChargesPath = @"DroneChargesXml.xml"; //XMLSerializer
        private readonly string usersPath = @"UsersXml.xml"; //XMLSerializer
        private readonly string batteryCunsumptionPath = @"BatteryCunsumptionXml.xml"; //XElement
        private readonly string runningNumbersPath = @"RunningNumbersXml.xml"; //XMLSerializer
        private readonly string defaultsPath = @"DefaultsXml.xml"; //XMLElement
        #endregion

        #region Drones XElement
        public void AddDrone(int id, string model, WeightCategories maxWeight)
        {
            XElement dronesRootElem = XmlTools.LoadListFromXMLElement(dronesPath);

            XElement drone = (from b in dronesRootElem.Elements()
                              where b.Element("Id").Value == id.ToString()
                              select b).FirstOrDefault();

            if (drone != null)
                throw new DroneException("Drone already exists");

            XElement droneElem = new XElement("Drone", new XElement("Id", id.ToString()),
                                  new XElement("Model", model),
                                  new XElement("MaxWeight", maxWeight.ToString()));

            dronesRootElem.Add(droneElem);

            XmlTools.SaveListToXMLElement(dronesRootElem, dronesPath);
        }
        public void DeleteDrone(int id)
        {
            XElement dronesRootElem = XmlTools.LoadListFromXMLElement(dronesPath);

            XElement drone = (from b in dronesRootElem.Elements()
                              where b.Element("Id").Value == id.ToString()
                              select b).FirstOrDefault();

            if (drone == null)
                throw new DroneException("Drone doesn't exist");
            drone.Remove();
            XmlTools.SaveListToXMLElement(dronesRootElem, dronesPath);
        }
        public void DroneToCharge(int droneId, int stationId)
        {
            XElement dronesRootElem = XmlTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in dronesRootElem.Elements()
                              where b.Element("Id").Value == droneId.ToString()
                              select b).FirstOrDefault();
            if (drone == null)
                throw new DroneException("Drone doesn't exist");

            XElement stationsRootElem = XmlTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationsRootElem.Elements()
                                where b.Element("Id").Value == stationId.ToString()
                                select b).FirstOrDefault();
            if (station == null)
                throw new StationException("Station doesn't exist");

            int.TryParse(station.Element("ChargeSlots").Value, out int chargeSlots);
            station.Element("ChargeSlots").Value = (chargeSlots - 1).ToString();
            XmlTools.SaveListToXMLElement(stationsRootElem, stationsPath);

            XElement droneChargesRootElem = XmlTools.LoadListFromXMLElement(droneChargesPath);
            XElement droneCharge = new XElement("DroneCharge", new XElement("DroneId", droneId.ToString()),
                                                                new XElement("StationId", stationId.ToString()),
                                                                new XElement("BeginTime", DateTime.Now.ToString("O")));
            droneChargesRootElem.Add(droneCharge);
            XmlTools.SaveListToXMLElement(droneChargesRootElem, droneChargesPath);
        }
        public void ReleaseCharging(int droneId)
        {
            XElement dronesRootElem = XmlTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in dronesRootElem.Elements()
                              where b.Element("Id").Value == droneId.ToString()
                              select b).FirstOrDefault();
            if (drone == null)
                throw new DroneException("Drone to release doesn't exist");

            XElement droneChargesRootElem = XmlTools.LoadListFromXMLElement(droneChargesPath);
            XElement droneCharge = (from b in droneChargesRootElem.Elements()
                                    where b.Element("DroneId").Value == droneId.ToString()
                                    select b).FirstOrDefault();
            int.TryParse(droneCharge.Element("StationId").Value, out int stationId);

            XElement stationsRootElem = XmlTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationsRootElem.Elements()
                                where b.Element("Id").Value == stationId.ToString()
                                select b).FirstOrDefault();
            if (station == null)
                throw new StationException("Station doesn't exist");

            int.TryParse(station.Element("ChargeSlots").Value, out int chargeSlots);
            station.Element("ChargeSlots").Value = (chargeSlots + 1).ToString();
            XmlTools.SaveListToXMLElement(stationsRootElem, stationsPath);

            droneCharge.Remove();
            XmlTools.SaveListToXMLElement(droneChargesRootElem, droneChargesPath);
        }

        public DateTime? GetBeginningChargeTime(int droneId)
        {
            XElement droneChargesRootElem = XmlTools.LoadListFromXMLElement(droneChargesPath);
            XElement droneCharge = (from b in droneChargesRootElem.Elements()
                                    where b.Element("DroneId").Value == droneId.ToString()
                                    select b).FirstOrDefault();
            if (droneCharge == null)
                throw new DroneException("No such drone in charging");
            return DateTime.Parse(droneCharge.Element("BeginTime").Value);
        }
        public Drone SearchDrone(int droneId)
        {
            XElement dronesRootElem = XmlTools.LoadListFromXMLElement(dronesPath);
            Drone drone = (from d in dronesRootElem.Elements()
                           where d.Element("Id").Value == droneId.ToString()
                           select new Drone()
                           {
                               Id = int.Parse(d.Element("Id").Value),
                               Model = d.Element("Model").Value,
                               MaxWeight = (WeightCategories)int.Parse(d.Element("MaxWeight").Value)
                           }).FirstOrDefault();
            return drone;
        }
        public IEnumerable<Drone> YieldDrone()
        {
            XElement dronesRootElem = XmlTools.LoadListFromXMLElement(dronesPath);
            return from d in dronesRootElem.Elements()
                   select new Drone()
                   {
                       Id = int.Parse(d.Element("Id").Value),
                       Model = d.Element("Model").Value,
                       MaxWeight = (WeightCategories)int.Parse(d.Element("MaxWeight").Value)
                   };
        }
        #endregion

        #region Stations XMLSerializer
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (stations.Exists(x => x.Id == id))
                throw new StationException("Station to add already exists");
            Station tempStation = new Station() { Id = id, Name = name, Longitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(latitude, "Latitude"), ChargeSlots = chargeSlots };
            stations.Add(tempStation);
            XmlTools.SaveListToXMLSerializer(stations, stationsPath);
        }
        public Station SearchStation(int stationId)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (!stations.Exists(x => x.Id == stationId))
                throw new StationException("Station does not exist");
            return stations.Find(x => x.Id == stationId);
        }
        public void DeleteStation(int id)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (!stations.Exists(x => x.Id == id))
                throw new StationException("Station to delete does not exist");
            stations.RemoveAll(x => x.Id == id);
            XmlTools.SaveListToXMLSerializer(stations, stationsPath);
        }
        public IEnumerable<Station> ListStationConditional(Predicate<Station> predicate)
        {
            return from station in XmlTools.LoadListFromXMLSerializer<Station>(stationsPath)
                   where predicate(station)
                   select station;
        }
        public IEnumerable<Station> OpenChargeSlots()
        {
            return from station in XmlTools.LoadListFromXMLSerializer<Station>(stationsPath)
                   where station.ChargeSlots > 0
                   select station;
        }
        public double CalcDisFromStation(int id, double longitude, double latitude)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (!stations.Exists(x => x.Id == id))
                throw new CustomerException("Station does not exist.");
            Station station = stations.Find(x => x.Id == id);
            double clong = StaticSexagesimal.ParseDouble(station.Longitude);
            double clat = StaticSexagesimal.ParseDouble(station.Latitude);
            return StaticSexagesimal.CalcDis(clong, clat, longitude, latitude);
        }
        public IEnumerable<Station> YieldStation()
        {
            return XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
        }
        public IEnumerable<DroneCharge> YieldDroneCharges()
        {
            return XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargesPath);
        }
        #endregion

        #region Customers XMLSerializer
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            List<Customer> customers = XmlTools.LoadListFromXMLSerializer<Customer>(customersPath);
            if (customers.Exists(x => x.Id == id))
                throw new CustomerException("Customer to add already exists");
            Customer tempCustomer = new Customer() { Id = id, Name = name, Phone = phone, Longitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(latitude, "Latitude") };
            customers.Add(tempCustomer);
            XmlTools.SaveListToXMLSerializer(customers, customersPath);
        }
        public void DeleteCustomer(int id)
        {
            List<Customer> customers = XmlTools.LoadListFromXMLSerializer<Customer>(customersPath);
            if (!customers.Exists(x => x.Id == id))
                throw new CustomerException("Customer to delete does not exist");
            customers.RemoveAll(x => x.Id == id);
            XmlTools.SaveListToXMLSerializer(customers, customersPath);
        }
        public Customer SearchCustomer(int customerId)
        {
            List<Customer> customers = XmlTools.LoadListFromXMLSerializer<Customer>(customersPath);
            if (!customers.Exists(x => x.Id == customerId))
                throw new CustomerException("Customer does not exist");
            return customers.Find(x => x.Id == customerId);
        }
        public double CalcDisFromCustomer(int id, double longitude, double latitude)
        {
            List<Customer> customers = XmlTools.LoadListFromXMLSerializer<Customer>(customersPath);
            if (!customers.Exists(x => x.Id == id))
                throw new CustomerException("Customer does not exist.");
            Customer customer = customers.Find(x => x.Id == id);
            double clong = StaticSexagesimal.ParseDouble(customer.Longitude);
            double clat = StaticSexagesimal.ParseDouble(customer.Latitude);
            return StaticSexagesimal.CalcDis(clong, clat, longitude, latitude);
        }
        public IEnumerable<Customer> YieldCustomer()
        {
            return XmlTools.LoadListFromXMLSerializer<Customer>(customersPath);
        }
        #endregion

        #region Parcels XMLSerializer
        public int AddParcel(int senderId, int targetId, WeightCategories weight, Priorities priority, int droneId)
        {
            List<Parcel> parcels = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            List<int> runningNumbers = XmlTools.LoadListFromXMLSerializer<int>(runningNumbersPath);
            int runningParcelNumber = runningNumbers[0]++;
            XmlTools.SaveListToXMLSerializer(runningNumbers, runningNumbersPath);
            Parcel tempParcel = new Parcel() { Id = runningParcelNumber, SenderId = senderId, TargetId = targetId, Weight = weight, Priority = priority, Requested = DateTime.Now, DroneId = droneId, Scheduled = null, Delivered = null, PickedUp = null };
            parcels.Add(tempParcel);
            XmlTools.SaveListToXMLSerializer(parcels, parcelsPath);
            return runningParcelNumber;
        }
        public void DeleteParcel(int id)
        {
            List<Parcel> parcels = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (!parcels.Exists(x => x.Id == id))
                throw new ParcelException("Parcel to delete does not exist");
            parcels.RemoveAll(x => x.Id == id);
            XmlTools.SaveListToXMLSerializer(parcels, parcelsPath);
        }
        public Parcel SearchParcel(int parcelId)
        {
            List<Parcel> parcels = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (!parcels.Exists(x => x.Id == parcelId))
                throw new ParcelException("Parcel does not exist");
            return parcels.Find(x => x.Id == parcelId);
        }
        public void UpdateParcelsDrone(int parcelId, int droneId)
        {
            List<Drone> drones = XmlTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (!drones.Exists(x => x.Id == droneId))
                throw new DroneException("Requested drone was not found");
            List<Parcel> parcels = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            int indexParcel = parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)
                throw new ParcelException("Parcel to update does not exist.");
            Parcel tempParcel = parcels[indexParcel];
            tempParcel.DroneId = droneId;
            parcels[indexParcel] = tempParcel;
            XmlTools.SaveListToXMLSerializer(parcels, parcelsPath);

        }
        public void ScheduleParcel(int parcelId)
        {
            List<Parcel> parcels = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            int indexParcel = parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)//if parcel doesn't exist
                throw new ParcelException("Parcel to schedule does not exist");
            Parcel tempParcel = parcels[indexParcel];
            tempParcel.Scheduled = DateTime.Now;
            parcels[indexParcel] = tempParcel;
            XmlTools.SaveListToXMLSerializer(parcels, parcelsPath);
        }
        public void PickUpParcel(int parcelId)
        {
            List<Parcel> parcels = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            int indexParcel = parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)//if parcel doesn't exist
                throw new ParcelException("Parcel to pick up does not exist.");
            Parcel tempParcel = parcels[indexParcel];
            tempParcel.PickedUp = DateTime.Now;
            parcels[indexParcel] = tempParcel;
            XmlTools.SaveListToXMLSerializer(parcels, parcelsPath);
        }
        public void DeliverToCustomer(int parcelId)
        {
            List<Parcel> parcels = XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            int indexParcel = parcels.FindIndex(x => x.Id == parcelId);
            if (indexParcel == -1)
                throw new ParcelException("Parcel to deliver does not exist.");
            Parcel tempParcel = parcels[indexParcel];
            tempParcel.Delivered = DateTime.Now;
            parcels[indexParcel] = tempParcel;
            XmlTools.SaveListToXMLSerializer(parcels, parcelsPath);
        }
        public IEnumerable<Parcel> ParcelsWithNoDrone()
        {
            return from parcel in XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath)
                   where parcel.DroneId == 0
                   select parcel;
        }
        public IEnumerable<Parcel> ListParcelConditional(Predicate<Parcel> predicate)
        {
            return from parcel in XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath)
                   where predicate(parcel)
                   select parcel;
        }
        public IEnumerable<Parcel> YieldParcel()
        {
            return XmlTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
        }
        #endregion

        #region Users XMLSerializer
        public void AddUser(int id, string userName, string photo, string email, string password, bool isManager)
        {
            List<User> users = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            if (users.Exists(x => x.Id == id || x.UserName == userName))
                throw new UserException("User with the same id or username already exists");
            //if (!File.Exists(photo))
            //    photo = GetDefaultPhoto();
            int salt = PasswordHandler.GenerateSalt();
            //string man = isManager ? "Manager" : "Customer";
            //string photoPath = $@"..\..\..\{man}Photos\" + id + @".jpg";
            //(File.Create(photoPath)).Close();
            //System.IO.File.Copy(photo, photoPath, true);
            User tempUser = new User()
            {
                Id = id,
                UserName = userName,
                Photo = photo,
                Email = email,
                Salt = salt,
                HashedPassword = PasswordHandler.GenerateNewPassword(password, salt),
                IsManager = isManager
            };
            users.Add(tempUser);
            XmlTools.SaveListToXMLSerializer(users, usersPath);
        }
        public void DeleteUser(int id)
        {
            List<User> users = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            if (!users.Exists(x => x.Id == id))
                throw new UserException("no such user");
            users.RemoveAll(x => x.Id == id);
            XmlTools.SaveListToXMLSerializer(users, usersPath);
        }
        public User SearchUser(string userName)
        {
            List<User> users = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            if (!users.Exists(x => x.UserName == userName))
                throw new UserException("Cannot find user");
            return users.Find(x => x.UserName == userName);
        }
        public bool UserInfoCorrect(string userName, string password, bool isManager)
        {
            List<User> users = XmlTools.LoadListFromXMLSerializer<User>(usersPath);
            return users.Exists(x => x.UserName == userName && x.IsManager == isManager && PasswordHandler.CheckPassword(password, x.HashedPassword, x.Salt));

        }
        #endregion

        #region Battery Consumption Data XElement
        public IEnumerable<double> ReqPowerConsumption()
        {
            XElement batteryConsumption = XmlTools.LoadListFromXMLElement(batteryCunsumptionPath);
            return from consumption in batteryConsumption.Elements()
                   select double.Parse(consumption.Value);
        }
        #endregion
        
        #region Defaults XElement
        public string GetDefaultPhoto()
        {
            XElement defaultsRoot = XmlTools.LoadListFromXMLElement(defaultsPath);
            return (from def in defaultsRoot.Elements()
                    select def.Value).FirstOrDefault();
        }
        #endregion
    }
}
