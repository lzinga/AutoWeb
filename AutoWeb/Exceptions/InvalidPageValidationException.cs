using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Exceptions
{
    public class InvalidPageValidationException : Exception
    {
        public InvalidPageValidationException(string message) : base(message)
        {

        }

        public InvalidPageValidationException()
        {

        }
    }
}
