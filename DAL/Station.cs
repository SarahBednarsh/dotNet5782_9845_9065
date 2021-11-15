using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public Coordinates Location { get; set; }
            public int ChargeSlots { get; set; }
            public override string ToString()
            {
                return string.Format("Id: {0}, Name: {1}, Location: {2}, ChargeSlots: {3}", Id, Name, Location, ChargeSlots);
            }
        }
    }
}
