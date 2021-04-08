using AutoWeb.Browsers;
using AutoWeb.WebElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb
{
    public interface IPage
    {
        void Execute(IBrowser browser);
        bool Validate(IBrowser browser) => true;
    }
}
