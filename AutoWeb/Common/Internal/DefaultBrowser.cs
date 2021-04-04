using AutoWeb.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Common.Internal
{
    internal class DefaultBrowser : IBrowser
    {
        private readonly IWebDriver driver;
        private readonly TimeSpan defaultTimeout;
        private WebDriverWait wait;

        public string Url => this.driver.Url;


        public DefaultBrowser(IWebDriver driver, TimeSpan timeout)
        {
            this.driver = driver;
            this.defaultTimeout = timeout;
            this.wait = new WebDriverWait(driver, timeout);


            this.wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public IHtmlElement FindElement(Where where, string value)
        {
            By by = where.GetBy(value);
            return new HtmlElement(this.driver.FindElement(by), this);
        }

        public IEnumerable<IHtmlElement> FindElements(Where where, string value)
        {
            By by = where.GetBy(value);
            return this.driver.FindElements(by).Select(i => new HtmlElement(i, this));
        }

        public void ResetWaitTime()
        {
            this.wait.Timeout = defaultTimeout;
        }

        public void SetWaitTime(TimeSpan timeout)
        {
            this.wait.Timeout = timeout;
        }

        public bool TryWaitFor(IHtmlElement element)
        {
            return this.wait.Until((d) =>
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

        public bool TryWaitFor(Where where, string value, out IHtmlElement element)
        {
            try
            {
                By by = where.GetBy(value);

                var elementExists = this.wait.Until(i =>
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
                    var e = this.driver.FindElement(by);
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

        public void WaitFor(Where where, string value)
        {
            try
            {
                By by = where.GetBy(value);
                this.wait.Until(x => x.FindElement(by) != null);
            }
            catch(NoSuchElementException exception)
            {
                throw new NullReferenceException(exception.Message);
            }

        }

        /// <summary>
        /// Returns all elements where the text is found in the inner html.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <returns><see cref="IHtmlElement"/></returns>
        public IEnumerable<IHtmlElement> FindElementsWithText(string text)
        {
            return this.FindElements(Where.XPath, @$"//*[normalize-space(.)='{text}']");
        }
    }
}
