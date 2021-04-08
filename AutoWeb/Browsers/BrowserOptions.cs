using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Browsers
{
    public class BrowserOptions
    {
        public string Driver { get; set; } = "msedgedriver.exe";

        public TimeSpan Timeout { get; set; } = new TimeSpan(0, 0, 10);

        public string[] Arguments { get; set; }
    }
}
