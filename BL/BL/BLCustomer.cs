using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
using BO;
using Customer = BO.Customer;
using Parcel = BO.Parcel;
using Priorities = BO.Priorities;
using WeightCategories = BO.WeightCategories;
using System.Runtime.CompilerServices;

namespace BL
{
    internal partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)//are we supposed to get location here?
        {
            if (longitude < 29.489 || longitude > 33.154 || latitude < 34.361 || latitude > 35.475)
                throw new FormatException("The location is not in Israel");
            try
            {
                lock (dalAP)
                {
                    dalAP.AddCustomer(id, name, phone, longitude, latitude);
                }
            }
            catch (CustomerException exception)
            {
                throw new KeyAlreadyExists(string.Format("Customer with id {0} alreday exists", id), exception);
            }

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int customerId)
        {
            Customer customer = SearchCustomer(customerId);
            IEnumerable<Parcel> onTheWayToCustomer = from drone in ListDrone()
                                                     where drone.Status == DroneStatuses.Delivering
                                                     let parcel = SearchParcel(drone.IdOfParcel)
                                                     where parcel.Target.Id == customerId
                                                     select parcel;
            if (onTheWayToCustomer.Count() > 0)
                throw new CannotDelete("There are parcels on their way to customer, cannot delete");
            try
            {
                lock (dalAP)
                {
                    dalAP.DeleteCustomer(customerId);
                }
            }
            catch (CustomerException exception)
            {
                throw new KeyDoesNotExist("No such customer", exception);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerInfo(int customerId, string name, string phone)
        {
            try
            {
                lock (dalAP)
                {
                    DO.Customer customer = dalAP.SearchCustomer(customerId); //find relevant customer
                    if (name != "")
                        customer.Name = name;
                    if (phone != "")
                        customer.Phone = phone;
                    dalAP.DeleteCustomer(customerId);
                    dalAP.AddCustomer(customerId, customer.Name, customer.Phone, StaticSexagesimal.ParseDouble(customer.Longitude), StaticSexagesimal.ParseDouble(customer.Latitude));
                }
            }
            catch (CustomerException exception)
            {
                throw new KeyDoesNotExist(string.Format("Customer with id {0} does not exists", customerId), exception);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer SearchCustomer(int customerId)
        {
            try
            {
                lock (dalAP)
                {
                    DO.Customer customer = dalAP.SearchCustomer(customerId);
                    Customer BLcustomer = CreateCustomer(customer); //convert DO.Customer to BL.Customer
                    return BLcustomer;
                }
            }
            catch (CustomerException exception)
            {
                throw new KeyDoesNotExist(string.Format("Customer with id {0} does not exists", customerId), exception);
            }
        }
        private IEnumerable<Customer> YieldCustomer() //return list of BL customers
        {
            lock (dalAP)
            {
                return from customer in dalAP.YieldCustomer()
                       select CreateCustomer(customer);
            }
        }
        private Customer CreateCustomer(DO.Customer old) //convert DO.Customer to BO.Customer
        {

            Customer customer = new Customer();
            customer.ToCustomer = new List<ParcelAtCustomer> { };
            customer.AtCustomer = new List<ParcelAtCustomer> { };
            customer.Id = old.Id;
            customer.Location = LocationStaticClass.InitializeLocation(old.Longitude, old.Latitude);
            customer.Name = old.Name;
            customer.PhoneNum = old.Phone;
            customer.Location = LocationStaticClass.InitializeLocation(old.Longitude, old.Latitude);
            lock (dalAP)
            {
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
                        customer.AtCustomer.Add(new ParcelAtCustomer { Id = parcel.Id, Customer = tmp, Priority = (Priorities)parcel.Priority, State = state, Weight = (WeightCategories)parcel.Weight });
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
            }
            return customer;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> ListCustomer()
        {
            return from Customer customer in YieldCustomer()
                   select new CustomerToList
                   {
                       Id = customer.Id,
                       Name = customer.Name,
                       PhoneNum = customer.PhoneNum,
                       Delivered = customer.AtCustomer.FindAll(x => x.State == States.Delivered).Count,
                       Got = customer.AtCustomer.FindAll(x => x.State != States.Delivered).Count,
                       OnTheirWay = customer.ToCustomer.FindAll(x => x.State != States.Delivered).Count,
                       Sent = customer.AtCustomer.FindAll(x => x.State != States.Delivered).Count
                   };
        }
    }
}
