using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutoWeb.Browsers
{
    public sealed class EdgeBrowser : Browser
    {
        public EdgeBrowser(string driver, TimeSpan timeout, params string[] arguments)
        {
            var file = base.ValidateDriver(driver);

            var options = new EdgeOptions
            {
                UseChromium = true
            };
            if (arguments != null && arguments.Length > 0)
            {
                options.AddArguments(arguments);
            }

            var service = EdgeDriverService.CreateChromiumService(file.DirectoryName, file.Name);
            service.EnableVerboseLogging = false;
            service.HideCommandPromptWindow = true;

            this.Timeout = timeout;
            this.Driver = new EdgeDriver(service, options);
            this.DriverWait = new WebDriverWait(base.Driver, timeout);
        }
    }
}
