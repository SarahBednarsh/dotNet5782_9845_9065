using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ParcelToList
        {
            public int Id { get; set; }
            public string Sender { get; set; }
            public string Target { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public States State { get; set; }
            public enum WeightCategories { Light=1, Medium, Heavy }
        public enum Priorities { Normal=1, Quick, Emergency }
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
