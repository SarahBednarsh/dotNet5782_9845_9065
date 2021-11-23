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
                if (name == "" && phone == "")
                {
                    //  throw Exception
                }
                IDAL.DO.Customer customer = dalAP.SearchCustomer(customerId);
                //deal with if it doesnt exist
                if (name != "")
                    customer.Name = name;
                if (phone != "")
                    customer.Phone = phone;
            }
            public Customer SearchCustomer(int customerId)
            {
                IDAL.DO.Customer customer = dalAP.SearchCustomer(customerId);
                //if equals default exception
                Customer BLcustomer = createCustomer(customer);
                return BLcustomer;
            }
            public IEnumerable<Customer> YieldCustomer()
            {
                IEnumerable<IDAL.DO.Customer> customers = dalAP.YieldCustomer();
                List<Customer> newCustomers = new List<Customer>();
                foreach (IDAL.DO.Customer customer in customers)
                {
                    newCustomers.Add(createCustomer(customer));
                }
                return newCustomers;
            }
            public Customer createCustomer(IDAL.DO.Customer old)
            {
                Customer customer = new Customer();
                customer.Id = old.Id;
                customer.Location = LocationStaticClass.InitializeLocation(old.Longitude, old.Latitude);
                customer.Name = old.Name;
                customer.PhoneNum = old.Phone;
                List<ParcelAtCustomer> sentFromCustomer = new List<ParcelAtCustomer>();
                List<ParcelAtCustomer> sentToCustomer = new List<ParcelAtCustomer>();
                foreach (IDAL.DO.Parcel parcel in dalAP.YieldParcel())
                {
                    if (customer.Id == parcel.SenderId)//from this customer
                    {
                        sentFromCustomer.Add(createParcelAtCustomer(parcel.Id,parcel.Weight,parcel.Priority,parcel.))
                    }
                }
            }
            public ParcelAtCustomer CreateParcelAtCustomer(int id, WeightCategories weight, Priorities priority, States state, CustomerInParcel customer)
            {
                ParcelAtCustomer parcel = new ParcelAtCustomer();
                parcel.Id = id;
                parcel.Weight = weight;
                parcel.Priority = priority;
                parcel.State = state;
                parcel.Customer = customer;
                return parcel;

            }
        }
    }
}