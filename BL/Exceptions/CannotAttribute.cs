using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class CannotAttribute : Exception
    {
        public CannotAttribute()
        {
        }

        public CannotAttribute(string message) : base(message)
        {
        }

        public CannotAttribute(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotAttribute(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}