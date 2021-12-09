using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class CustomerInParcel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            //return base.ToString();
            return string.Format($"Id: {Id}, Name: {Name}");
        }
    }
}
