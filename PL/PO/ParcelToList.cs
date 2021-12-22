using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PL
{
    public class ParcelToList : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("PTLId", typeof(int), typeof(Drone));
        static readonly DependencyProperty SenderNameProperty = DependencyProperty.Register("PTLSenderName", typeof(string), typeof(Drone));
        static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register("PTLTargetName", typeof(string), typeof(Drone));
        static readonly DependencyProperty WeightProperty = DependencyProperty.Register("PTLWeight", typeof(WeightCategories), typeof(Drone));
        static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("PTLPriority", typeof(Priorities), typeof(Drone));
        public int PTLId { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string PTLSenderName { get => (string)GetValue(SenderNameProperty); set => SetValue(SenderNameProperty, value); }
        public string PTLTargetName { get => (string)GetValue(TargetNameProperty); set => SetValue(TargetNameProperty, value); }
        public WeightCategories PTLWeight { get => (WeightCategories)GetValue(WeightProperty); set => SetValue(WeightProperty, value); }
        public Priorities PTLPriority { get => (Priorities)GetValue(PriorityProperty); set => SetValue(PriorityProperty, value); }
    }
}
