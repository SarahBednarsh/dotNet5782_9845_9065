using System;
using System.Runtime.Serialization;

namespace BL
{
    [Serializable]
    public class InternalError : Exception
    {
        public InternalError()
        {
        }

        public InternalError(string message) : base(message)
        {
        }

        public InternalError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InternalError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}