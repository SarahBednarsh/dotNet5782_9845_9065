using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class NotEnoughChargingSlots : Exception
    {
        public NotEnoughChargingSlots()
        {
        }

        public NotEnoughChargingSlots(string message) : base(message)
        {
        }

        public NotEnoughChargingSlots(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughChargingSlots(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}