using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutoWeb.Browsers
{
    /// <summary>
    /// Use the Chrome driver for automation.
    /// </summary>
    public sealed class ChromeBrowser : Browser
    {
        public ChromeBrowser(string driver, TimeSpan timeout, params string[] arguments)
        {
            var file = base.ValidateDriver(driver);

            var options = new ChromeOptions();
            if (arguments != null && arguments.Length > 0)
            {
                options.AddArguments(arguments);
            }

            var service = ChromeDriverService.CreateDefaultService(file.DirectoryName, file.Name);
            service.EnableVerboseLogging = false;
            service.HideCommandPromptWindow = true;

            this.Timeout = timeout;
            this.Driver = new ChromeDriver(service, options);
            this.DriverWait = new WebDriverWait(base.Driver, timeout);
        }
    }
}
