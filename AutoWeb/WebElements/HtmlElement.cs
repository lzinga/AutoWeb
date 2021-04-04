using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutoWeb.WebElements
{
    internal class HtmlElement : BaseHtmlElement
    {
        internal HtmlElement(IWebElement element) : base(element) { }
        internal HtmlElement(IWebElement element, IBrowser wait) : base(element, wait) { }
    }
}
