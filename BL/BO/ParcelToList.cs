using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public class ParcelToList
        {
            public int Id { get; set; }
            public string SenderName { get; set; }
            public string TargetName { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            // public enum WeightCategories { Light=1, Medium, Heavy }
            //public enum Priorities { Normal=1, Quick, Emergency }
            public override string ToString()
            {
                return string.Format($"Id: {Id}, Sender: {SenderName}, Target: {TargetName}, Weight: {Weight}, Priority: {Priority}");
            }
        }
    }
}
