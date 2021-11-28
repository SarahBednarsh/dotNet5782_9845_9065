using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace IBL
{
    namespace BO
    {
        public partial class BL
        {
            public void AddCustomer(int id, string name, string phone, double longitude, double latitude)//are we supposed to get location here?
            {
                //if customer exists then exception-check if there is in dal
                try
                { 
                    dalAP.AddCustomer(id, name, phone, longitude, latitude); 
                }
                catch(CustomerException exception)
                {
                    throw new KeyAlreadyExists(string.Format("Customer with id {0} alreday exists", id), exception);
                }

            }
            public void UpdateCustomerInfo(int customerId, string name, string phone)//not sure
            {
                try
                { 
                    IDAL.DO.Customer customer = dalAP.SearchCustomer(customerId);
                    //deal with if it doesnt exist
                    if (name != "")
                        customer.Name = name;
                    if (phone != "")
                        customer.Phone = phone;
                    dalAP.DeleteCustomer(customerId);
                    dalAP.AddCustomer(customerId, customer.Name, customer.Phone, StaticSexagesimal.ParseDouble(customer.Longitude), StaticSexagesimal.ParseDouble(customer.Latitude));
                }
                catch (CustomerException exception)
                {
                    throw new KeyDoesNotExist(string.Format("Customer with id {0} does not exists", customerId), exception);
                }
            }
            public Customer SearchCustomer(int customerId)
            {
                try
                {
                    IDAL.DO.Customer customer = dalAP.SearchCustomer(customerId);
                    //if equals default exception
                    Customer BLcustomer = CreateCustomer(customer);
                    return BLcustomer;
                }
                catch (CustomerException exception)
                {
                    throw new KeyDoesNotExist(string.Format("Customer with id {0} does not exists", customerId), exception);
                }
            }
            private IEnumerable<Customer> YieldCustomer()
            {
                IEnumerable<IDAL.DO.Customer> customers = dalAP.YieldCustomer();
                List<Customer> newCustomers = new List<Customer>();
                foreach (IDAL.DO.Customer customer in customers)
                {
                    newCustomers.Add(CreateCustomer(customer));
                }
                return newCustomers;
            }

            private Customer CreateCustomer(IDAL.DO.Customer old)
            {
                
                Customer customer = new Customer();
                customer.Id = old.Id;
                customer.Location = LocationStaticClass.InitializeLocation(old.Longitude, old.Latitude);
                customer.Name = old.Name;
                customer.PhoneNum = old.Phone;
                customer.Location = LocationStaticClass.InitializeLocation(old.Longitude, old.Latitude);
                foreach (IDAL.DO.Parcel parcel in dalAP.YieldParcel())
                {
                    if (customer.Id == parcel.SenderId)//from this customer
                    {
                        States state = States.Created;
                        Parcel BLparcel = SearchParcel(parcel.Id);
                        if (BLparcel.Delivery <= DateTime.Now)
                            state = States.Delivered;
                        else if (BLparcel.PickUp <= DateTime.Now)
                            state = States.PickedUp;
                        else if (BLparcel.Attribution <= DateTime.Now)
                            state = States.Attributed;
                        CustomerInParcel tmp = new CustomerInParcel { Id = parcel.TargetId, Name = SearchCustomer(parcel.TargetId).Name};
                        customer.AtCustomer.Add(new ParcelAtCustomer { Id = customer.Id, Customer = tmp, Priority = (Priorities)parcel.Priority, State = state, Weight = (WeightCategories)parcel.Weight });
                    }
                    if (customer.Id == parcel.TargetId)//to this customer
                    {
                        States state = States.Created;
                        Parcel BLparcel = SearchParcel(parcel.Id);
                        if (BLparcel.Delivery <= DateTime.Now)
                            state = States.Delivered;
                        else if (BLparcel.PickUp <= DateTime.Now)
                            state = States.PickedUp;
                        else if (BLparcel.Attribution <= DateTime.Now)
                            state = States.Attributed;
                        CustomerInParcel tmp = new CustomerInParcel { Id = parcel.SenderId, Name = SearchCustomer(parcel.SenderId).Name };
                        customer.ToCustomer.Add(new ParcelAtCustomer { Id = customer.Id, Customer = tmp, Priority = (Priorities)parcel.Priority, State = state, Weight = (WeightCategories)parcel.Weight });
                    }
                }
                return customer;
            }
            public IEnumerable<CustomerToList> ListCustomer()
            {
                List<CustomerToList> newCustomers = new List<CustomerToList>();
                foreach (IDAL.DO.Customer customer in YieldCustomer())
                {
                    newCustomers.Add(CreateCustomer(customer));
                }
                return newCustomers;
            }

        }
    }
}