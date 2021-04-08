using AutoWeb.Common.Internal;
using AutoWeb.Exceptions;
using AutoWeb.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Browsers
{
    /// <summary>
    /// The base type of a browser, implements all expected browser methods and actions.
    /// </summary>
    public abstract class Browser : IBrowser
    {
        internal TimeSpan Timeout { get; set; }
        internal IWebDriver Driver { get; set; }
        internal WebDriverWait DriverWait { get; set; }

        public string Url => Driver.Url;

        public FileInfo ValidateDriver(string driver)
        {
            FileInfo fileInfo;
            // TODO: Validate driver file.
            if (Path.IsPathRooted(driver))
            {
                fileInfo = new FileInfo(driver);
            }
            else
            {
                string path = Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                fileInfo = new FileInfo(Path.Combine(path, driver));
            }

            if (File.Exists(fileInfo.FullName) && fileInfo.Extension.Equals(".exe", StringComparison.OrdinalIgnoreCase))
            {
                return fileInfo;
            }

            throw new BrowserValidationException($"The browser driver '{driver}' could not be found or is not an executable.");
        }

        public IHtmlElement FindElement(Where where, string value)
        {
            By by = where.GetBy(value);
            return new HtmlElement(this.Driver.FindElement(by), this);
        }

        public IEnumerable<IHtmlElement> FindElements(Where where, string value)
        {
            By by = where.GetBy(value);
            return this.Driver.FindElements(by).Select(i => new HtmlElement(i, this));
        }

        public IEnumerable<IHtmlElement> FindElementsWithText(string text)
        {
            return this.FindElements(Where.XPath, @$"//*[normalize-space(.)='{text}']");
        }

        public void ResetWaitTime()
        {
            this.DriverWait.Timeout = this.Timeout;
        }

        public void SetWaitTime(TimeSpan timeout)
        {
            this.DriverWait.Timeout = this.Timeout;
        }

        public bool TryWaitFor(Where where, string value, out IHtmlElement element)
        {
            try
            {
                By by = where.GetBy(value);

                var elementExists = this.DriverWait.Until(i =>
                {
                    try
                    {
                        i.FindElement(by);
                        return true;
                    }
                    catch (NoSuchElementException ex)
                    {
                        return false; //By returning false, wait will still rerun the func.
                    }
                });

                if (elementExists)
                {
                    var e = this.Driver.FindElement(by);
                    element = new HtmlElement(e, this);
                    return true;
                }
                else
                {
                    element = null;
                    return false;
                }
            }
            catch (WebDriverTimeoutException)
            {
                element = null;
                return false;
            }
        }

        public bool TryWaitFor(IHtmlElement element)
        {
            return this.DriverWait.Until((d) =>
            {
                try
                {
                    if (element != null && element.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    return false;
                }
            });
        }

        public void WaitFor(Where where, string value)
        {
            try
            {
                By by = where.GetBy(value);
                this.DriverWait.Until(x => x.FindElement(by) != null);
            }
            catch (NoSuchElementException exception)
            {
                throw new NullReferenceException(exception.Message);
            }
        }
    }
}
