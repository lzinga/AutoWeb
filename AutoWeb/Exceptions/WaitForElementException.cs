using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Exceptions
{
    public class WaitForElementException : Exception
    {
        public WaitForElementException(string message) : base(message)
        {

        }

        public WaitForElementException()
        {

        }
    }
}
