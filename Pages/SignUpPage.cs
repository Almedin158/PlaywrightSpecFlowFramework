using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class SignUpPage
    {
        private IPage _page;
        private Utility _utility;
        public SignUpPage(Hooks hooks)
        {
            _page = hooks.Page;
            _utility = new Utility();
        }

        ILocator _enterAccountInformation => _page.GetByText("Enter Account Information");
        ILocator _radioBtnTitle => _page.GetByLabel("Mr.");
        ILocator _inputPassword => _page.GetByLabel("Password");
        ILocator _ddDayOfBirth => _page.Locator("#days");
        ILocator _ddMonthOfBirth => _page.Locator("#months");
        ILocator _ddYearOfBirth => _page.Locator("#years");
        ILocator _checkBoxNewsLetter => _page.GetByLabel("Sign up for our newsletter!");
        ILocator _checkBoxSpecialOffer => _page.GetByLabel("Receive special offers from our partners!");
        ILocator _inputFirstName => _page.GetByLabel("First name *");
        ILocator _inputLastName => _page.GetByLabel("Last name *");
        ILocator _inputCompany => _page.GetByLabel("Company", new() { Exact= true});
        ILocator _inputAddress => _page.GetByLabel("Address *");
        ILocator _inputAddress2 => _page.GetByLabel("Address 2");
        ILocator _comboBoxCountry => _page.GetByRole(AriaRole.Combobox, new() { Name = "Country" });
        ILocator _inputState => _page.GetByLabel("State *");
        ILocator _inputCity => _page.GetByLabel("City *");
        ILocator _inputZipCode => _page.Locator("#zipcode");
        ILocator _inputMobileNumber => _page.GetByLabel("Mobile Number *");
        ILocator _btnCreateAccount => _page.GetByRole(AriaRole.Button, new() { Name = "Create Account" });

        public async Task AssertEnterAccountInformationVisible()
        {
            Assert.IsTrue(await _enterAccountInformation.IsVisibleAsync());
        }
        public async Task EnterTitleNameEmailPasswordAndDateOfBirth()
        {
            await _radioBtnTitle.ClickAsync();
            await _inputPassword.FillAsync(_utility.GenerateName(3, 8));
            await _ddDayOfBirth.SelectOptionAsync(new[] { $"{_utility.GenerateNumber(1, 28)}" });
            await _ddMonthOfBirth.SelectOptionAsync(new[] { $"{_utility.GenerateNumber(1, 12)}" });
            await _ddYearOfBirth.SelectOptionAsync(new[] { $"{_utility.GenerateNumber(1900, 2021)}" });
        }
        public async Task SelectNewsLetter()
        {
            await _checkBoxNewsLetter.ClickAsync();
        }
        public async Task SelectSpecialOffer()
        {
            await _checkBoxSpecialOffer.ClickAsync();
        }
        public async Task EnterFirstNameLastNameCompanyAddressAddress2CountryStateCityZipcodeMobileNumber()
        {
            await _inputFirstName.FillAsync(_utility.GenerateName(5, 12));
            await _inputLastName.FillAsync(_utility.GenerateName(5, 12));
            await _inputCompany.FillAsync(_utility.GenerateName(5, 12));
            await _inputAddress.FillAsync(_utility.GenerateName(5, 12));
            await _inputAddress2.FillAsync(_utility.GenerateName(5, 12));
            await _comboBoxCountry.SelectOptionAsync(new[] { "Singapore" });
            await _inputState.FillAsync(_utility.GenerateName(5, 12));
            await _inputCity.FillAsync(_utility.GenerateName(5, 12));
            await _inputZipCode.FillAsync(_utility.GenerateNumber(10000, 99999).ToString());
            await _inputMobileNumber.FillAsync(_utility.GenerateNumber(000000000, 999999999).ToString());
        }
        public async Task ClickCreateAccount()
        {
            await _btnCreateAccount.ClickAsync();
        }
    }
}
