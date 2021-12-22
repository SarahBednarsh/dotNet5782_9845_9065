using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class Customer: DependencyObject
    {
        static readonly DependencyProperty CustomerIdProperty = DependencyProperty.Register("CustomerId", typeof(int), typeof(Drone));
        static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(Drone));
        static readonly DependencyProperty CustomerPhoneNumProperty = DependencyProperty.Register("CustomerPhoneNum", typeof(string), typeof(Drone));
        static readonly DependencyProperty CustomerLongitudeProperty = DependencyProperty.Register("CustomerLongitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty CustomerLatitudeProperty = DependencyProperty.Register("CustomerLatitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty AtCustomerProperty = DependencyProperty.Register("AtCustomer", typeof(List<int>), typeof(Drone));
        static readonly DependencyProperty ToCustomerProperty = DependencyProperty.Register("ToCustomer", typeof(List<int>), typeof(Drone));


        public int CustomerId { get => (int)GetValue(CustomerIdProperty); set => SetValue(CustomerIdProperty, value); }
        public string CustomerName { get => (string)GetValue(CustomerNameProperty); set => SetValue(CustomerNameProperty, value); }
        public string CustomerPhoneNum { get => (string)GetValue(CustomerPhoneNumProperty); set => SetValue(CustomerPhoneNumProperty, value); }
        public string CustomerLongitude { get => (string)GetValue(CustomerLongitudeProperty); set => SetValue(CustomerLongitudeProperty, value); }
        public string CustomerLatitude { get => (string)GetValue(CustomerLatitudeProperty); set => SetValue(CustomerLatitudeProperty, value); }
        public List<int> AtCustomer { get => (List<int>)GetValue(AtCustomerProperty); set => SetValue(AtCustomerProperty, value); }
        public List<int> ToCustomer { get => (List<int>)GetValue(ToCustomerProperty); set => SetValue(ToCustomerProperty, value); }
        //public int Delivered { get; set; }
        //public int Sent { get; set; }
        //public int Got { get; set; }
        //public int OnTheirWay { get; set; }

    }
}
