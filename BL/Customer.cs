﻿using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
namespace IBL
{
    namespace BO
    {
        class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNum { get; set; }
            public Coordinates Location { get; set; }
            public List<Parcel> AtCustomer;
            public List<Parcel> ToCustomer;
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
