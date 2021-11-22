using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class CustomerInParcel
        {
            public int Id { get; set; }
            public string CustomerName { get; set; }
            public override string ToString()
            {
                return base.ToString();
            }
        }
    }
}
