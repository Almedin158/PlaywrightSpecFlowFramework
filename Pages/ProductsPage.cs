using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PSF.Pages
{
    internal class ProductsPage
    {
        public IPage _page;
        public Utility _utility;
        public Hooks hooks;
        public ProductsPage(Hooks hooks)
        {
            _page = hooks.Page;
            _utility = new Utility();
            this.hooks= hooks;
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
        ILocator _txtProductsHeader => _page.Locator("h2[class='title text-center']");
        ILocator _txtProductBrandsHeader => _page.GetByRole(AriaRole.Heading, new() { Name = "Brands" });
        ILocator _lnkProductBrandsPolo => _page.GetByRole(AriaRole.Link, new() { Name = "Polo" });
        ILocator _lnkProductBrandsHM => _page.GetByRole(AriaRole.Link, new() { Name = "H&M" });
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
        public async Task AssertProductsCategoryHeader(string category)
        {
            Assert.AreEqual(category.ToUpper(), _txtProductsHeader.TextContentAsync().Result.ToUpper());
        }
        public async Task AssertProductBrandsVisible()
        {
            Assert.IsTrue(await _txtProductBrandsHeader.IsVisibleAsync());
        }
        public async Task AssertProductBrandsUrl()
        {
            StringAssert.Contains("https://automationexercise.com/brand_products/", _page.Url);
            Assert.IsTrue(await _product.CountAsync() != 0);
        }
        public async Task AssertUrlAndProducts()
        {
            StringAssert.Contains("https://automationexercise.com/brand_products/", _page.Url);
            Assert.IsTrue(await _product.CountAsync()!=0);
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

            hooks.dynamics.Add(obj);

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
        public async Task ClickBrandsPolo()
        {
            await _lnkProductBrandsPolo.ClickAsync();   
        }
        public async Task ClickBrandsHM()
        {
            await _lnkProductBrandsHM.ClickAsync();
        }
        
    }
}
