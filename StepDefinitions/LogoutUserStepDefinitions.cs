using PSF.Pages;
using PSF.Support;
using System;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class LogoutUserStepDefinitions
    {
        HeaderPage HeaderPage;
        LoginPage LoginPage;

        public LogoutUserStepDefinitions(Hooks hooks)
        {
            HeaderPage = new HeaderPage(hooks);
            LoginPage = new LoginPage(hooks);
        }

        [When(@"Click Logout button")]
        public async Task WhenClickLogoutButton()
        {
            await HeaderPage.ClickLogout();
        }

        [Then(@"Verify that user is navigated to login page")]
        public async Task ThenVerifyThatUserIsNavigatedToLoginPage()
        {
            await LoginPage.AssertUrl();
        }
    }
}
