using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PSF.Pages
{
    internal class ProductsPage
    {
        public IPage _page;
        public ProductsPage(Hooks hooks)
        {
            _page = hooks.Page;
        }

        ILocator _productsList => _page.Locator("div[class='features_items']");
        ILocator _lnkViewProduct => _page.Locator(".choose > .nav > li > a");
        ILocator _inputSearchProduct => _page.GetByPlaceholder("Search Product");
        ILocator _txtSearchedProducts => _page.GetByRole(AriaRole.Heading, new() { Name = "Searched Products" });
        ILocator _txtProductName => _page.Locator("div[class='productinfo text-center']>p");
        ILocator _btnSearch => _page.GetByRole(AriaRole.Button, new() { Name = "" });
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
        public async Task ClickViewProductFirstProduct()
        {
            await _lnkViewProduct.First.ClickAsync();
        }
        public async Task EnterSearchProduct(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            await _inputSearchProduct.FillAsync(data.Name);
            await _btnSearch.ClickAsync();
        }
    }
}
