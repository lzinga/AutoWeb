# AutoWeb
This library wraps [Selenium](https://github.com/SeleniumHQ/selenium) into an easy to use library for any
automated tasks you may require in the browser.

[![Run-Tests](https://github.com/lzinga/AutoWeb/actions/workflows/run-tests.yml/badge.svg)](https://github.com/lzinga/AutoWeb/actions/workflows/run-tests.yml)

[![Build-Packages](https://github.com/lzinga/AutoWeb/actions/workflows/build-packages.yml/badge.svg)](https://github.com/lzinga/AutoWeb/actions/workflows/build-packages.yml)

##### Pull Requests/Issues
Please feel free to open an issue or pull request. I am sure there are some things that
can be improved in the library and I welcome any suggestions/requests.

## Why?
There were many times I wanted to create a quick project that would automate a process I do often, or tasks that are cumbersome and annoying. However from my experience
Selenium had quite a bit of overhead and can feel clunky/messy if you wanted to spin up something quickly. So here we are.

As an example I have to open tickets in part of my job in a system that is __not__ user friendly, does not give access to API, nor was it built with my group in mind. Alas we are still required to open tickets in said system. So... I will be using this library to automate the process to be much simpler and quicker for my team. I leave you with the hopes that this library can help in some way or another.

## Usage
To properly load and open a browser you will need to include a web driver, by default
AutoWeb uses [Selenium.WebDriver.MSEdgeDriver](https://www.nuget.org/packages/Selenium.WebDriver.MSEdgeDriver/89.0.774.54)
as the default driver but you can view the [#tested-browsers](Tested Browsers). I have not tested with other browsers but it should be possible with some slight modifications.

AutoWeb uses a `PageCollection` object to add and manage your pages.
```csharp
// Creates a default page collection. (uses msedgedriver)
new PageCollection();

// Creates a page collection with configured options.
new PageCollection(options => {

    // Option #1
    // Option to specify a different browser.
    options.Browser<IBrowser>("chromedriver.exe")

    // Option #2
    // Option to specify all options for the browser
    options.Browser<ChromeBrowser>(opts => {
        opts.Driver = "chromedriver.exe";
        opts.Timeout = new TimeSpan(0, 0, 30);
        opts.Arguments = new string[] {
            "--headless"
        };
    });

    // At times the driver will remain running in the background, this will
    // attempt to clear them before starting a new one.
    options.CleanOrphanedDrivers = true,
});
```

Once created you can add pages later or directly off the new object itself. Either of these methods are fine.
```csharp
new PageCollection()
    .AddPage<LoginPage>()
    .AddPage<RepositoryPage>();

var pages = new PageCollection();
pages.Add<LoginPage>();
```

Once you have pages added to your collection you can execute them individually,
or all in the order they were added.
```csharp
// This will execute all pages starting at LoginPage and continuing to RepositoryPage
new PageCollection()
    .AddPage<LoginPage>()
    .AddPage<RepositoryPage>()
    .Execute()

// If executing against the variable itself you can execute a singular page.
pages.Execute<LoginPage>();

```


## Pages
To create a page you must create a class which implements `IPage` and apply the
`PageAttribute` to it. You can view an example of a page below or within the example project
included in the solution.

```csharp
[Page("Login", "https://github.com/login")]
public class LoginPage : IPage
{
    [FindWhere(Where.Name, "login")]
    public IHtmlElement Username { get; set; }

    [FindWhere(Where.Name, "password")]
    public IHtmlElement Password { get; set; }

    [FindWhere(Where.Name, "commit")]
    public IHtmlElement Submit { get; set; }

    // When the page is called to execute this is the process
    // that will be executed.
    public void Execute(IBrowser browser)
    {
        Username.SendKeys("<your username>");
        Password.SendKeys("<your password>");
        Submit.Click();
    }

    // When the page is complete this must return true before it
    // continues to the next page (if executing many)
    public bool Validate(IBrowser browser)
    {
            
        // Before moving to the next page, wait for the profile image to appear.
        return browser.TryWaitFor(Where.Css,
            "body > div.position-relative.js-header-wrapper > header > div.Header-item.position-relative.mr-0.d-none.d-md-flex > details > summary > img",
            out IHtmlElement _);
    }
}
```

##### Attributes
`PageAttribute` - Used to specify the pages metadata.
1. `Name`: The friendly name of the page that will be loaded.
2. `Url`: The URL that will be navigated too when this page is executed.
3. `pageLoadTimeout`: The individual load time for this page, does not affect other pages.. (Defaults to PageCollectionOptions)

`WaitForElementAttribute` - Applies to the Page class to wait for the specified element before executing. (Uses page timeout if specified)
1. `Where`: The defining type of how the selection will be found.
2. `Value`: The query used for selecting the element. (For Example - XPath, CSS, Class, etc)

`FindWhereAttribute` - Applies to Page properties to load the html element on page load.
##### Chain Commands
When writing automation for a page things can get messy pretty quickly if the website has you jump
around a lot. You can chain commands like in the example below.
```csharp
browser.FindElement(Where.XPath, "//*[@id='sp_formfield_search']")
    .SendKeys("TicketVM01")
    .Wait(1000)
    .ThenFindElement(Where.XPath, "//*[@id='Add']/div/span/div/div/div[2]/input[2]")
    .Click();
```

# Tested Browsers
The following browser drivers have been tested and currently integrated into unit tests.

1. [Selenium.WebDriver.MSEdgeDriver](https://www.nuget.org/packages/Selenium.WebDriver.MSEdgeDriver)
1. [Selenium.WebDriver.ChromeDriver](https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver)