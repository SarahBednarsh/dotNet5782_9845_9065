using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class KeyDoesNotExists : Exception
    {
        public KeyDoesNotExists()
        {
        }

        public KeyDoesNotExists(string message) : base(message)
        {
        }

        public KeyDoesNotExists(string message, Exception innerException) : base(message, innerException)
        {
        }


        protected KeyDoesNotExists(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}