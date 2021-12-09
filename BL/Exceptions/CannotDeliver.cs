using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    internal class CannotDeliver : Exception
    {
        public CannotDeliver()
        {
        }

        public CannotDeliver(string message) : base(message)
        {
        }

        public CannotDeliver(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotDeliver(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}