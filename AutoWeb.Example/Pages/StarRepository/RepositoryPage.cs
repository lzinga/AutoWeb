using AutoWeb.Attributes;
using AutoWeb.Browsers;
using AutoWeb.Common;
using AutoWeb.WebElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Example.Pages.StarRepository
{
    [Page("StarRepo", "https://github.com/lzinga/AutoWeb")]
    public class RepositoryPage : IPage
    {

        [FindWhere(Where.XPath, @"//*[@id='js-repo-pjax-container']/div[1]/div[1]/ul/li[2]/div/form[2]/button")]
        public IHtmlElement StarButton { get; set; }


        public void Execute(IBrowser browser)
        {
            if (StarButton.Displayed)
            {
                StarButton.Click();
                Console.WriteLine("The repository has been starred!");
            }
            else
            {
                Console.WriteLine("You already starred the repository!");
            }
        }
    }
}
