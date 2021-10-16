using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Extensions
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