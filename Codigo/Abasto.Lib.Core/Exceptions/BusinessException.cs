using System;
using System.Collections.Generic;
using System.Text;

namespace Abasto.Lib.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }

        public BusinessException(string message) : base(message)
        {

        }
    }
}
