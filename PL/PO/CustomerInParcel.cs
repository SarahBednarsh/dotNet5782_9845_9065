using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class CustomerInParcel : DependencyObject
    {

        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(CustomerInParcel));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(CustomerInParcel));

        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
    }
}
