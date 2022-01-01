using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL;

namespace PO
{
    public class Customer: DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(Customer));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Customer));
        static readonly DependencyProperty PhoneNumProperty = DependencyProperty.Register("PhoneNum", typeof(string), typeof(Customer));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(string), typeof(Customer));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(string), typeof(Customer));
        static readonly DependencyProperty AtCustomerProperty = DependencyProperty.Register("AtCustomer", typeof(List<ParcelAtCustomer>), typeof(Customer));
        static readonly DependencyProperty ToCustomerProperty = DependencyProperty.Register("ToCustomer", typeof(List<ParcelAtCustomer>), typeof(Customer));


        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public string PhoneNum { get => (string)GetValue(PhoneNumProperty); set => SetValue(PhoneNumProperty, value); }
        public string Longitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string Latitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public List<ParcelAtCustomer> AtCustomer { get => (List<ParcelAtCustomer>)GetValue(AtCustomerProperty); set => SetValue(AtCustomerProperty, value); }
        public List<ParcelAtCustomer> ToCustomer { get => (List<ParcelAtCustomer>)GetValue(ToCustomerProperty); set => SetValue(ToCustomerProperty, value); }
    }
}
