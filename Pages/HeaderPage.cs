using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class HeaderPage
    {
        IPage _page;

        public HeaderPage(Hooks hooks)
        {
            _page=hooks.Page;
        }

        ILocator _lnkSignUpLogin => _page.GetByRole(AriaRole.Link, new() { Name = "Signup / Login" });
        ILocator _lnkLoggedIn => _page.GetByText("Logged in as");
        ILocator _lnkDeleteAccount => _page.GetByRole(AriaRole.Link, new() { Name = "Delete Account" });

        public async Task ClickSignUpLogin()
        {
            await _lnkSignUpLogin.ClickAsync();
        }
        public async Task AssertLoggedInAsVisible()
        {
            Assert.IsTrue(await _lnkLoggedIn.IsVisibleAsync());
        }
        public async Task ClickDeleteAccount()
        {
            await _lnkDeleteAccount.ClickAsync();
        }
    }
}
