using AutoWeb.Common.Internal;
using AutoWeb.WebElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Extensions
{
    public static class IHtmlElementExtensions
    {
        public static IHtmlElement ThenFindElement(this IActionable action, Where where, string value)
        {
            IElementWrapper wrap = (IElementWrapper)action;
            return wrap.Browser.FindElement(where, value);
        }

        public static IEnumerable<IHtmlElement> ThenFindElements(this IActionable action, Where where, string value)
        {
            IElementWrapper wrap = (IElementWrapper)action;
            return wrap.Browser.FindElements(where, value);
        }
    }
}
