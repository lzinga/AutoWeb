using AutoWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Attributes
{
    /// <summary>
    /// Used to wait for a specific element to be ready before executing the page.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WaitForElementAttribute : Attribute
    {

        internal WhereClause Where { get; set; }


        /// <summary>
        /// Used to wait for a specific element to be ready before executing the page.
        /// </summary>
        /// <param name="where"><see cref="Where"/></param>
        /// <param name="value">The value query that works with <paramref name="where"/></param>
        public WaitForElementAttribute(Where where, string value)
        {
            this.Where = new WhereClause(where, value);
        }
    }
}
