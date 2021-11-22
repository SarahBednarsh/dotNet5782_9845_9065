using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
            {
                //if customer exists then exception-check if there is in dal
                dalAP.AddCustomer(id, name, phone, longitude, latitude);

            }
            public void UpdateCustomerInfo(int customerId, string name, string phone)//not sure
            {
                if (name==""&&phone=="")
                {
                  //  throw Exception
                }
                IDAL.DO.Customer customer = dalAP.SearchCustomer(customerId);
                //deal with if it doesnt exist
                if (name!="")
                    customer.Name = name;
                if (phone != "")
                    customer.Name = phone;
            }
            public Customer SearchCustomer(int customerId)
            {
                IDAL.DO.Customer customer = dalAP.SearchCustomer(customerId);
                //if equals default exception
                Customer BLcustomer = createNewCustomer(customer);
                return BLcustomer;
            }
            public IEnumerable<Customer> YieldCustomer()
            {
                IEnumerable<IDAL.DO.Customer> customers = dalAP.YieldCustomer();
                List<Customer> newCustomers = new List<Customer>();
                foreach(IDAL.DO.Customer customer in customers)
                {
                    newCustomers.Add(createNewCustomer(customer));
                }
                return newCustomers;
            }
        }
    }
}