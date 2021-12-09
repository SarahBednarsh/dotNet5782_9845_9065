using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class KeyAlreadyExists : Exception
    {
        public KeyAlreadyExists()
        {
        }

        public KeyAlreadyExists(string message) : base(message)
        {
        }

        public KeyAlreadyExists(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyAlreadyExists(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}