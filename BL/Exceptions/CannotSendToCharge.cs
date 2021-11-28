using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class CannotSendToCharge : Exception
    {
        public CannotSendToCharge()
        {
        }

        public CannotSendToCharge(string message) : base(message)
        {
        }

        public CannotSendToCharge(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotSendToCharge(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}