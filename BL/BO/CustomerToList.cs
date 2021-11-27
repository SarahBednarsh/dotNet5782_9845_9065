using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNum { get; set; }
            public int Delivered { get; set; }
            public int Sent { get; set; }
            public int Got { get; set; }
            public int OnTheirWay { get; set; }
            public override string ToString()
            {
                return string.Format($"Id: {Id}, Name: {Name}, Phone number: {PhoneNum}, Number of parcels delivered: {Delivered}, Number of parcels sent: {Sent}, Number of parcels gotten: {Got}, Number of parcels on their way: {OnTheirWay}");
            }
        }
    }
}
