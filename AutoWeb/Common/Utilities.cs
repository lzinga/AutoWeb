using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Common
{
    internal static class Utilities
    {
        internal static string ToInvariantCulture(Key key)
        {
            return Convert.ToString(Convert.ToChar(key, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }
    }
}
