using System;
using System.Collections.Generic;
using System.Text;


namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            //public Customer(int id, string name, string phone, double longitude, double latitude)
            //{
            //    this.Id = id;
            //    this.Name = name;
            //    this.Phone = phone;
            //    this.Longitude = new Sexagesimal(longitude, "Longitude");
            //    this.Latitude = new Sexagesimal(latitude, "Latitude");
            //}
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Sexagesimal Longitude { get; set; }
            public Sexagesimal Latitude { get; set; }
            public override string ToString()
            {
                return string.Format("Id: {0}, Name: {1}, Phone: {2}, Longitude: {3}, Latitude: {4}", Id, Name, Phone, Longitude, Latitude);
            }
        }
    }
}

