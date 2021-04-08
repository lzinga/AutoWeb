using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Exceptions
{
    public class BrowserValidationException : Exception
    {
        public BrowserValidationException(string message) : base(message)
        {

        }

        public BrowserValidationException()
        {

        }
    }
}
