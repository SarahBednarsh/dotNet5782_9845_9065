using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL;

namespace PO
{
    public class ParcelToList : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(ParcelToList));
        static readonly DependencyProperty SenderNameProperty = DependencyProperty.Register("SenderName", typeof(string), typeof(ParcelToList));
        static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register("TargetName", typeof(string), typeof(ParcelToList));
        static readonly DependencyProperty WeightProperty = DependencyProperty.Register("Weight", typeof(WeightCategories), typeof(ParcelToList));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(Priorities), typeof(ParcelToList));
        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string SenderName { get => (string)GetValue(SenderNameProperty); set => SetValue(SenderNameProperty, value); }
        public string TargetName { get => (string)GetValue(TargetNameProperty); set => SetValue(TargetNameProperty, value); }
        public WeightCategories Weight { get => (WeightCategories)GetValue(WeightProperty); set => SetValue(WeightProperty, value); }
        public Priorities Priority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
    }
}
