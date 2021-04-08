using AutoWeb.Attributes;
using AutoWeb.Common;
using AutoWeb.Exceptions;
using AutoWeb.Common.Internal;
using AutoWeb.WebElements;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoWeb.Browsers;

namespace AutoWeb
{
    /// <summary>
    /// The entry object for all things AutoWeb.
    /// </summary>
    public class PageCollection : IAutoWeb
    {
        readonly List<IPage> pages = new();
        
        private readonly PageCollectionOptions options = new();
        private readonly IServiceProvider provider = null;

        internal IWebDriver Driver { get; private set; }

        public IBrowser Browser { get; private set; }

        /// <summary>
        /// Creates a collection of pages which can be executed individually or all together.
        /// <para>Executes in the order of insertion.</para>
        /// </summary>
        /// <param name="options">The options to configure your execution.</param>
        public PageCollection(Action<PageCollectionOptions> options)
        {
            if(options != null)
            {
                options.Invoke(this.options);
            }
            else
            {
                this.options = PageCollectionOptions.Default;
            }
        }

        /// <summary>
        /// Quits out of the browser.
        /// </summary>
        ~PageCollection()
        {
            if(Driver != null)
            {
                Driver.Quit();
            }
        }

        /// <summary>
        /// Creates a collection of pages which can be executed individually or all together.
        /// </summary>
        public PageCollection() : this(options: null) { }

        /// <summary>
        /// Opens the browser configured in options. (msedgedriver is default)
        /// </summary>
        /// <param name="url">The url to open.</param>
        public void OpenBrowser(string url)
        {
            // Clean 
            if (this.options.CleanOrphanedDrivers)
            {
                var orphans = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(this.options.BrowserOptions.Driver));
                foreach (var orphan in orphans)
                {
                    // ( ͡❛ _⦣ ͡❛)
                    orphan.Kill();
                }
            }



            var browser = (Activator.CreateInstance(options.Type, new object[] {
                options.BrowserOptions.Driver,
                options.BrowserOptions.Timeout,
                options.BrowserOptions.Arguments ?? Array.Empty<string>()
            }) as Browser);

            this.Driver = browser.Driver;
            this.Browser = browser;




            if (!string.IsNullOrEmpty(url))
            {
                this.Driver.Navigate().GoToUrl(url);
            }
        }

        public void OpenBrowser() => this.OpenBrowser(string.Empty);

        private IPage Populate(IPage page, IWebDriver driver)
        {
            var elements = page.GetType().GetProperties().Where(i => i.GetCustomAttribute<FindWhereAttribute>() != null);
            foreach (var element in elements)
            {
                var findWhere = element.GetCustomAttribute<FindWhereAttribute>();
                try
                {
                    var value = new HtmlElement(driver.FindElement(findWhere.Finder), this.Browser);
                    element.SetValue(page, value);
                }
                catch (OpenQA.Selenium.NoSuchElementException exception)
                {
                    // Leave it as an empty element.
                }

            }

            return page;
        }

        #region IAutoWeb Implementation
        /// <summary>
        /// Adds an IPage type into the AutoWeb collection.
        /// </summary>
        /// <typeparam name="T">The page object, which must implement <see cref="IPage"/>.</typeparam>
        /// <returns>The current <see cref="IAutoWeb"/> interface.</returns>
        public IAutoWeb AddPage<T>() where T : IPage
        {
            IPage instance;
            if (provider != null)
            {
                instance = ActivatorUtilities.CreateInstance(provider, typeof(T)) as IPage;
            }
            else
            {
                instance = Activator.CreateInstance(typeof(T)) as IPage;
            }

            this.pages.Add(instance);
            return this;
        }


        /// <summary>
        /// Executes all pages.
        /// </summary>
        public void Execute()
        {
            this.OpenBrowser();

            foreach (var page in this.pages)
            {
                if (!this.Execute(page.GetType()))
                {
                    throw new InvalidPageValidationException($"Validation for {page.GetType().FullName} failed.");
                }
            }
        }

        public void GoTo(string url)
        {
            if (this.Driver == null)
            {
                throw new NullReferenceException($"You must open the browser before you change the page.");
            }

            Driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Executes a specific page.
        /// </summary>
        /// <param name="type">Must implement from <see cref="IPage"/>.</param>
        /// <returns>The pages validation result.</returns>
        public bool Execute(Type type)
        {
            if (this.Driver == null || this.Browser == null)
            {
                throw new NullReferenceException($"The browser must be opened before executing a page.");
            }

            var page = this.pages.SingleOrDefault(i => i.GetType() == type);
            if (page == null)
            {
                throw new NullReferenceException($"No page exists with type of {type.Name}.");
            }

            // Load page url.
            var pageInfo = page.GetType().GetCustomAttribute<PageAttribute>();
            if (pageInfo == null)
            {
                throw new NullReferenceException($"{page.GetType().Name} does not implement the Page attribute.");
            }

            this.Driver.Url = pageInfo.Url;

            // Set the default timeout for the browser instance, this can be set per page.
            var timeout = options.BrowserOptions.Timeout;
            if(pageInfo.PageLoadTimeout != default)
            {
                timeout = pageInfo.PageLoadTimeout;
                this.Browser.SetWaitTime(timeout);
            }

            // Wait for the page to load.
            new WebDriverWait(this.Driver, timeout).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            // If the user specifies to wait for an element, attempt to wait for it.
            // This is dependent on the pages timeout set in Page Attribute.
            var waitInfo = page.GetType().GetCustomAttribute<WaitForElementAttribute>();
            if (waitInfo != null)
            {
                if(!this.Browser.TryWaitFor(waitInfo.Where.Where, waitInfo.Where.Path, out IHtmlElement element))
                {
                    throw new WaitForElementException($"The page timed out ({timeout}) while waiting to find an element where the {waitInfo.Where.Where} matches \"{waitInfo.Where.Path}\".");
                }
            }

            Populate(page, this.Driver);
            page.Execute(this.Browser);

            // Reset the wait time before moving to the next page.
            this.Browser.ResetWaitTime();
            return page.Validate(this.Browser);
        }

        /// <summary>
        /// Quits the browser.
        /// </summary>
        public void Dispose()
        {
            this.Driver.Quit();
        }
        #endregion

        #region IEnumerable<IPage> Implementation
        public IEnumerator<IPage> GetEnumerator()
        {
            foreach (var page in pages)
            {
                yield return page;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion


    }
}
