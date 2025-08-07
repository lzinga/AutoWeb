<h1 align="center">
    AutoWeb
</h1>
<h6 align="center">
    Created by: <a href="https://github.com/lzinga" target="_blank">Lucas Elzinga</a>
</h6>

<p align="center">This library wraps the <a href="https://github.com/SeleniumHQ/selenium" target="_blank">Selenium</a> library into an easy to use library for any
automated tasks you may require in the browser.</p>
<p align="center">
    <a href="https://www.nuget.org/packages/AutoWeb/" title="Nuget"><img src="https://img.shields.io/nuget/v/AutoWeb?style=for-the-badge" /></a>
</p>

<p align="center">
    <a href="https://www.nuget.org/packages/AutoWeb/" title="Package Building"><img src="https://img.shields.io/github/workflow/status/lzinga/autoweb/Build-Packages/main?style=for-the-badge" /></a>
    <a href="https://github.com/lzinga/AutoWeb/actions/workflows/run-tests.yml" title="Unit Tests"><img src="https://img.shields.io/github/workflow/status/lzinga/autoweb/Run-Tests/main?label=Tests&style=for-the-badge" /></a>
    <a href="https://github.com/lzinga?tab=packages&repo_name=AutoWeb" title="GitHub Package"><img src="https://img.shields.io/github/workflow/status/lzinga/autoweb/Build-Packages/main?label=Packaging&style=for-the-badge" /></a>
</p>

<p align="center">
    <a href="https://github.com/lzinga/AutoWeb/blob/main/LICENSE" title="License"><img src="https://img.shields.io/github/license/lzinga/AutoWeb?style=for-the-badge" /></a>
    <a href="https://github.com/lzinga/AutoWeb/discussions" title="Discussions"><img src="https://img.shields.io/static/v1?label=Community&message=Discussions&style=for-the-badge" /></a>
</p>

## Why?
There were many times I wanted to create a quick project that would automate a process I do often, or tasks that are cumbersome and annoying. However from my experience
Selenium had quite a bit of overhead and can feel clunky/messy if you wanted to spin up something quickly. So here we are.

As an example I have to open tickets in part of my job in a system that is __not__ user friendly, does not give access to API, nor was it built with my group in mind. Alas we are still required to open tickets in said system. So... I will be using this library to automate the process to be much simpler and quicker for my team. I leave you with the hopes that this library can help in some way or another.

## Quick Links
1. [Wiki Documentation](https://github.com/lzinga/AutoWeb/wiki)

## Usage

__Only basic usage is the readme, view the wiki for more detailed information.__

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

## Fluent Control
When writing automation for a page things can get messy pretty quickly if the website has you jump
around a lot. You can chain commands like in the example below.
```csharp
browser.FindElement(Where.XPath, "//*[@id='sp_formfield_search']")
    .SendKeys("TicketVM01")
    .Wait(1000)
    .ThenFindElement(Where.XPath, "//*[@id='Add']/div/span/div/div/div[2]/input[2]")
    .Click();
```

# Supported Drivers
The following browser drivers have been tested and currently built into unit tests.

1. [Selenium.WebDriver.MSEdgeDriver](https://www.nuget.org/packages/Selenium.WebDriver.MSEdgeDriver)
1. [Selenium.WebDriver.ChromeDriver](https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver)
