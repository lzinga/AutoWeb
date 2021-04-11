using AutoWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Attributes
{
    /// <summary>
    /// Defines a pages metadata for execution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PageAttribute : Attribute
    {
        /// <summary>
        /// Defines a pages metadata.
        /// </summary>
        /// <param name="name">The friendly name of the page.</param>
        /// <param name="url">The url of the page to load. Supports local files.</param>
        public PageAttribute(string name, string url)
        {
            Name = name;
            Url = url;
        }

        /// <summary>
        /// Defines a pages metadata.
        /// </summary>
        /// <param name="url">The URL the page will open.</param>
        public PageAttribute(string url)
        {
            Name = string.Empty;
            Url = url;
        }


        /// <summary>
        /// Defines a pages metadata.
        /// </summary>
        /// <param name="url">The URL the page will open.</param>
        /// <param name="pageLoadTimeout">The time in seconds how long the page should take before timing out.</param>
        public PageAttribute(string url, int pageLoadTimeout) : this(string.Empty, url, pageLoadTimeout) { }

        /// <summary>
        /// Defines a pages metadata.
        /// </summary>
        /// <param name="name">the friendly name of the page.</param>
        /// <param name="url">The url of the page to load. Supports local files.</param>
        /// <param name="pageLoadTimeout">The time in seconds how long the page should take before timing out.</param>
        public PageAttribute(string name, string url, int pageLoadTimeout) : this(name, url)
        {
            this.PageLoadTimeout = new TimeSpan(0, 0, pageLoadTimeout);
        }

        /// <summary>
        /// The friendly name of the page.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The url of the page.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The time to wait before the page fails to load according to certain parameters.
        /// </summary>
        public TimeSpan PageLoadTimeout { get; set; }




    }
}
