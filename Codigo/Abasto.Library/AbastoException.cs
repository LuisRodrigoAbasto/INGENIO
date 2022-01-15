using System;
using System.Runtime.Serialization;

namespace Abasto.Library
{
    [Serializable]
    public class AbastoException : Exception
    {
        public AbastoException()
        {
        }

        public AbastoException(string message) : base(message)
        {
        }

        public AbastoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AbastoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}