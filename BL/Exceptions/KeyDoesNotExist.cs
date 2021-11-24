using System;
using System.Runtime.Serialization;

namespace IBL.BO
{
    [Serializable]
    public class KeyDoesNotExist : Exception
    {
        public KeyDoesNotExist()
        {
        }

        public KeyDoesNotExist(string message) : base(message)
        {
        }

        public KeyDoesNotExist(string message, Exception innerException) : base(message, innerException)
        {
        }


        protected KeyDoesNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}