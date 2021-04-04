using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoWeb.Common;

namespace AutoWeb
{
    /// <summary>
    /// The options for <see cref="IAutoWeb"/> creation.
    /// </summary>
    public class PageCollectionOptions
    {
        /// <summary>
        /// The path to a selenium supported web driver.
        /// </summary>
        public string DriverPath { get; set; }

        /// <summary>
        /// If the <see cref="IAutoWeb"/> throws an exception the browser can remain as a process.
        /// This options allows them to be cleaned up on every execution.
        /// </summary>
        public bool CleanOrphanedDrivers { get; set; }

        /// <summary>
        /// The time out for various web selection elements.
        /// </summary>
        public TimeSpan DefaultTimeOut { get; set; }

        /// <summary>
        /// The arguments for the browser (default --headless)
        /// </summary>
        public string[] BrowserArguments { get; set; }


        /// <summary>
        /// Default <see cref="IAutoWeb"/> options.
        /// </summary>
        public static PageCollectionOptions DefaultEdge => new PageCollectionOptions()
        {
            DriverPath = "msedgedriver.exe",
            DefaultTimeOut = new TimeSpan(0, 0, 5),
            CleanOrphanedDrivers = true,
            BrowserArguments = new string[]
            {

            }
        };
    }
}
