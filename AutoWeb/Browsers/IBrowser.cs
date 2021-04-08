using AutoWeb.WebElements;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Browsers
{
    public interface IBrowser
    {
        /// <summary>
        /// The current URL the browser is on.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Wait until the element is found.
        /// </summary>
        /// <param name="where"><see cref="Where"/></param>
        /// <param name="value"></param>
        void WaitFor(Where where, string value);

        bool TryWaitFor(Where where, string value, out IHtmlElement element);
        bool TryWaitFor(IHtmlElement element);

        void ResetWaitTime();
        void SetWaitTime(TimeSpan timeout);
        void SetWaitTime(int seconds) => new TimeSpan(0, 0, seconds);
        void SetWaitTime(int minutes, int seconds) => new TimeSpan(0, minutes, seconds);
        void SetWaitTime(int hours, int minutes, int seconds) => new TimeSpan(hours, seconds, seconds);

        /// <summary>
        /// Finds an element where where the text is found.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IHtmlElement FindElementWithText(string text) => FindElementsWithText(text).FirstOrDefault();

        /// <summary>
        /// Finds an element where where the text is found.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IEnumerable<IHtmlElement> FindElementsWithText(string text);
        IHtmlElement FindElement(Where where, string value);
        IEnumerable<IHtmlElement> FindElements(Where where, string value);
    }

    internal interface IBrowserDriver
    {

        IBrowser LoadDriver(string driver, TimeSpan timeout, params string[] arguments);
    }
}
