using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
using BO;
using Customer = BO.Customer;
using Parcel = BO.Parcel;
using Priorities = BO.Priorities;

namespace BL
{
    internal partial class BL
    {
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)//are we supposed to get location here?
        {
            //if customer exists then exception-check if there is in dal
            try
            {
                dalAP.AddCustomer(id, name, phone, longitude, latitude);
            }
            catch (CustomerException exception)
            {
                throw new KeyAlreadyExists(string.Format("Customer with id {0} alreday exists", id), exception);
            }

        }
        public void UpdateCustomerInfo(int customerId, string name, string phone)
        {
            try
            {
                DO.Customer customer = dalAP.SearchCustomer(customerId); //find relevant customer
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
                DO.Customer customer = dalAP.SearchCustomer(customerId);
                Customer BLcustomer = CreateCustomer(customer); //convert DO.Customer to BL.Customer
                return BLcustomer;
            }
            catch (CustomerException exception)
            {
                throw new KeyDoesNotExist(string.Format("Customer with id {0} does not exists", customerId), exception);
            }
        }
        private IEnumerable<Customer> YieldCustomer() //return list of BL customers
        {
            IEnumerable<DO.Customer> customers = dalAP.YieldCustomer();
            foreach (DO.Customer customer in customers)
            {
                yield return CreateCustomer(customer);
            }
        }
        private Customer CreateCustomer(DO.Customer old) //convert DO.Customer to BL.Customer
        {

            Customer customer = new Customer();
            customer.ToCustomer = new List<ParcelAtCustomer> { };
            customer.AtCustomer = new List<ParcelAtCustomer> { };
            customer.Id = old.Id;
            customer.Location = LocationStaticClass.InitializeLocation(old.Longitude, old.Latitude);
            customer.Name = old.Name;
            customer.PhoneNum = old.Phone;
            customer.Location = LocationStaticClass.InitializeLocation(old.Longitude, old.Latitude);
            foreach (DO.Parcel parcel in dalAP.YieldParcel()) //make list of sent and recieved parcels
            {
                if (customer.Id == parcel.SenderId)//from this customer
                {
                    States state = States.Created;
                    Parcel BLparcel = CreateParcel(dalAP.SearchParcel(parcel.Id));
                    if (BLparcel.Delivery != null)
                        state = States.Delivered;
                    else if (BLparcel.PickUp != null)
                        state = States.PickedUp;
                    else if (BLparcel.Attribution != null)
                        state = States.Attributed;
                    CustomerInParcel tmp = new CustomerInParcel { Id = parcel.TargetId, Name = dalAP.SearchCustomer(parcel.TargetId).Name };
                    customer.AtCustomer.Add(new ParcelAtCustomer { Id = customer.Id, Customer = tmp, Priority = (Priorities)parcel.Priority, State = state, Weight = (WeightCategories)parcel.Weight });
                }
                if (customer.Id == parcel.TargetId)//to this customer
                {
                    States state = States.Created;
                    Parcel BLparcel = SearchParcel(parcel.Id);
                    if (BLparcel.Delivery != null)
                        state = States.Delivered;
                    else if (BLparcel.PickUp != null)
                        state = States.PickedUp;
                    else if (BLparcel.Attribution != null)
                        state = States.Attributed;
                    CustomerInParcel tmp = new CustomerInParcel { Id = parcel.SenderId, Name = dalAP.SearchCustomer(parcel.SenderId).Name };
                    customer.ToCustomer.Add(new ParcelAtCustomer { Id = customer.Id, Customer = tmp, Priority = (Priorities)parcel.Priority, State = state, Weight = (WeightCategories)parcel.Weight });
                }
            }
            return customer;
        }
        public IEnumerable<CustomerToList> ListCustomer()
        {
            foreach (Customer customer in YieldCustomer())
            {
                int delivered = customer.AtCustomer.FindAll(x => x.State == States.Delivered).Count;
                int sent = customer.AtCustomer.FindAll(x => x.State != States.Delivered).Count;
                int got = customer.ToCustomer.FindAll(x => x.State == States.Delivered).Count;
                int onTheirWay = customer.ToCustomer.FindAll(x => x.State != States.Delivered).Count;
                yield return new CustomerToList { Id = customer.Id, Name = customer.Name, PhoneNum = customer.PhoneNum, Delivered = delivered, Got = got, OnTheirWay = onTheirWay, Sent = sent };
            }
        }
        public IEnumerable<CustomerToList> ListCustomerConditional(Predicate<CustomerToList> predicate)
        {
            return from customer in ListCustomer()
                   where predicate(customer)
                   select customer;
        }
    }
}
