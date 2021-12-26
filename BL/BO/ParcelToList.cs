using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class ParcelToList
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string TargetName { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public override string ToString()
        {
            return string.Format($"Id: {Id}, Sender: {SenderName}, Target: {TargetName}, Weight: {Weight}, Priority: {Priority}");
        }
    }
}
