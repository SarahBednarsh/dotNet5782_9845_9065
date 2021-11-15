using System;
using System.Collections.Generic;
using System.Text;


namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public Coordinates Location { get; set; }
            public override string ToString()
            {
                return string.Format("Id: {0}, Name: {1}, Phone: {2}, Location: {3}", Id, Name, Phone, Location);
            }
        }
    }
}

