using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            Customer tempCustomer = new Customer() { Id = id, Name = name, Phone = phone, Location = new Coordinates(longitude, latitude) };
            DataSource.Customers.Add(tempCustomer);
        }

        public Customer SearchCustomer(int customerId)
        {
            return DataSource.Customers.Find(x => x.Id == customerId);
        }
        public IEnumerable<Customer> YieldCustomer()
        {
            return new List<Customer>(DataSource.Customers);
        }
        public double CalcDisFromCustomer(int id, double longitude, double latitude)
        {
            Customer customer = this.SearchCustomer(id);
            return customer.Location.CalcDis(new Coordinates(longitude, latitude));
        }
    }
}
