using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace IBL
{
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
                return base.ToString();
            }
        }
    }
}
