using PSF.Pages;
using PSF.Support;
using System;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class ContactUsStepDefinitions
    {
        HeaderPage HeaderPage;
        ContactUsPage ContactUsPage;
        HomePage HomePage;
        public ContactUsStepDefinitions(Hooks hooks)
        {
            HeaderPage = new HeaderPage(hooks);
            ContactUsPage = new ContactUsPage(hooks);
            HomePage = new HomePage(hooks);
        }

        [When(@"Click on Contact Us button")]
        public async Task WhenClickOnContactUsButton()
        {
            await HeaderPage.ClickContactUs();
        }

        [Then(@"Verify GET IN TOUCH is visible")]
        public async Task ThenVerifyGETINTOUCHIsVisible()
        {
            await ContactUsPage.AssertGetInTouchVisible();
        }

        [When(@"Enter name, email, subject and message")]
        public async Task WhenEnterNameEmailSubjectAndMessage()
        {
            await ContactUsPage.EnterNameEmailSubjectMessage();
        }

        [When(@"Upload file")]
        public async Task WhenUploadFile()
        {
            await ContactUsPage.ChooseFile();
        }

        [When(@"Click Submit button")]
        public async Task WhenClickSubmitButton()
        {
            await ContactUsPage.ClickSubmit();
        }

        [Then(@"Verify success message Success! Your details have been submitted successfully\. is visible")]
        public async Task ThenVerifySuccessMessageSuccessYourDetailsHaveBeenSubmittedSuccessfully_IsVisible()
        {
            await ContactUsPage.AssertSuccessYourDetailsHaveBeenSubmittedSuccessfullyVisible();
        }

        [When(@"Click Home button")]
        public async Task WhenClickButton()
        {
            await ContactUsPage.ClickHome();
        }

        [Then(@"Verify that user is navigated to home page")]
        public async Task ThenVerifyThatUserIsNavigatedToHomePage()
        {
            await HomePage.AssertUrl();
        }
    }
}
