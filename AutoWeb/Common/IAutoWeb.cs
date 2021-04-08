using AutoWeb.Browsers;
using AutoWeb.WebElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Common
{
    public interface IAutoWeb : IEnumerable<IPage>, IDisposable
    {

        /// <summary>
        /// The browser created from within IAutoWeb.
        /// </summary>
        IBrowser Browser { get; }

        /// <summary>
        /// Adds a page to the IAutoWeb collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IAutoWeb AddPage<T>() where T : IPage;

        /// <summary>
        /// Executes all pages in the IAutoWeb.
        /// </summary>
        void Execute();

        /// <summary>
        /// Used to go to a specific url or file.
        /// </summary>
        /// <param name="url"></param>
        void GoTo(string url);

        /// <summary>
        /// Opens the browser.
        /// </summary>
        void OpenBrowser();

        /// <summary>
        /// Opens the browser and navigates to the specified page.
        /// </summary>
        /// <param name="url">The url to navigate to on open.</param>
        void OpenBrowser(string url);

        /// <summary>
        /// Executes a single pages actions.
        /// </summary>
        /// <typeparam name="T">The page to execute.</typeparam>
        /// <returns>The page validation result.</returns>
        bool Execute<T>() where T : IPage => Execute(typeof(T));

        /// <summary>
        /// Executes a page by its type.
        /// </summary>
        /// <param name="type">The IPage implemented object.</param>
        /// <returns>The pages validation result.</returns>
        bool Execute(Type type);
    }
}
