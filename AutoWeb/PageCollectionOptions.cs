using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoWeb.Browsers;
using AutoWeb.Common;

namespace AutoWeb
{
    /// <summary>
    /// The options for <see cref="IAutoWeb"/> creation.
    /// </summary>
    public class PageCollectionOptions
    {
        internal static PageCollectionOptions Default => new();

        internal BrowserOptions BrowserOptions { get; private set; } = new BrowserOptions();


        public bool CleanOrphanedDrivers { get; set; } = true;


        #region Internal Options
        internal Type Type { get; private set; } = typeof(EdgeBrowser);
        #endregion


        #region Browser Configuration
        /// <summary>
        /// Sets the browser to use. For example Edge (default), Chrome, Firefox. Driver path will need to be set in options.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        public void Browser<T>(Action<BrowserOptions> options) where T : IBrowser
        {
            if(options != null)
            {
                options.Invoke(BrowserOptions);
            }
            
            this.Type = typeof(T);
        }

        /// <summary>
        /// Sets the browser to use. For example Edge (default), Chrome, or Firefox with the specified driver path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="driver"></param>
        public void Browser<T>(string driver) where T : IBrowser
        {
            this.Browser<T>(options =>
            {
                options.Driver = driver;
            });
        }
        #endregion
    }
}
