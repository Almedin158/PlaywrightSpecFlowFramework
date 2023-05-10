using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSF.Pages
{
    internal class CheckoutPage
    {
        public IPage _page;
        public Hooks hooks;
        public Utility _utility;

        public CheckoutPage(Hooks hooks)
        {
            _page = hooks.Page;
            this.hooks = hooks;
            _utility= new Utility();
        }

        ILocator _productsOrder => _page.Locator("tbody>tr");
        ILocator _productName => _page.Locator(">td[class='cart_description']>h4>a");
        ILocator _productPrice => _page.Locator(">td[class='cart_price']>p");
        ILocator _inputMessage => _page.Locator("textarea[name=\"message\"]");
        ILocator _lnkPlaceOrder => _page.GetByRole(AriaRole.Link, new() { Name = "Place Order" });
        ILocator _inputNameOnCard => _page.Locator("input[name=\"name_on_card\"]");
        ILocator _inputCardNumber => _page.Locator("input[name=\"card_number\"]");
        ILocator _inputCVC => _page.GetByPlaceholder("ex. 311");
        ILocator _inputExpirationMonth => _page.GetByPlaceholder("MM");
        ILocator _inputExpirationYear => _page.GetByPlaceholder("YYYY");
        ILocator _btnPayAndConfirmOrder => _page.GetByRole(AriaRole.Button, new() { Name = "Pay and Confirm Order" });
        ILocator _txtYourOrderHasBeenPlacedSuccessfully => _page.Locator("#success_message");
        public async Task AssertOrderDetails()
        {
            for (int i = 0; i < hooks.dynamics.Count; i++)
            {
                Assert.AreEqual(hooks.dynamics[i].name, await _productsOrder.Nth(i).Locator(_productName).TextContentAsync());
                Assert.AreEqual(hooks.dynamics[i].price, await _productsOrder.Nth(i).Locator(_productPrice).TextContentAsync());
            }
        }
        public async Task AssertSuccessfulOrderMessage()
        {
            Task.Delay(500);
            Assert.IsTrue(await _txtYourOrderHasBeenPlacedSuccessfully.IsVisibleAsync());
        }
        public async Task EnterMessage()
        {
            await _inputMessage.FillAsync(_utility.GenerateSentence());
        }
        public async Task ClickPlaceOrder()
        {
            await _lnkPlaceOrder.ClickAsync();
        }
        public async Task EnterNameOnCard()
        {
            await _inputNameOnCard.FillAsync("Neko Nesto");
        }
        public async Task EnterCardNumber()
        {
            await _inputCardNumber.FillAsync("1234123412341234");
        }
        public async Task EnterCVC()
        {
            await _inputCVC.FillAsync("311");
        }
        public async Task EnterExpirationMonth()
        {
            await _inputExpirationMonth.FillAsync("08");
        }
        public async Task EnterExpirationYear()
        {
            await _inputExpirationYear.FillAsync("2025");
        }
        public async Task ClickPayAndConfirmOrder()
        {
            await _btnPayAndConfirmOrder.ClickAsync();
        }
    }
}
