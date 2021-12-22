using System;
using System.Runtime.Serialization;

namespace BL
{
    [Serializable]
    internal class CannotDelete : Exception
    {
        public CannotDelete()
        {
        }

        public CannotDelete(string message) : base(message)
        {
        }

        public CannotDelete(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotDelete(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}