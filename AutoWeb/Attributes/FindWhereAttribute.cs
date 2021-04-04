using AutoWeb.Common.Internal;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace AutoWeb
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FindWhereAttribute : AbstractFindsByAttribute
    {
        public override By Finder { get; }

        /// <summary>
        /// Finds the element on the page and sets the property as its value.
        /// </summary>
        /// <param name="where"><see cref="Where"/></param>
        /// <param name="value">The value in which to search by.</param>
        public FindWhereAttribute(Where where, string value)
        {
            this.Finder = where.GetBy(value);
        }
    }
}
