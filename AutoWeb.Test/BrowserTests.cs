using AutoWeb.WebElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace AutoWeb.Test
{
    [TestClass]
    public class BrowserTests
    {

        public PageCollection autoweb;


        [TestInitialize]
        public void Initialize()
        {
            autoweb = new PageCollection();
        }

        [TestCleanup]
        public void Cleanup()
        {
            autoweb.Dispose();
        }



        [TestMethod]
        public void TestIfBrowserOpensAndCloses()
        {
            autoweb.OpenBrowser();
            var process = Process.GetProcessesByName("msedgedriver");
            Assert.IsNotNull(process);
            Assert.IsTrue(process.Length == 1);

            autoweb.Dispose();
            Assert.IsTrue(process[0].HasExited);
        }

        [TestMethod]
        public void TestBrowserNavigation()
        {
            autoweb.OpenBrowser(Constants.TemplateFile);

            var url = Path.GetFullPath(autoweb.Browser.Url.Replace("file:///", ""));
            Assert.IsTrue(url == Constants.TemplateFile);
        }

        [TestMethod]
        public void TestBrowserTryWaitFor()
        {
            autoweb.OpenBrowser(Constants.TemplateFile);

            var noElement = autoweb.Browser.TryWaitFor(Where.Id, "no-element-exists", out IHtmlElement element2);
            Assert.IsFalse(noElement);
            Assert.IsNull(element2);


            var found = autoweb.Browser.TryWaitFor(Where.Id, "hidden-for-3-seconds", out IHtmlElement element3);
            Assert.IsTrue(found);
            Assert.IsNotNull(element3);
        }
    }
}
