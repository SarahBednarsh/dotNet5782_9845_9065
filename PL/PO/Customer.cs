﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    class Customer: DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(Drone));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Drone));
        static readonly DependencyProperty PhoneNumProperty = DependencyProperty.Register("PhoneNum", typeof(string), typeof(Drone));
        static readonly DependencyProperty LongitudeProperty = DependencyProperty.Register("Longitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty LatitudeProperty = DependencyProperty.Register("Latitude", typeof(string), typeof(Drone));
        static readonly DependencyProperty AtCustomerProperty = DependencyProperty.Register("AtCustomer", typeof(List<int>), typeof(Drone));
        static readonly DependencyProperty ToCustomerProperty = DependencyProperty.Register("ToCustomer", typeof(List<int>), typeof(Drone));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public string PhoneNum { get => (string)GetValue(PhoneNumProperty); set => SetValue(PhoneNumProperty, value); }
        public string Longitude { get => (string)GetValue(LongitudeProperty); set => SetValue(LongitudeProperty, value); }
        public string Latitude { get => (string)GetValue(LatitudeProperty); set => SetValue(LatitudeProperty, value); }
        public List<int> AtCustomer { get => (List<int>)GetValue(AtCustomerProperty); set => SetValue(AtCustomerProperty, value); }
        public List<int> ToCustomer { get => (List<int>)GetValue(ToCustomerProperty); set => SetValue(ToCustomerProperty, value); }

    }
}