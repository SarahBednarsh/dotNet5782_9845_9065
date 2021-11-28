using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class CannotPickUp : Exception
    {
        public CannotPickUp()
        {
        }

        public CannotPickUp(string message) : base(message)
        {
        }

        public CannotPickUp(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotPickUp(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}