﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IBL
{
    namespace BO
    {
        public class StationToList
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public int OpenChargeSlots { get; set; }
            public int UsedChargeSlots { get; set; }
            public override string ToString()
            {
                return string.Format($"Id: {Id}, Name: {Name}, Open charge slots: {OpenChargeSlots}, Used charge slots: {UsedChargeSlots}");
            }
        }
    }
}