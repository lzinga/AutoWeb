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
        /// <summary>
        /// Execute your pages automation.
        /// </summary>
        /// <param name="browser"></param>
        void Execute(IBrowser browser);

        /// <summary>
        /// Validate your automation ran succesfully before moving to the next page (if applicable)
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        bool Validate(IBrowser browser) => true;
    }
}
