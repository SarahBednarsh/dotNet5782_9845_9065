using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class NotEnoughBattery : Exception
    {
        public NotEnoughBattery()
        {
        }

        public NotEnoughBattery(string message) : base(message)
        {
        }

        public NotEnoughBattery(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughBattery(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}