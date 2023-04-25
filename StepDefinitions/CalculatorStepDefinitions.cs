using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        public IPage _page;

        public CalculatorStepDefinitions(Hooks hooks)
        {
            _page = hooks.Page;
        }

        private ILocator searchTerm => _page.Locator("adsfadsfasdf");

        [Given(@"the first number is (.*)")]
        public async Task GivenTheFirstNumberIs(int p0, Table table)
        {
            await _page.GotoAsync("https://www.saucedemo.com/inventory.html");
            Assert.Multiple(() =>
            {
                Assert.AreEqual("asdfasdfsadf", _page.Url);
                Assert.IsTrue("adfasdf".Length == 0);
            });
        }
    }
}