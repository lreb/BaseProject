using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProjectAPI.Domain.Helpers
{
    public class BaseException : Exception
    {
        public BaseException() : base()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
