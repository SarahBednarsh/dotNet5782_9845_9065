﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtCustomer
        {
            public int Id { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public States State { get; set; }
            public CustomerInParcel Customer { get; set; }
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
