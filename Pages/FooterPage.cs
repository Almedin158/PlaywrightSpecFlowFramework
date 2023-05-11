using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class FooterPage
    {
        public IPage _page;
        public Utility _utility;
        public FooterPage(Hooks hooks)
        {
            _page = hooks.Page;
            _utility = new Utility();
        }

        ILocator _txtSubsctiontion => _page.GetByRole(AriaRole.Heading, new() { Name = "Subscription" });
        ILocator _inputSubscriptionEmail => _page.GetByPlaceholder("Your email address");
        ILocator _btnSubscriptionSubmit => _page.GetByRole(AriaRole.Button, new() { Name = "" });
        ILocator _txtYouHaveBeenSuccessfullySubscribed => _page.GetByText("You have been successfully subscribed!");
        public async Task AssertSubscriptionVisible()
        {
            Assert.IsTrue(await _txtSubsctiontion.IsVisibleAsync());
        }
        public async Task AssertYouHaveBeenSuccessfullySubscribedVisible()
        {
            Assert.IsTrue(await _txtYouHaveBeenSuccessfullySubscribed.IsVisibleAsync());
        }
        public async Task EnterSubscriptionEmailAndClickSubmit()
        {
            await _inputSubscriptionEmail.FillAsync(_utility.GenerateEmail(_utility.GenerateName(5,10)));
            await _btnSubscriptionSubmit.ClickAsync();
        }
    }
}