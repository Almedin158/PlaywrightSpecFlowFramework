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
    }
}
