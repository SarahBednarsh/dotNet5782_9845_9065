using System;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class NoRelevantParcels : Exception
    {
        public NoRelevantParcels()
        {
        }

        public NoRelevantParcels(string message) : base(message)
        {
        }

        public NoRelevantParcels(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoRelevantParcels(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}