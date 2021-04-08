using AutoWeb.Browsers;
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
        [TestMethod]
        public void TestAlternativeBrowserChrome()
        {
            var local = new PageCollection(options =>
            {
                options.Browser<ChromeBrowser>("chromedriver.exe");
            });

            local.OpenBrowser();

            var process = Process.GetProcessesByName("chromedriver");
            Assert.IsNotNull(process);
            Assert.IsTrue(process.Length == 1);

            local.Dispose();
            Assert.IsTrue(process[0].HasExited);
        }


        [TestMethod]
        public void TestIfBrowserOpensAndCloses()
        {
            var local = new PageCollection();
            local.OpenBrowser();
            var process = Process.GetProcessesByName("msedgedriver");
            Assert.IsNotNull(process);
            Assert.IsTrue(process.Length == 1);

            local.Dispose();
            Assert.IsTrue(process[0].HasExited);
        }

        [TestMethod]
        public void TestBrowserNavigation()
        {
            using var local = new PageCollection();
            local.OpenBrowser(Constants.TemplateFile);

            var url = Path.GetFullPath(local.Browser.Url.Replace("file:///", ""));
            Assert.IsTrue(url == Constants.TemplateFile);
        }

        [TestMethod]
        public void TestBrowserTryWaitFor()
        {
            using var local = new PageCollection();
            local.OpenBrowser(Constants.TemplateFile);

            var noElement = local.Browser.TryWaitFor(Where.Id, "no-element-exists", out IHtmlElement element2);
            Assert.IsFalse(noElement);
            Assert.IsNull(element2);


            var found = local.Browser.TryWaitFor(Where.Id, "hidden-for-3-seconds", out IHtmlElement element3);
            Assert.IsTrue(found);
            Assert.IsNotNull(element3);

        }
    }
}
