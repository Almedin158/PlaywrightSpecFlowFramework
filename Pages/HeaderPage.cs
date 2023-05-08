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
        ILocator _lnkLogout => _page.GetByRole(AriaRole.Link, new() { Name = "Logout" });
        ILocator _lnkContactUs => _page.GetByRole(AriaRole.Link, new() { Name = "Contact us" });
        ILocator _lnkTestCases => _page.GetByRole(AriaRole.Link, new() { Name = "Test Cases" });
        ILocator _lnkProducts => _page.GetByRole(AriaRole.Link, new() { Name = " Products" });

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
        public async Task ClickLogout()
        {
            await _lnkLogout.ClickAsync();  
        }
        public async Task ClickContactUs()
        {
            await _lnkContactUs.ClickAsync();
        }
        public async Task ClickTestCases()
        {
            await _lnkTestCases.First.ClickAsync();
        }
        public async Task ClickProducts()
        {
            await _lnkProducts.ClickAsync();
        }
    }
}
