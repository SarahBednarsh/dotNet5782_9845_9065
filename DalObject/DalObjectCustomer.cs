using System;
using System.Collections.Generic;
using System.Text;
using DO;
using DalApi;

namespace Dal
{
    internal partial class DalObject
    {
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            //handle existing customer
            if (DataSource.Customers.Exists(x => x.Id == id))
                throw new CustomerException("Customer to add does already exists.");
            Customer tempCustomer = new Customer() { Id = id, Name = name, Phone = phone, Longitude = StaticSexagesimal.InitializeSexagesimal(longitude, "Longitude"), Latitude = StaticSexagesimal.InitializeSexagesimal(latitude, "Latitude") };
            DataSource.Customers.Add(tempCustomer);
        }
        public void DeleteCustomer(int id)
        {
            if (!DataSource.Customers.Exists(x => x.Id == id))
                throw new CustomerException("Customer to delete does not exist.");
            DataSource.Customers.Remove(DataSource.Customers.Find(x => x.Id == id));
        }

        public Customer SearchCustomer(int customerId)
        {
            if (!DataSource.Customers.Exists(x => x.Id == customerId))
                throw new CustomerException("Customer does not exist.");
            return DataSource.Customers.Find(x => x.Id == customerId);
        }
        public IEnumerable<Customer> YieldCustomer()
        {
            return new List<Customer>(DataSource.Customers);
        }
        public double CalcDisFromCustomer(int id, double longitude, double latitude)
        {
            if (!DataSource.Customers.Exists(x => x.Id == id))
                throw new CustomerException("Customer does not exist.");
            Customer customer = this.SearchCustomer(id);
            double clong = StaticSexagesimal.ParseDouble(customer.Longitude);
            double clat = StaticSexagesimal.ParseDouble(customer.Latitude);
            return StaticSexagesimal.CalcDis(clong, clat, longitude, latitude);
        }
    }
}
