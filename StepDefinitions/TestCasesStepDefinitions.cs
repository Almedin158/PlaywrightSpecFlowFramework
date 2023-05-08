using PSF.Pages;
using PSF.Support;
using System;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class TestCasesStepDefinitions
    {
        HeaderPage HeaderPage;
        TestCasesPage TestCasesPage;
        public TestCasesStepDefinitions(Hooks hooks)
        {
            HeaderPage = new HeaderPage(hooks);
            TestCasesPage = new TestCasesPage(hooks);
        }

        [When(@"Click on Test Cases button")]
        public async Task WhenClickOnTestCasesButton()
        {
            await HeaderPage.ClickTestCases();
        }

        [Then(@"Verify user is navigated to test cases page successfully")]
        public async Task ThenVerifyUserIsNavigatedToTestCasesPageSuccessfully()
        {
            await TestCasesPage.AssertUrl();
        }
    }
}
