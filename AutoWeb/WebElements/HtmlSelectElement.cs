using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.WebElements
{
    public class HtmlSelectElement : SelectElement
    {
        public HtmlSelectElement(IHtmlElement element) : base(element as IWebElement)
        {

        }
    }
}
