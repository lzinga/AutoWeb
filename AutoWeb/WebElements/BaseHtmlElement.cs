using AutoWeb.Browsers;
using AutoWeb.Common.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.WebElements
{
    public abstract class BaseHtmlElement : IWebElement, IHtmlElement
    {
        #region IWebElement
        internal readonly IWebElement element;
        internal readonly IBrowser browser;

        public string TagName => element.TagName;

        public string Text => element.Text;

        public bool Enabled => element.Enabled;

        public bool Selected => element.Selected;

        public Point Location => element.Location;

        public Size Size => element.Size;

        public bool Displayed => element.Displayed;

        public void Clear() => element.Clear();

        public void Click() => element.Click();

        public IWebElement FindElement(By by) => element.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By by) => element.FindElements(by);

        public string GetAttribute(string attributeName) => element.GetAttribute(attributeName);

        public string GetCssValue(string propertyName) => element.GetCssValue(propertyName);

        public string GetProperty(string propertyName) => element.GetProperty(propertyName);

        public void SendKeys(string text) => element.SendKeys(text);

        public void Submit() => element.Submit();


        public string Html => element.GetAttribute("outerHTML").Trim();

        public string Tag => element.TagName.Trim();
        #endregion

        #region IHtmlElement
        public bool HasChildren => element.FindElements(By.XPath(".//*")).Any();

        public bool IsInteractive => element.Displayed && element.Enabled;


        /// <summary>
        /// Returns all elements where the text is found in the inner html.
        /// </summary>
        /// <param name="text">The text to search.</param>
        /// <returns><see cref="IHtmlElement"/></returns>
        public IEnumerable<IHtmlElement> FindElementsWithText(string text)
        {
            return this.FindElements(Where.XPath, @$"//*[normalize-space()='{text}']");
        }

        #endregion

        public T As<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { this });
        }

        internal BaseHtmlElement(IWebElement element)
        {
            this.element = element;
        }

        internal BaseHtmlElement(IWebElement element, IBrowser browser) : this(element)
        {
            this.browser = browser;
        }

        public IHtmlElement FindElement(Where where, string value)
        {
            var query = this.FindElement(where.GetBy(value));
            return new HtmlElement(query, this.browser);
        }

        public IEnumerable<IHtmlElement> FindElements(Where where, string value)
        {
            var query = this.FindElements(where.GetBy(value));
            return query.Select(i => new HtmlElement(i, this.browser));
        }


        public IHtmlElement FindClosestAncestor(string xpath)
        {
            return this.FindElement(Where.XPath, $"ancestor-or-self::{xpath}");
        }

        public T GetAttribute<T>(string attribute)
        {
            var val = this.GetAttribute(attribute);
            return (T)Convert.ChangeType(val, typeof(T));
        }

        IActionable IHtmlElement.SendKeys(string text, bool clear, bool wait)
        {
            
            if (wait && this.browser != null)
            {
                this.browser.TryWaitFor(this);
            }

            if (clear && !string.IsNullOrEmpty(this.Text))
            {
                this.Clear();
            }

            
            element.SendKeys(text);
            return new Common.Internal.Action(this, browser);
        }

        IActionable IHtmlElement.Click()
        {
            element.Click();
            return new Common.Internal.Action(this, browser);
        }

        IActionable IHtmlElement.Submit()
        {
            element.Submit();
            return new Common.Internal.Action(this, browser);
        }
    }
}
