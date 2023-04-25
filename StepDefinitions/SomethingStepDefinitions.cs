using Microsoft.Playwright;
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
            await searchTerm.ClickAsync();
        }
    }
}