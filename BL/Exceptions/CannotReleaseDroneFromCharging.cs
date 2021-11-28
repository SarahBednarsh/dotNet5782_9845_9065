using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class CannotReleaseDroneFromCharging : Exception
    {
        public CannotReleaseDroneFromCharging()
        {
        }

        public CannotReleaseDroneFromCharging(string message) : base(message)
        {
        }

        public CannotReleaseDroneFromCharging(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotReleaseDroneFromCharging(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}