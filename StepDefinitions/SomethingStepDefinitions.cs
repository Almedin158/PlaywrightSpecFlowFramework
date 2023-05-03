using Microsoft.Playwright;
using Newtonsoft.Json;
using NUnit.Framework;
using PSF.Support;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class SomethingStepDefinitions
    {
        IPage _page;
        Utility _utility;

        public SomethingStepDefinitions(Hooks hooks)
        {
            _page = hooks.Page;
            _utility=new Utility();
        }

        private ILocator searchTerm => _page.Locator("adsfadsfasdf");

        [Given(@"the first number isnt (.*)")]
        public async Task GivenTheFirstNumberIsnt(int p0, Table table)
        {
            var obj = _utility.ReadFromJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Json\\DummyJson.json");
            obj.age = 1;
            obj.grades.science = 11;
            _utility.WriteToJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Json\\DummyJson.json", obj);

            await _page.GotoAsync("https://www.saucedemo.com/inventory.html");
        }

        [Then(@"The number is (.*)")]
        public async Task ThenTheNumberIs(int p0)
        {
            Assert.AreEqual(_page.Url, _page.Url);
        }
    }
}