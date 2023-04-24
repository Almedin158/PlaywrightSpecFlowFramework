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

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        //Use both BeforeTestRun and BeforeScenario in case there are situation where you would like to skip logging in, if there is no situation, just use a basic BeforeScenario without importing the state.json
        //BeforeTestRun should perform the login, after that the state is saved inside of the state.json
        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            var playwright = await Playwright.CreateAsync();

            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] { "--start-maximized" },
                Timeout = 5000
            });
            BrowserContext = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });

            var loginPage = await BrowserContext.NewPageAsync();
            await loginPage.GotoAsync("https://www.saucedemo.com/");
            await loginPage.Locator("input[id='user-name']").FillAsync("standard_user");
            await loginPage.Locator("input[id='password']").FillAsync("secret_sauce");
            await loginPage.Locator("input[id='login-button']").ClickAsync();

            await BrowserContext.StorageStateAsync(new()
            {
                Path = "state.json"
            });

            await loginPage.CloseAsync();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var playwright = await Playwright.CreateAsync();
            //Initialise a browser - 'Chromium' can be changed to 'Firefox' or 'Webkit'
            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, // -> Use this option to be able to see your test running

                //Args = new[] { "--start-maximized" } with ViewportSize = ViewportSize.NoViewport will maximize the browser. THIS ONLY WORKS FOR CHROMIUM.
                Args = new[] { "--start-maximized" },
                Timeout = 5000
            });

            //Setup a browser context
            //State is imported based on tags, at the moment, if a test scenario has any tags, the state will be imported, this can also be changed so a specific tag causes the import, in case the general t
            var browserContextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            };
            if (_scenarioContext.ScenarioInfo.Tags.Any())
            {
                browserContextOptions.StorageStatePath = "state.json";
            }
            BrowserContext = await Browser.NewContextAsync(browserContextOptions);

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
    }
}