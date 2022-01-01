using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL;
namespace PO
{
    public class ParcelAtCustomer : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(ParcelAtCustomer));
        static readonly DependencyProperty WeightProperty = DependencyProperty.Register("Weight", typeof(WeightCategories), typeof(ParcelAtCustomer));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(ParcelAtCustomer));
        static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(States), typeof(ParcelAtCustomer));
        static readonly DependencyProperty CustomerProperty = DependencyProperty.Register("Customer", typeof(CustomerInParcel), typeof(ParcelAtCustomer));
        
        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public WeightCategories Weight { get => (WeightCategories)GetValue(WeightProperty); set => SetValue(WeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
        public States State { get => (States)GetValue(StateProperty); set => SetValue(StateProperty, value); }
        public CustomerInParcel Customer { get => (CustomerInParcel)GetValue(CustomerProperty); set => SetValue(CustomerProperty, value); }
        

    }
}
