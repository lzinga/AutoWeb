using AutoWeb.WebElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoWeb.Test
{
    [TestClass]
    public class HtmlElementTest
    {
        public PageCollection autoweb;

        [TestInitialize]
        public void Initialize()
        {
            var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            autoweb = new PageCollection();
            autoweb.OpenBrowser(Constants.TemplateFile);
        }

        [TestCleanup]
        public void Cleanup()
        {
            autoweb.Dispose();
        }


        [TestMethod]
        public void TestFindElementWithText()
        {
            Assert.IsNotNull(autoweb.Browser.FindElementWithText("Basic Card"));
        }


        [TestMethod]
        public void TestFindClosestAncestor()
        {
            // Get the regular list.
            var element = autoweb.Browser.FindElement(Where.Id, "united-states");
            Assert.IsNotNull(element);

            Assert.IsNotNull(element.FindClosestAncestor("ul"));
            Assert.IsTrue(element.FindClosestAncestor("ul").GetAttribute("id") == "regular-list");
        }

        [TestMethod]
        public void TestElementDisplayed()
        {
            Assert.IsTrue(autoweb.Browser.FindElement(Where.Id, "basic-card").Displayed);
            Assert.IsFalse(autoweb.Browser.FindElement(Where.Id, "hidden-title").Displayed);

            // The element should not be displayed
            var delayedElement = autoweb.Browser.FindElement(Where.Id, "hidden-for-3-seconds");
            Assert.IsFalse(delayedElement.Displayed);

            // Sleep for the element to be displayed.
            Thread.Sleep(3500);

            // The element should be displayed now.
            delayedElement = autoweb.Browser.FindElement(Where.Id, "hidden-for-3-seconds");
            Assert.IsTrue(delayedElement.Displayed);

        }

        [TestMethod]
        public void TestInteractivity()
        {
            Assert.IsTrue(autoweb.Browser.FindElement(Where.Id, "button-enabled").IsInteractive);
            Assert.IsFalse(autoweb.Browser.FindElement(Where.Id, "button-disabled").IsInteractive);
        }



        [TestMethod]
        public void TestHasChildren()
        {
            Assert.IsTrue(autoweb.Browser.FindElement(Where.Id, "regular-list").HasChildren);
            Assert.IsFalse(autoweb.Browser.FindElement(Where.Id, "united-states").HasChildren);
        }


    }
}
