using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class DeleteAccountPage
    {
        public IPage _page;

        public DeleteAccountPage(Hooks hooks)
        {
            _page = hooks.Page;
        }

        ILocator _accountDeleted => _page.GetByText("Account Deleted!");
        ILocator _btnContinue => _page.GetByRole(AriaRole.Link, new() { Name = "Continue" });

        public async Task AssertAccountDeletedVisibleAndContinue()
        {
            Assert.IsTrue(await _accountDeleted.IsVisibleAsync());
            await _btnContinue.ClickAsync();
        }
    }
}
