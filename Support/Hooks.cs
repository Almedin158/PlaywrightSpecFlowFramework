﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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
        private static ExtentReports ExtentReport;
        private ExtentTest ExtentTest;

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

        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            //Add extent report here
            ExtentReport = new ExtentReports();
            ExtentReport.AttachReporter(new ExtentHtmlReporter(Directory.GetCurrentDirectory()));

            //To skip loggin in each test, you can simply call SetBrowserState and perform the login inside of the function
            await SetBrowserState();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            ExtentTest = ExtentReport.CreateTest(TestContext.CurrentContext.Test.Name);

            PlaywrightContext = await Playwright.CreateAsync();
            //Initialise a browser - 'Chromium' can be changed to 'Firefox' or 'Webkit'
            Browser = await InitializeBrowser(Browsers.Chromium.ToString());

            //This will cause an error if SetBrowserState isn't performed and a state.json file does not exist
            BrowserContext = await GetBrowserState();

            Page = await BrowserContext.NewPageAsync();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logStatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logStatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logStatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logStatus = Status.Skip;
                    break;
                default:
                    logStatus = Status.Pass;
                    break;
            }

            if (logStatus == Status.Fail)
            {
                // Take a screenshot of the browser and save it to the specified path
                var screenshotData = await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Type = ScreenshotType.Jpeg,
                    Quality = 80
                });
                var base64String = Convert.ToBase64String(screenshotData);

                ExtentTest.Info("Screenshot: ", MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64String).Build());
                ExtentTest.Log(logStatus, "Message: " + TestContext.CurrentContext.Result.Message);
                ExtentTest.Log(logStatus, "Stack trace: " + stackTrace);
            }

            await Page.CloseAsync();
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            await BrowserContext.DisposeAsync();

            await Browser.DisposeAsync();

            ExtentReport.Flush();
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

        public static async Task SetBrowserState()
        {
            PlaywrightContext = await Playwright.CreateAsync();

            Browser = await InitializeBrowser(Browsers.Chromium.ToString());

            BrowserContext = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });

            var page = await BrowserContext.NewPageAsync();

            //Perform the login here
            await page.GotoAsync("https://www.saucedemo.com/");
            await page.Locator("input[id='user-name']").FillAsync("standard_user");
            await page.Locator("input[id='password']").FillAsync("secret_sauce");
            await page.Locator("input[id='login-button']").ClickAsync();

            await BrowserContext.StorageStateAsync(new()
            {
                Path = "state.json"
            });

            await page.CloseAsync();
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