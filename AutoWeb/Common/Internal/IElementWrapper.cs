using AutoWeb.WebElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Common.Internal
{
    internal interface IElementWrapper
    {
        IBrowser Browser { get; }
        IHtmlElement Element { get; }
    }
}
