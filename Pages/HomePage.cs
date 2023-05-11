using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class HomePage
    {
        private IPage _page;
        public Hooks hooks;
        public HomePage(Hooks hooks)
        {
            _page = hooks.Page;
            this.hooks = hooks;
        }
        ILocator _txtProductName => _page.Locator("div[class='productinfo text-center']>p");
        ILocator _txtProductPrice => _page.Locator("div[class='overlay-content']>h2");
        ILocator _product => _page.Locator("div[class='productinfo text-center']");
        ILocator _addToCart => _page.Locator("a[class='btn btn-default add-to-cart']");
        ILocator _btnContinueShopping => _page.GetByRole(AriaRole.Button, new() { Name = "Continue Shopping" });
        ILocator _txtCategories => _page.GetByRole(AriaRole.Heading, new() { Name = "Category" });
        ILocator _btnWomenCategory => _page.GetByRole(AriaRole.Link, new() { Name = " Women" });
        ILocator _btnMenCategory => _page.GetByRole(AriaRole.Link, new() { Name = " Men" });
        ILocator _btnWomenCategoryTops => _page.GetByRole(AriaRole.Link, new() { Name = "Tops" });
        ILocator _btnMenCategoriesTShirts => _page.GetByRole(AriaRole.Link, new() { Name = "Tshirts" });

        public async Task AssertUrl()
        {
            Assert.AreEqual("https://automationexercise.com/", _page.Url);
        }
        public async Task GoTo()
        {
            await _page.GotoAsync("https://automationexercise.com/");
        }
        public async Task AssertPageVisible()
        {
            bool loaded = await _page.EvaluateAsync<bool>(@"() => {
                return document.readyState === 'complete';
            }");
            Assert.IsTrue(loaded);
        }
        public async Task AssertCategoriesVisible()
        {
            Assert.IsTrue(await _txtCategories.IsVisibleAsync());
        }
        public async Task ScrollTOBottomOfPage()
        {
            await _page.Keyboard.DownAsync("End");
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
        public async Task AssertProductCategoriesUrl()
        {
            StringAssert.Contains("https://automationexercise.com/category_products/", _page.Url);
        }
        public async Task ClickContinueShopping()
        {
            await _btnContinueShopping.ClickAsync();
        }
        public async Task ClickWomenCategory()
        {
            await _btnWomenCategory.ClickAsync();
        }
        public async Task ClickWomenCategoryTops()
        {
            await _btnWomenCategoryTops.ClickAsync();
        }
        public async Task ClickMenCategory()
        {
            await _btnMenCategory.ClickAsync();
        }
        public async Task ClickMenCategoryTShirts()
        {
            await _btnMenCategoriesTShirts.ClickAsync();
        }
    }
}
