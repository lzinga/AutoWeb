using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb
{
    /// <summary>
    /// The configuration of how the page will be searched.
    /// </summary>
    public enum Where
    {
        /// <summary>
        /// The Id attribute of the html element.
        /// </summary>
        Id,

        /// <summary>
        /// The Name attribute of the html element.
        /// </summary>
        Name,

        /// <summary>
        /// The Class attribute of the html element.
        /// <para>No spaces allowed.</para>
        /// </summary>
        Class,

        /// <summary>
        /// The element where the Css selector matches.
        /// </summary>
        Css,

        /// <summary>
        /// The element where the Tag equals.
        /// </summary>
        Tag,

        /// <summary>
        /// The element that matches the XPath
        /// </summary>
        XPath
    }
}
