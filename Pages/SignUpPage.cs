using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class SignUpPage
    {
        private IPage _page;

        public SignUpPage(Hooks hooks)
        {
            _page = hooks.Page;   
        }

        ILocator _enterAccountInformation => _page.GetByText("Enter Account Information");
        ILocator _radiobtnTitle => _page.GetByLabel("Mr.");
        ILocator _txtPassword => _page.GetByLabel("Password");
        ILocator _ddDayOfBirth => _page.Locator("#days");
        ILocator _ddMonthOfBirth => _page.Locator("#months");
        ILocator _ddYearOfBirth => _page.Locator("#years");
        ILocator _checkboxNewsLetter => _page.GetByLabel("Sign up for our newsletter!");
        ILocator _checkboxSpecialOffer => _page.GetByLabel("Receive special offers from our partners!");
        ILocator _txtFirstName => _page.GetByLabel("First name *");
        ILocator _txtLastName => _page.GetByLabel("Last name *");
        ILocator _txtCompany => _page.GetByLabel("Company", new() { Exact= true});
        ILocator _txtAddress => _page.GetByLabel("Address *");
        ILocator _txtAddress2 => _page.GetByLabel("Address 2");
        ILocator _comboboxCountry => _page.GetByRole(AriaRole.Combobox, new() { Name = "Country" });
        ILocator _txtState => _page.GetByLabel("State *");
        ILocator _txtCity => _page.GetByLabel("City *");
        ILocator _txtZipCode => _page.Locator("#zipcode");
        ILocator _txtMobileNumber => _page.GetByLabel("Mobile Number *");
        ILocator _btnCreateAccount => _page.GetByRole(AriaRole.Button, new() { Name = "Create Account" });

        public async Task AssertEnterAccountInformationVisible()
        {
            Assert.IsTrue(await _enterAccountInformation.IsVisibleAsync());
        }
        public async Task EnterTitleNameEmailPasswordAndDateOfBirth()
        {
            await _radiobtnTitle.ClickAsync();
            await _txtPassword.FillAsync(Utility.GenerateName(3, 8));
            await _ddDayOfBirth.SelectOptionAsync(new[] { $"{Utility.GenerateRandomNumber(1, 28)}" });
            await _ddMonthOfBirth.SelectOptionAsync(new[] { $"{Utility.GenerateRandomNumber(1, 12)}" });
            await _ddYearOfBirth.SelectOptionAsync(new[] { $"{Utility.GenerateRandomNumber(1900, 2021)}" });
        }
        public async Task SelectNewsLetter()
        {
            await _checkboxNewsLetter.ClickAsync();
        }
        public async Task SelectSpecialOffer()
        {
            await _checkboxSpecialOffer.ClickAsync();
        }
        public async Task EnterFirstNameLastNameCompanyAddressAddress2CountryStateCityZipcodeMobileNumber()
        {
            await _txtFirstName.FillAsync(Utility.GenerateName(5, 12));
            await _txtLastName.FillAsync(Utility.GenerateName(5, 12));
            await _txtCompany.FillAsync(Utility.GenerateName(5, 12));
            await _txtAddress.FillAsync(Utility.GenerateName(5, 12));
            await _txtAddress2.FillAsync(Utility.GenerateName(5, 12));
            await _comboboxCountry.SelectOptionAsync(new[] { "Singapore" });
            await _txtState.FillAsync(Utility.GenerateName(5, 12));
            await _txtCity.FillAsync(Utility.GenerateName(5, 12));
            await _txtZipCode.FillAsync(Utility.GenerateRandomNumber(10000, 99999).ToString());
            await _txtMobileNumber.FillAsync(Utility.GenerateRandomNumber(000000000, 999999999).ToString());
        }
        public async Task ClickCreateAccount()
        {
            await _btnCreateAccount.ClickAsync();
        }
    }
}
