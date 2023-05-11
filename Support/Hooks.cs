using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TechTalk.SpecFlow;

//Allows parallel executions of test cases inside different feature files (meaning multiple test cases in one feature file can not run in parallel)
[assembly: Parallelizable(ParallelScope.Fixtures)]
//Limits the number of possible parallel executions
[assembly: LevelOfParallelism(8)]

namespace PSF.Support
{
    [Binding]
    public sealed class Hooks
    {
        public List<dynamic> dynamics;

        private static dynamic _config;
        private static Utility _utility;
        private readonly ScenarioContext _scenarioContext;
        public IPage Page;
        public static IBrowser Browser;
        public static IBrowserContext BrowserContext;
        public static IPlaywright PlaywrightContext;
        private static ExtentReports ExtentReport;
        private ExtentTest ExtentTest;
        private ExtentTest StepsNode;
        public string CurrentStep;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            _utility = new Utility();
            _config = _utility.ReadFromJsonFile("../../../configuration.json");

            ExtentReport = new ExtentReports();
            ExtentReport.AttachReporter(new ExtentHtmlReporter(Directory.GetCurrentDirectory()));

            //set ibs (import browser state) in configuration.json to true if you wish to import browser state, else set it to false.
            if ((bool)_config.ibs)
            {
                PlaywrightContext = await Playwright.CreateAsync();

                Browser = await InitializeBrowser((string)_config.browser, (bool)_config.btrheadless);

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

                //this creates the browser state file
                await BrowserContext.StorageStateAsync(new()
                {
                    Path = "../../../state.json"
                });

                await page.CloseAsync();
            }
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            dynamics = new List<dynamic>();

            ExtentTest = ExtentReport.CreateTest(TestContext.CurrentContext.Test.Name);

            StepsNode = ExtentTest.CreateNode("Steps:");

            PlaywrightContext = await Playwright.CreateAsync();

            Browser = await InitializeBrowser((string)_config.browser, (bool)_config.headless);

            var browserContextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
            };

            //this will import the browser state only if at least one scenario is tagged with "SetState" and ibs in the configuration.json is set to true
            if (_scenarioContext.ScenarioInfo.Tags.Any(tag => tag.Equals("SetState") && (bool)_config.ibs))
            {
                browserContextOptions.StorageStatePath = "../../../state.json";
            }

            BrowserContext = await Browser.NewContextAsync(browserContextOptions);

            Page = await BrowserContext.NewPageAsync();
        }

        [BeforeStep]
        public void BeforeStep()
        {
            CurrentStep = _scenarioContext.StepContext.StepInfo.Text;
        }

        [AfterStep]
        public void AfterStep() 
        {
            if (_scenarioContext.TestError == null)
            {
                StepsNode.Log(Status.Pass, CurrentStep);
            }
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                   ? ""
                   : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
                // Take a screenshot of the browser and save it to the specified path
                var screenshotData = await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Type = ScreenshotType.Jpeg,
                    Quality = 80,                    FullPage = true

                });
                var base64String = Convert.ToBase64String(screenshotData);

                ExtentTest.Info("Screenshot: ", MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64String).Build());

                
                StepsNode.Log(Status.Fail, CurrentStep);
                var errorNode = ExtentTest.CreateNode("Info:");
                errorNode.Info("Message: " + TestContext.CurrentContext.Result.Message);
                errorNode.Info("Stack trace: " + stackTrace);
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

        public static async Task<IBrowser> InitializeBrowser(string browser, bool headless = true)
        {
            switch (browser)
            {
                case "Firefox":
                    return await PlaywrightContext.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        //Timeout = 5000,
                        SlowMo = 500
                    });

                case "WebKit":
                    return await PlaywrightContext.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        //Timeout = 5000,
                        SlowMo = 500
                    });
            }

            return await PlaywrightContext.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless,
                Args = new[] { "--start-maximized" },
                //Timeout = 5000,
                SlowMo = 500
            });
        }
    }
}