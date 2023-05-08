using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class TestCasesPage
    {
        private IPage _page;

        public TestCasesPage(Hooks hooks)
        {
            _page = hooks.Page;
        }

        public async Task AssertUrl()
        {
            Assert.AreEqual("https://automationexercise.com/test_cases", _page.Url);
        }
    }
}
