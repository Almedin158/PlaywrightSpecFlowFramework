using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class LoginPage
    {
        private IPage _page;
        private Utility _utility;

        public LoginPage(Hooks hooks)
        {
            _page = hooks.Page;
            _utility = new Utility();
        }

        ILocator _hdngNewUserSignUp => _page.GetByRole(AriaRole.Heading, new() { Name = "New User Signup!" });
        ILocator _txtName => _page.GetByPlaceholder("Name");
        ILocator _txtSingUpEmail => _page.Locator("form").Filter(new() { HasText = "Signup" }).GetByPlaceholder("Email Address");
        ILocator _btnSignUp => _page.GetByRole(AriaRole.Button, new() { Name = "Signup" });

        public async Task AssertHeadingNewUserSignupVisible()
        {
            Assert.IsTrue(await _hdngNewUserSignUp.IsVisibleAsync());
        }
        public async Task EnterNameAndEmail()
        {
            string Name = _utility.GenerateName(5,10);
            await _txtName.FillAsync(Name);
            string Email = _utility.GenerateEmail(Name);
            await _txtSingUpEmail.FillAsync(Email);
        }
        public async Task ClickSignUp()
        {
            await _btnSignUp.ClickAsync();
        }
    }
}
