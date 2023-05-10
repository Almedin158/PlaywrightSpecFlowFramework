using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TechTalk.SpecFlow;

//Allows parallel executions of test cases inside different feature files (meaning multiple test cases in one feature file can not run in parallel)
[assembly: Parallelizable(ParallelScope.Fixtures)]
//Limits the number of possible parallel executions
[assembly: LevelOfParallelism(2)]

namespace PSF.Support
{
    [Binding]
    public sealed class Hooks
    {
        public List<dynamic> dynamics;

        private readonly ScenarioContext _scenarioContext;
        public IPage Page;
        public static IBrowser Browser;
        public static IBrowserContext BrowserContext;
        public static IPlaywright PlaywrightContext;
        private static ExtentReports ExtentReport;
        private ExtentTest ExtentTest;
        private ExtentTest StepsNode;
        public string CurrentStep;

        public enum Browsers
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
            dynamics= new List<dynamic>();

            ExtentTest = ExtentReport.CreateTest(TestContext.CurrentContext.Test.Name);

            StepsNode = ExtentTest.CreateNode("Steps:");

            PlaywrightContext = await Playwright.CreateAsync();
            //Initialise a browser - 'Chromium' can be changed to 'Firefox' or 'Webkit'
            Browser = await InitializeBrowser(Browsers.Chromium);

            //This will cause an error if SetBrowserState isn't performed and a state.json file does not exist
            BrowserContext = await GetBrowserState();

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

            //var a = _scenarioContext.StepContext.StepInfo.Text;
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            await BrowserContext.DisposeAsync();

            await Browser.DisposeAsync();

            ExtentReport.Flush();
        }

        public static async Task<IBrowser> InitializeBrowser(Browsers browser, bool headless=false)
        {
            switch(browser) {
                case Browsers.Firefox:
                    return await PlaywrightContext.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        Timeout = 5000,
                        SlowMo=500
                    });
                
                case Browsers.WebKit:
                    return await PlaywrightContext.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        Timeout = 5000,
                        SlowMo=500
                    });
            }

            return await PlaywrightContext.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless,
                Args = new[] { "--start-maximized" },
                Timeout = 5000,
                SlowMo = 500
            });
        }

        public static async Task SetBrowserState()
        {
            PlaywrightContext = await Playwright.CreateAsync();

            Browser = await InitializeBrowser(Browsers.Chromium, true);

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
                Path = "../../../state.json"
            });

            await page.CloseAsync();
        }

        public async Task<IBrowserContext> GetBrowserState()
        {
            //State is imported based on tags, at the moment, if a test scenario has a SetState tag, the state will be imported.
            var browserContextOptions = new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
            };

            if (_scenarioContext.ScenarioInfo.Tags.Any(tag => tag.Equals("SetState")))
            {
                browserContextOptions.StorageStatePath = "../../../state.json";
            }

            return await Browser.NewContextAsync(browserContextOptions);
        }
    }
}