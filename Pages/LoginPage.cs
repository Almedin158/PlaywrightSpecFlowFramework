using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
        ILocator _hdngLoginToYourAccount => _page.GetByRole(AriaRole.Heading, new() { Name = "Login to your account" });
        ILocator _inputName => _page.GetByPlaceholder("Name");
        ILocator _inputSingUpEmail => _page.Locator("form").Filter(new() { HasText = "Signup" }).GetByPlaceholder("Email Address");
        ILocator _inputLoginEmail => _page.Locator("form").Filter(new() { HasText = "Login" }).GetByPlaceholder("Email Address");
        ILocator _inputPassword => _page.GetByPlaceholder("Password");
        ILocator _btnLogin => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        ILocator _btnSignUp => _page.GetByRole(AriaRole.Button, new() { Name = "Signup" });
        ILocator _txtYourEmailOrPasswordIsIncorrect => _page.GetByText("Your email or password is incorrect!");
        ILocator _txtEmailAddressAlreadyExist => _page.GetByText("Email Address already exist!");

        public async Task AssertHeadingNewUserSignupVisible()
        {
            Assert.IsTrue(await _hdngNewUserSignUp.IsVisibleAsync());
        }
        public async Task AssertHeadingLoginToYourAccountVisible()
        {
            Assert.IsTrue(await _hdngLoginToYourAccount.IsVisibleAsync());
        }
        public async Task AssertUrl()
        {
            Assert.AreEqual("https://automationexercise.com/login", _page.Url);
        }
        public async Task AssertEmailAddressAlreadyExistVisible()
        {
            Assert.IsTrue(await _txtEmailAddressAlreadyExist.IsVisibleAsync());
        }
        public async Task AssertYourEmailOrPasswordIsIncorrectVisible()
        {
            await _txtYourEmailOrPasswordIsIncorrect.IsVisibleAsync();
        }
        public async Task EnterNameAndEmail(Table table)
        {
            if(table== null) 
            {
                string Name = _utility.GenerateName(5,10);
                await _inputName.FillAsync(Name);
                string Email = _utility.GenerateEmail(Name);
                await _inputSingUpEmail.FillAsync(Email);
            }
            else
            {
                dynamic data = table.CreateDynamicInstance();
                await _inputName.FillAsync(data.Name);
                await _inputSingUpEmail.FillAsync(data.Email);
            }
        }
        public async Task EnterEmailAndPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            await _inputLoginEmail.FillAsync(data.Username);
            await _inputPassword.FillAsync(data.Password);
        }
        public async Task ClickSignUp()
        {
            await _btnSignUp.ClickAsync();
        }
        public async Task ClickLogin()
        {
            await _btnLogin.ClickAsync();
        }
    }
}
