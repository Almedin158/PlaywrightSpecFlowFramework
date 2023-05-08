using PSF.Pages;
using PSF.Support;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PSF.StepDefinitions
{
    [Binding]
    public class UserLoginStepDefinitions
    {
        LoginPage LoginPage;
        public UserLoginStepDefinitions(Hooks hooks)
        {
            LoginPage = new LoginPage(hooks);
        }

        [Then(@"Verify Login to your account is visible")]
        public async Task ThenVerifyLoginToYourAccountIsVisible()
        {
            await LoginPage.AssertHeadingLoginToYourAccountVisible();
        }

        [When(@"Enter correct email address and password")]
        public async Task WhenEnterCorrectEmailAddressAndPassword(Table table)
        {
            await LoginPage.EnterEmailAndPassword(table);
        }

        [When(@"Click login button")]
        public async Task WhenClickLoginButton()
        {
            await LoginPage.ClickLogin();
        }

        [When(@"Enter incorrect email address and password")]
        public async Task WhenEnterIncorrectEmailAddressAndPassword(Table table)
        {
            await LoginPage.EnterEmailAndPassword(table);
        }

        [Then(@"Verify error Your email or password is incorrect! is visible")]
        public async Task ThenVerifyErrorYourEmailOrPasswordIsIncorrectIsVisible()
        {
            await LoginPage.AssertYourEmailOrPasswordIsIncorrectVisible();
        }

    }
}
