using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PSF.Pages
{
    internal class ProductDetailsPage
    {
        public IPage _page;
        public ProductDetailsPage(Hooks hooks)
        {
            _page = hooks.Page;
        }

        ILocator _txtProductName => _page.Locator("div[class='product-information']>h2");
        ILocator _txtCategory => _page.GetByText("Category:");
        ILocator _txtPrice => _page.GetByText("Rs.");
        ILocator _txtAvailability => _page.GetByText("Availability:");
        ILocator _txtCondition => _page.GetByText("Condition:");
        ILocator _txtBrand => _page.GetByText("Brand:");
        ILocator _inputQuantity => _page.Locator("#quantity");
        ILocator _btnAddToCart => _page.GetByRole(AriaRole.Button, new() { Name = " Add to cart" });

        public async Task AssertUrl()
        {
            StringAssert.Contains("https://automationexercise.com/product_details/", _page.Url);
        }
        public async Task AssertProductNameCategoryPriceAvailabilityConditionBrandVisible()
        {
            Assert.IsTrue(await _txtProductName.IsVisibleAsync());
            Assert.IsTrue(await _txtCategory.IsVisibleAsync());
            Assert.IsTrue(await _txtPrice.IsVisibleAsync());
            Assert.IsTrue(await _txtAvailability.IsVisibleAsync());
            Assert.IsTrue(await _txtCondition.IsVisibleAsync());
            Assert.IsTrue(await _txtBrand.IsVisibleAsync());
        }
        public async Task ChangeQuantity(int number)
        {
            await _inputQuantity.ClearAsync();
            await _inputQuantity.FillAsync(number.ToString());
        }
        public async Task ClickAddToCart()
        {
            await _btnAddToCart.ClickAsync();
        }
    }
}
