using PSF.Pages;
using PSF.Support;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class FooterStepDefinitions
    {
        HomePage HomePage;
        FooterPage FooterPage;
        HeaderPage HeaderPage;
        public FooterStepDefinitions(Hooks hooks)
        {
            HomePage = new HomePage(hooks);
            FooterPage = new FooterPage(hooks);
            HeaderPage = new HeaderPage(hooks);
        }

        [When(@"Scroll down to footer")]
        public async Task WhenScrollDownToFooter()
        {
            await HomePage.ScrollTOBottomOfPage();
        }

        [Then(@"Verify text SUBSCRIPTION")]
        public async Task ThenVerifyTextSUBSCRIPTION()
        {
            await FooterPage.AssertSubscriptionVisible();
        }

        [When(@"Enter email address in input and click arrow button")]
        public async Task WhenEnterEmailAddressInInputAndClickArrowButton()
        {
            await FooterPage.EnterSubscriptionEmailAndClickSubmit();
        }

        [Then(@"Verify success message You have been successfully subscribed! is visible")]
        public async Task ThenVerifySuccessMessageYouHaveBeenSuccessfullySubscribedIsVisible()
        {
            await FooterPage.AssertYouHaveBeenSuccessfullySubscribedVisible();  
        }

        [When(@"Click Cart button")]
        public async Task WhenClickCartButton()
        {
            await HeaderPage.ClickCart();
        }

    }
}
