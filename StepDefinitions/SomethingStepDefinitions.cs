using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class SomethingStepDefinitions
    {
        IPage _page;

        public SomethingStepDefinitions(Hooks hooks)
        {
            _page = hooks.Page;
        }

        private ILocator searchTerm => _page.Locator("adsfadsfasdf");

        [Given(@"the first number isnt (.*)")]
        public async Task GivenTheFirstNumberIsnt(int p0, Table table)
        {
            await _page.GotoAsync("https://www.saucedemo.com/inventory.html");
        }

        [Then(@"The number is (.*)")]
        public async Task ThenTheNumberIs(int p0)
        {
            Assert.AreEqual(_page.Url, _page.Url);
        }
    }
}