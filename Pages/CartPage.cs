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
    internal class CartPage
    {
        public Hooks hook;
        public IPage _page;
        
        public CartPage(Hooks hooks)
        {
            hook = hooks;
            _page = hooks.Page;
        }

        ILocator _cartProduct => _page.Locator("tbody>tr");
        ILocator _cartProductName => _page.Locator(">td[class='cart_description']>h4>a");
        ILocator _cartPrice => _page.Locator(">td[class='cart_price']>p");
        ILocator _cartQuantity => _page.Locator(">td[class='cart_quantity']>button");
        ILocator _cartTotalPrice => _page.Locator(">td[class='cart_total']>p");
        public async Task AssertProducts()
        {
            for(int i =0; i < hook.dynamics.Count; i++)
            {
                Assert.AreEqual(hook.dynamics[i].name, await _cartProduct.Nth(i).Locator(_cartProductName).TextContentAsync());
            }
        }
        public async Task AssertProductsDetails()
        {
            for (int i = 0; i < hook.dynamics.Count; i++)
            {
                string price = _cartProduct.Nth(i).Locator(_cartPrice).TextContentAsync().Result.Substring(4);
                string quantity = await _cartProduct.Nth(i).Locator(_cartQuantity).TextContentAsync();
                Assert.AreEqual(hook.dynamics[i].price, await _cartProduct.Nth(i).Locator(_cartPrice).TextContentAsync());
                Assert.AreEqual(await _cartProduct.Nth(i).Locator(_cartQuantity).TextContentAsync(), "1");
                StringAssert.Contains((int.Parse(price) * int.Parse(quantity)).ToString(), await _cartProduct.Nth(i).Locator(_cartTotalPrice).TextContentAsync());
            }
        }
        public async Task AssertProductQuantity(int number)
        {
            var a = await _cartProduct.Locator(_cartQuantity).TextContentAsync();
            Assert.AreEqual(await _cartProduct.Locator(_cartQuantity).TextContentAsync(), number.ToString());
        }
    }
}