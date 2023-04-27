using PSF.Pages;
using PSF.Support;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class RegisterUserStepDefinitions
    {
        HomePage HomePage;
        LoginPage LoginPage;
        SignUpPage SignUpPage;
        AccountCreatedPage AccountCreatedPage;
        DeleteAccountPage DeleteAccountPage;
        HeaderPage HeaderPage;
        public RegisterUserStepDefinitions(Hooks hooks)
        {
            HomePage = new HomePage(hooks);
            LoginPage = new LoginPage(hooks);
            SignUpPage = new SignUpPage(hooks);
            AccountCreatedPage = new AccountCreatedPage(hooks);
            DeleteAccountPage = new DeleteAccountPage(hooks);
            HeaderPage = new HeaderPage(hooks);
        }

        [When(@"Navigate to url http://automationexercise\.com")]
        public async Task WhenNavigateToUrlHttpAutomationexercise_Com()
        {
            await HomePage.GoTo();
        }

        [Then(@"Verify that home page is visible successfully")]
        public async Task ThenVerifyThatHomePageIsVisibleSuccessfully()
        {
            await HomePage.AssertPageVisible();
        }

        [When(@"Click on Signup / Login button")]
        public async Task WhenClickOnSignupLoginButton()
        {
            await HeaderPage.ClickSignUpLogin();
        }

        [Then(@"Verify New User Signup! is visible")]
        public async Task ThenVerifyNewUserSignupIsVisible()
        {
            await LoginPage.AssertHeadingNewUserSignupVisible();
        }

        [When(@"Enter name and email address")]
        public async Task WhenEnterNameAndEmailAddress()
        {
            await LoginPage.EnterNameAndEmail();
        }

        [When(@"Click Signup button")]
        public async Task WhenClickSignupButton()
        {
            await LoginPage.ClickSignUp();
        }

        [Then(@"Verify that ENTER ACCOUNT INFORMATION is visible")]
        public async Task ThenVerifyThatENTERACCOUNTINFORMATIONIsVisible()
        {
            await SignUpPage.AssertEnterAccountInformationVisible();
        }

        [When(@"Fill details: Title, Name, Email, Password, Date of birth")]
        public async Task WhenFillDetailsTitleNameEmailPasswordDateOfBirth()
        {
            await SignUpPage.EnterTitleNameEmailPasswordAndDateOfBirth();
        }

        [When(@"Select checkbox Sign up for our newsletter!")]
        public async Task WhenSelectCheckboxSignUpForOurNewsletter()
        {
            await SignUpPage.SelectNewsLetter();
        }

        [When(@"Select checkbox Receive special offers from our partners!")]
        public async Task WhenSelectCheckboxReceiveSpecialOffersFromOurPartners()
        {
            await SignUpPage.SelectSpecialOffer();
        }

        [When(@"Fill details: First name, Last name, Company, Address, Address(.*), Country, State, City, Zipcode, Mobile Number")]
        public async Task WhenFillDetailsFirstNameLastNameCompanyAddressAddressCountryStateCityZipcodeMobileNumber(int p0)
        {
            await SignUpPage.EnterFirstNameLastNameCompanyAddressAddress2CountryStateCityZipcodeMobileNumber();
        }

        [When(@"Click Create Account button")]
        public async Task WhenClickCreateAccountButton()
        {
            await SignUpPage.ClickCreateAccount();
        }

        [Then(@"Verify that ACCOUNT CREATED! is visible")]
        public async Task ThenVerifyThatACCOUNTCREATEDIsVisible()
        {
            await AccountCreatedPage.AssertAccountCreatedVisible();
        }

        [When(@"Click Continue button")]
        public async Task WhenClickContinueButton()
        {
            await AccountCreatedPage.ClickContinue();
        }

        [Then(@"Verify that Logged in as username is visible")]
        public async Task ThenVerifyThatLoggedInAsUsernameIsVisible()
        {
            await HeaderPage.AssertLoggedInAsVisible();
        }

        [When(@"Click Delete Account button")]
        public async Task WhenClickDeleteAccountButton()
        {
            await HeaderPage.ClickDeleteAccount();
        }

        [Then(@"Verify that ACCOUNT DELETED! is visible and click Continue button")]
        public async Task ThenVerifyThatACCOUNTDELETEDIsVisibleAndClickContinueButton()
        {
            await DeleteAccountPage.AssertAccountDeletedVisibleAndContinue();
        }
    }
}
