using System;
using System.Collections.Generic;
using System.Text;
namespace BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public Location Location { get; set; }
        public List<ParcelAtCustomer> AtCustomer;
        public List<ParcelAtCustomer> ToCustomer;
        public override string ToString()
        {
            string atCustomer = "";
            foreach (ParcelAtCustomer parcel in AtCustomer)
                atCustomer += parcel.ToString();
            string toCustomer = "";
            foreach (ParcelAtCustomer parcel in ToCustomer)
                toCustomer += parcel.ToString();

            return string.Format("Id: {0}, Name: {1}, Phone number: {2}, Location: {3}, Parcels at customer: {4}, Parcels to customer: {5}", Id, Name, PhoneNum, atCustomer, toCustomer);
        }
    }
}