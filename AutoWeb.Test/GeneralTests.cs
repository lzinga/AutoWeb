using AutoWeb.Common;
using AutoWeb.Test.Common;
using AutoWeb.WebElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoWeb.Test
{
    [TestClass]
    public class GeneralTests
    {

        [TestMethod]
        public void KeyEnumToSeleniumStringKeyTest()
        {
            // We are verifying the Selenium keys equate out to our enum values properly.
            foreach(var property in typeof(SeleniumKeys).GetFields())
            {
                if (Enum.TryParse(property.Name, true, out Key key))
                {
                    Assert.AreEqual(property.GetValue(null), Utilities.ToInvariantCulture(key));
                }
            }
        }



    }
}
