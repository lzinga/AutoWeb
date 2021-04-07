using AutoWeb.Common.Internal;
using AutoWeb.Extensions;
using AutoWeb.WebElements;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoWeb.Common.Internal
{
    internal class Action : IActionable, IElementWrapper
    {
        public Action(IHtmlElement element, IBrowser browser)
        {
            this.Element = element;
            this.Browser = browser;
        }

        public IHtmlElement Element { get; private set; }

        public IBrowser Browser { get; private set; }

        public IActionable Click()
        {
            ((IWebElement)this.Element).Click();
            return this;
        }

        public IActionable PressEnter()
        {
            ((IWebElement)this.Element).SendKeys(Utilities.ToInvariantCulture(Key.Enter));
            return this;
        }

        public IActionable Submit()
        {
            ((IWebElement)this.Element).Submit();
            return this;
        }

        public IActionable Wait(int milliseconds)
        {
            // Could probably find a better way to wait.
            Thread.Sleep(milliseconds);
            return this;
        }
    }
}
