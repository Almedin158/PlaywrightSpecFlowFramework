using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

//Allows parallel executions of test cases inside different feature files (meaning multiple test cases in one feature file can not run in parallel)
[assembly:Parallelizable(ParallelScope.Fixtures)]
//Limits the number of possible parallel executions
[assembly:LevelOfParallelism(2)]

namespace PSF.Support
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        public IPage Page;
        public static IBrowser Browser;
        public static IBrowserContext BrowserContext;
        public static IPlaywright PlaywrightContext;

        enum Browsers
        {
            Chromium,
            Firefox,
            WebKit
        }

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //Use both BeforeTestRun and BeforeScenario in case there are situation where you would like to skip logging in, if there is no situation, just use a basic BeforeScenario without importing the state.json
        //BeforeTestRun should perform the login, after that the state is saved inside of the state.json
        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            PlaywrightContext = await Playwright.CreateAsync();

            Browser = await InitializeBrowser(Browsers.Chromium.ToString());

            BrowserContext = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });

            var page = await BrowserContext.NewPageAsync();

            await SetBrowserState(page);

            await page.CloseAsync();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            PlaywrightContext = await Playwright.CreateAsync();
            //Initialise a browser - 'Chromium' can be changed to 'Firefox' or 'Webkit'
            Browser = await InitializeBrowser(Browsers.Chromium.ToString());

            BrowserContext = await GetBrowserState();

            Page = await BrowserContext.NewPageAsync();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await Page.CloseAsync();
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            await BrowserContext.DisposeAsync();

            await Browser.DisposeAsync();
        }

        public static async Task<IBrowser> InitializeBrowser(string browser)
        {
            switch(browser) {
                case "Firefox":
                    return await PlaywrightContext.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = false,
                        Timeout = 5000,
                        SlowMo=500
                    });
                
                case "WebKit":
                    return await PlaywrightContext.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = false,
                        Timeout = 5000,
                        SlowMo=500
                    });
            }

            return await PlaywrightContext.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] { "--start-maximized" },
                Timeout = 5000,
                SlowMo = 500
            });
        }

        public static async Task SetBrowserState(IPage page)
        {
            //Perform the login here
            await page.GotoAsync("https://www.saucedemo.com/");
            await page.Locator("input[id='user-name']").FillAsync("standard_user");
            await page.Locator("input[id='password']").FillAsync("secret_sauce");
            await page.Locator("input[id='login-button']").ClickAsync();

            await BrowserContext.StorageStateAsync(new()
            {
                Path = "state.json"
            });
        }
        public async Task<IBrowserContext> GetBrowserState()
        {
            //State is imported based on tags, at the moment, if a test scenario has any tags, the state will be imported, this can also be changed so a specific tag causes the import.
            var browserContextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            };
            if (_scenarioContext.ScenarioInfo.Tags.Any())
            {
                browserContextOptions.StorageStatePath = "state.json";
            }
            return await Browser.NewContextAsync(browserContextOptions);
        }
    }
}