using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.WebElements
{
    public interface IHtmlElement
    {
        /// <summary>
        /// The text value of the element.
        /// </summary>
        string Text { get; }

        string Html { get; }

        string Tag { get; }

        /// <summary>
        /// Is the element selected.
        /// </summary>
        bool Selected { get; }

        /// <summary>
        /// Does the element have children.
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// If the element is visible.
        /// </summary>
        bool Displayed { get; }

        /// <summary>
        /// Determines if the element is enabled and visible.
        /// </summary>
        bool IsInteractive { get; }

        /// <summary>
        /// Gets the attribute value from the current <see cref="IHtmlElement"/>
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        string GetAttribute(string attribute);

        /// <summary>
        /// Gets the attribute value from the current <see cref="IHtmlElement"/>
        /// and tries to convert it to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attribute"></param>
        /// <returns><typeparamref name="T"/></returns>
        T GetAttribute<T>(string attribute);

        /// <summary>
        /// Finds an element inside the current element.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IHtmlElement FindElement(Where where, string value);

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

        /// <summary>
        /// Finds the closest ancestor to the xpath.
        /// <para>
        /// ./ancestor-or-self::<paramref name="xpath"/>
        /// </para>
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        IHtmlElement FindClosestAncestor(string xpath);

        /// <summary>
        /// Finds all elements inside the current element.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<IHtmlElement> FindElements(Where where, string value);

        T As<T>();

        /// <summary>
        /// Clicks the element if available.
        /// </summary>
        IActionable Click();

        IActionable Submit();


        IActionable SendKeys(string text) => SendKeys(text, true);
        IActionable SendKeys(string text, bool clear) => SendKeys(text, true, true);
        IActionable SendKeys(string text, bool clear, bool wait);
    }
}
