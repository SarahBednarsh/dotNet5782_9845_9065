using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PL;

namespace PO
{
    public class StationToList : DependencyObject
    {
        static readonly DependencyProperty IdProperty = DependencyProperty.Register("Id", typeof(int), typeof(StationToList));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(StationToList));
        static readonly DependencyProperty OpenChargeSlotsProperty = DependencyProperty.Register("OpenChargeSlots", typeof(int), typeof(StationToList));
        static readonly DependencyProperty UsedChargeSlotsProperty = DependencyProperty.Register("UsedChargeSlots", typeof(int), typeof(StationToList));


        public int Id { get => (int)GetValue(IdProperty); set => SetValue(IdProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public int OpenChargeSlots { get => (int)GetValue(OpenChargeSlotsProperty); set => SetValue(OpenChargeSlotsProperty, value); }
        public int UsedChargeSlots { get => (int)GetValue(UsedChargeSlotsProperty); set => SetValue(UsedChargeSlotsProperty, value); }
    }
}
