using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public class CustomerToList : DependencyObject
    {
        static readonly DependencyProperty CustomerToListIdProperty = DependencyProperty.Register("CustomerToListId", typeof(int), typeof(CustomerToList));
        static readonly DependencyProperty CustomerToListNameProperty = DependencyProperty.Register("CustomerToListName", typeof(string), typeof(CustomerToList));
        static readonly DependencyProperty CustomerToListPhoneNumProperty = DependencyProperty.Register("CustomerToListPhoneNum", typeof(string), typeof(CustomerToList));
        static readonly DependencyProperty DeliveredProperty = DependencyProperty.Register("CustomerToListDelivered", typeof(int), typeof(CustomerToList));
        static readonly DependencyProperty SentProperty = DependencyProperty.Register("CustomerToListSent", typeof(int), typeof(CustomerToList));
        static readonly DependencyProperty GotProperty = DependencyProperty.Register("CustomerToListGot", typeof(int), typeof(CustomerToList));
        static readonly DependencyProperty OnTheirWayProperty = DependencyProperty.Register("CustomerToListOnTheirWay", typeof(int), typeof(CustomerToList));

        public int CustomerToListId { get => (int)GetValue(CustomerToListIdProperty); set => SetValue(CustomerToListIdProperty, value); }
        public string CustomerToListName { get => (string)GetValue(CustomerToListNameProperty); set => SetValue(CustomerToListNameProperty, value); }
        public string CustomerToListPhoneNum { get => (string)GetValue(CustomerToListPhoneNumProperty); set => SetValue(CustomerToListPhoneNumProperty, value); }
        public int Delivered { get => (int)GetValue(DeliveredProperty); set => SetValue(DeliveredProperty, value); }
        public int Sent { get => (int)GetValue(SentProperty); set => SetValue(SentProperty, value); }
        public int Got { get => (int)GetValue(GotProperty); set => SetValue(GotProperty, value); }
        public int OnTheirWay { get => (int)GetValue(OnTheirWayProperty); set => SetValue(OnTheirWayProperty, value); }
    }
}
