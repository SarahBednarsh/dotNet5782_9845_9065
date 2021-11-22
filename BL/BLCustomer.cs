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
            }
            public Customer SearchCustomer(int customerId)
            {

            }
            public IEnumerable<Customer> YieldCustomer()
            {

            }
        }
    }
}