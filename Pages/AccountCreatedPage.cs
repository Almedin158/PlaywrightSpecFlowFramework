using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class AccountCreatedPage
    {
        private IPage _page;

        public AccountCreatedPage(Hooks hooks)
        {
            _page = hooks.Page;
        }

        ILocator _btnContinue => _page.GetByRole(AriaRole.Link, new() { Name = "Continue" });
        ILocator _txtAccountCreated => _page.GetByText("Account Created!");
        
        public async Task AssertAccountCreatedVisible()
        {
            Assert.IsTrue(await _txtAccountCreated.IsVisibleAsync());
        }
        public async Task ClickContinue()
        {
            await _btnContinue.ClickAsync();
        }
    }
}