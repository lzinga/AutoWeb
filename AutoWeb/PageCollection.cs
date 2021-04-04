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

namespace AutoWeb
{
    /// <summary>
    /// The entry object for all things AutoWeb.
    /// </summary>
    public class PageCollection : IAutoWeb
    {
        List<IPage> pages = new List<IPage>();
        private IWebDriver driver;
        private readonly PageCollectionOptions options = new PageCollectionOptions();
        private readonly IServiceProvider provider = null;

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
                this.options = PageCollectionOptions.DefaultEdge;
            }

            // TODO: Make this independent of each AutoWeb if possible.
            if (this.options.CleanOrphanedDrivers)
            {
                var orphans = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(this.options.DriverPath));
                foreach (var orphan in orphans)
                {
                    orphan.Kill();
                }
            }
        }

        /// <summary>
        /// Quits out of the browser.
        /// </summary>
        ~PageCollection()
        {
            if(driver != null)
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// An internal page collection used for dependency injection.
        /// <para>I don't believe this is working 100%</para>
        /// </summary>
        /// <param name="provider"></param>
        internal PageCollection(IServiceProvider provider) : this()
        {
            this.provider = provider;
        }

        /// <summary>
        /// Creates a collection of pages which can be executed individually or all together.
        /// </summary>
        public PageCollection() : this(options: null) { }

        public void OpenBrowser(string url)
        {
            string path = "";
            string name = "";
            if (Path.IsPathRooted(options.DriverPath))
            {
                path = Path.GetFullPath(options.DriverPath);
                name = Path.GetFileName(options.DriverPath);
            }
            else
            {
                path = Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                name = Path.GetFileName(options.DriverPath);
            }

            // Ensure it is a driver and that the file exists.
            var driverOptions = new EdgeOptions();
            driverOptions.UseChromium = true;
            if (options.BrowserArguments != null && options.BrowserArguments.Length > 0)
            {
                //driverOptions.AddArguments(options.BrowserArguments);
            }

            var chromeDriverService = EdgeDriverService.CreateChromiumService(path, name);
            chromeDriverService.EnableVerboseLogging = false;
            chromeDriverService.HideCommandPromptWindow = true;

            this.driver = new EdgeDriver(chromeDriverService, driverOptions);
            this.Browser = new DefaultBrowser(driver, options.DefaultTimeOut);

            if (!string.IsNullOrEmpty(url))
            {
                this.driver.Navigate().GoToUrl(url);
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
            if (driver == null)
                throw new NullReferenceException($"You must open the browser before you change the page.");


            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Executes a specific page.
        /// </summary>
        /// <param name="type">Must implement from <see cref="IPage"/>.</param>
        /// <returns>The pages validation result.</returns>
        public bool Execute(Type type)
        {
            if (driver == null || this.Browser == null)
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

            driver.Url = pageInfo.Url;

            // Set the default timeout for the browser instance, this can be set per page.
            var timeout = options.DefaultTimeOut;
            if(pageInfo.PageLoadTimeout != default(TimeSpan))
            {
                timeout = pageInfo.PageLoadTimeout;
                this.Browser.SetWaitTime(timeout);
            }

            // Wait for the page to load.
            new WebDriverWait(driver, timeout).Until(
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

            Populate(page, driver);
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
            driver.Quit();
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
