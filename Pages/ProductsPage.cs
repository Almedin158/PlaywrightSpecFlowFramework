using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PSF.Pages
{
    internal class ProductsPage
    {
        public IPage _page;
        public Utility _utility;
        public List<dynamic> _products;
        public ProductsPage(Hooks hooks)
        {
            _page = hooks.Page;
            _utility = new Utility();
            _products = hooks.dynamics;
        }

        ILocator _productsList => _page.Locator("div[class='features_items']");
        ILocator _lnkViewProduct => _page.Locator(".choose > .nav > li > a");
        ILocator _inputSearchProduct => _page.GetByPlaceholder("Search Product");
        ILocator _txtSearchedProducts => _page.GetByRole(AriaRole.Heading, new() { Name = "Searched Products" });
        ILocator _txtProductName => _page.Locator("div[class='productinfo text-center']>p");
        ILocator _btnSearch => _page.GetByRole(AriaRole.Button, new() { Name = "" });
        ILocator _productImage => _page.Locator("div[class='single-products']");
        ILocator _product => _page.Locator("div[class='productinfo text-center']");
        ILocator _addToCart => _page.Locator("a[class='btn btn-default add-to-cart']");
        ILocator _btnContinueShopping => _page.GetByRole(AriaRole.Button, new() { Name = "Continue Shopping" });
        ILocator _btnViewCart => _page.GetByRole(AriaRole.Link, new() { Name = "View Cart" });
        ILocator _txtProductPrice => _page.Locator("div[class='overlay-content']>h2");
        public async Task AssertUrl()
        {
            Assert.AreEqual("https://automationexercise.com/products", _page.Url);
        }
        public async Task AssertProductsListVisible()
        {
            Assert.IsTrue(await _productsList.IsVisibleAsync());
        }
        public async Task AssertSearchedProductsVisible()
        {
            Assert.IsTrue(await _txtSearchedProducts.IsVisibleAsync());
        }
        public async Task AssertProductsRelatedToSearch(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            string name = data.Name;

            for (int i=0;i< await _txtProductName.CountAsync();i++)
            {
                string productName = await _txtProductName.Nth(i).TextContentAsync();


                StringAssert.Contains(name.ToLower(), productName.ToLower());
            }
        }
        public async Task ClickViewProduct(int number)
        {
            await _lnkViewProduct.Nth(number).ClickAsync();
        }
        public async Task EnterSearchProduct(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            await _inputSearchProduct.FillAsync(data.Name);
            await _btnSearch.ClickAsync();
        }
        public async Task HoverOverProductAddToCart(int number)
        {
            dynamic obj =
                new
                {
                    name = await _txtProductName.Nth(number).TextContentAsync(),
                    price = await _txtProductPrice.Nth(number).TextContentAsync()
                };

            _products.Add(obj);

            await _product.Nth(number).Locator(_addToCart).ClickAsync();
        }
        public async Task ClickContinueShopping()
        {
            await _btnContinueShopping.ClickAsync();
        }
        public async Task ClickViewCart()
        {
            await _btnViewCart.ClickAsync();
        }
    }
}
