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
        AdPage AdPage;
        CartPage CartPage;
        CheckoutPage CheckoutPage;
        public RegisterUserStepDefinitions(Hooks hooks)
        {
            HomePage = new HomePage(hooks);
            LoginPage = new LoginPage(hooks);
            SignUpPage = new SignUpPage(hooks);
            AccountCreatedPage = new AccountCreatedPage(hooks);
            DeleteAccountPage = new DeleteAccountPage(hooks);
            HeaderPage = new HeaderPage(hooks);
            AdPage = new AdPage(hooks);
            CartPage =new CartPage(hooks);
            CheckoutPage = new CheckoutPage(hooks); 
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
            await LoginPage.EnterNameAndEmail(null);
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

        [When(@"Close ad")]
        public async Task WhenCloseAd1()
        {
            await AdPage.CloseAd();
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

        [When(@"Enter name and already registered email address")]
        public async Task WhenEnterNameAndAlreadyRegisteredEmailAddress(Table table)
        {
            await LoginPage.EnterNameAndEmail(table);
        }

        [Then(@"Verify error Email Address already exist! is visible")]
        public async Task ThenVerifyErrorEmailAddressAlreadyExistIsVisible()
        {
            await LoginPage.AssertEmailAddressAlreadyExistVisible();
        }

        [When(@"Add products to cart")]
        public async Task WhenAddProductsToCart()
        {
            await HomePage.HoverOverProductAddToCart(1);
            await HomePage.ClickContinueShopping();
        }

        [Then(@"Verify that cart page is displayed")]
        public async Task ThenVerifyThatCartPageIsDisplayed()
        {
            await CartPage.AssertUrl();
        }

        [When(@"Click Proceed To Checkout")]
        public async Task WhenClickProceedToCheckout()
        {
            await CartPage.ClickProceedToCheckout();
        }

        [When(@"Click Register / Login button")]
        public async Task WhenClickRegisterLoginButton()
        {
            await CartPage.ClickRegisterLogin();
        }

        [When(@"Fill all details in Signup and create account")]
        public async Task WhenFillAllDetailsInSignupAndCreateAccount()
        {
            await LoginPage.EnterNameAndEmail(null);
            await LoginPage.ClickSignUp();
            await SignUpPage.EnterTitleNameEmailPasswordAndDateOfBirth();
            await SignUpPage.SelectNewsLetter();
            await SignUpPage.SelectSpecialOffer();
            await SignUpPage.EnterFirstNameLastNameCompanyAddressAddress2CountryStateCityZipcodeMobileNumber();
            await SignUpPage.ClickCreateAccount();
        }

        [Then(@"Verify ACCOUNT CREATED! and click Continue button")]
        public async Task ThenVerifyACCOUNTCREATEDAndClickContinueButton()
        {
            await AccountCreatedPage.AssertAccountCreatedVisible();
            await AccountCreatedPage.ClickContinue();
        }

        [When(@"Click Proceed To Checkout button")]
        public async Task WhenClickProceedToCheckoutButton()
        {
            await CartPage.ClickProceedToCheckout();
        }

        [Then(@"Verify Address Details and Review Your Order")]
        public async Task ThenVerifyAddressDetailsAndReviewYourOrder()
        {
            await CheckoutPage.AssertOrderDetails();
        }

        [When(@"Enter description in comment text area and click Place Order")]
        public async Task WhenEnterDescriptionInCommentTextAreaAndClickPlaceOrder()
        {
            await CheckoutPage.EnterMessage();
            await CheckoutPage.ClickPlaceOrder();
        }

        [When(@"Enter payment details: Name on Card, Card Number, CVC, Expiration date")]
        public async Task WhenEnterPaymentDetailsNameOnCardCardNumberCVCExpirationDate()
        {
            await CheckoutPage.EnterNameOnCard();
            await CheckoutPage.EnterCardNumber();
            await CheckoutPage.EnterCVC();
            await CheckoutPage.EnterExpirationMonth();
            await CheckoutPage.EnterExpirationYear();
        }

        [When(@"Click Pay and Confirm Order button")]
        public async Task WhenClickPayAndConfirmOrderButton()
        {
            await CheckoutPage.ClickPayAndConfirmOrder();
        }

        [Then(@"Verify success message Your order has been placed successfully!")]
        public async Task ThenVerifySuccessMessageYourOrderHasBeenPlacedSuccessfully()
        {
            //await CheckoutPage.AssertSuccessfulOrderMessage();
        }

        [Then(@"Verify ACCOUNT DELETED! and click Continue button")]
        public async Task ThenVerifyACCOUNTDELETEDAndClickContinueButton()
        {
            await DeleteAccountPage.AssertAccountDeletedVisibleAndContinue();
        }

    }
}
