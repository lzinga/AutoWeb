using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Common.Internal
{
    internal static class SeleniumExtensions
    {
        /// <summary>
        /// Converts a Where statement into a <see cref="By"/> statement for Selenium.
        /// </summary>
        /// <param name="where">The element type to find by.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns></returns>
        internal static By GetBy(this Where where, string value)
        {
            switch (where)
            {
                case Where.Id:
                    return By.Id(value);
                case Where.Name:
                    return By.Name(value);
                case Where.Class:
                    return By.ClassName(value);
                case Where.Css:
                    return By.CssSelector(value);
                case Where.Tag:
                    return By.TagName(value);
                case Where.XPath:
                    return By.XPath(value);
                default:
                    return null;
            }
        }
    }
}
