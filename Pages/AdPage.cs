using Microsoft.Playwright;
using PSF.Support;

namespace PSF.Pages
{
    internal class AdPage
    {
        private IPage _page;

        public AdPage(Hooks hooks)
        {
            _page = hooks.Page;
        }

        ILocator _frame => _page.Locator("//ins[@aria-hidden='false']//iframe");
        ILocator _btnDismiss => _page.Locator("div[id='dismiss-button']");

        public async Task CloseAd()
        {
            try
            {
                await _page.FrameLocator("//ins[@aria-hidden='false']//iframe").Locator("div[id='dismiss-button']").ClickAsync();
            }
            catch
            {
                try
                {
                    await _page.FrameLocator("//ins[@aria-hidden='false']//iframe").FrameLocator("//iframe").Locator("div[id='dismiss-button']").ClickAsync();
                }
                catch
                {
                    throw new Exception("Was not able to close the ad");
                }
            }
        }
    }
}
