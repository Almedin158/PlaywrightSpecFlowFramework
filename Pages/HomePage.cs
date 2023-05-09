using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class HomePage
    {
        private IPage _page;

        public HomePage(Hooks hooks)
        {
            _page = hooks.Page;
        }
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
        public async Task ScrollTOBottomOfPage()
        {
            await _page.Keyboard.DownAsync("End");
        }
    }
}
