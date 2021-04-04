using AutoWeb.Attributes;
using AutoWeb.WebElements;

namespace AutoWeb.Example.Pages.StarRepository
{
    [Page("Login", "https://github.com/login")]
    public class LoginPage : IPage
    {
        [FindWhere(Where.Name, "login")]
        public IHtmlElement Username { get; set; }

        [FindWhere(Where.Name, "password")]
        public IHtmlElement Password { get; set; }

        [FindWhere(Where.Name, "commit")]
        public IHtmlElement Submit { get; set; }

        public void Execute(IBrowser browser)
        {
            Username.SendKeys("<your username>");
            Password.SendKeys("<your password>");
            Submit.Click();
        }

        public bool Validate(IBrowser browser)
        {
            
            // Before moving to the next page, wait for the profile image to appear.
            return browser.TryWaitFor(Where.Css,
                "body > div.position-relative.js-header-wrapper > header > div.Header-item.position-relative.mr-0.d-none.d-md-flex > details > summary > img",
                out IHtmlElement _);
        }
    }
}
