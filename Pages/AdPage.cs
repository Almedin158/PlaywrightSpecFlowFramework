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

        public async Task CloseAd()
        {
            _page.SetDefaultTimeout(5000);
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
                    //I commented this out so it doesn't exit the test in case the ad doesn't appear (sometimes the time the ad appears changes, so sometimes my "Close ad" steps aren't corrent, and in those cases I do not wish to exit the test by throwing an exception).
                    //throw new Exception("Was not able to close the ad");
                }
            }
            _page.SetDefaultTimeout(30000);
        }
    }
}
