using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class CustomerInParcel : DependencyObject
    {

        static readonly DependencyProperty IdProperty = DependencyProperty.Register("CustomerInParcelId", typeof(int), typeof(CustomerInParcel));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("CustomerInParcelName", typeof(string), typeof(CustomerInParcel));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
    }
}
