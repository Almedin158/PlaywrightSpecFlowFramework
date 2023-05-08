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

        ILocator _btnClose => _page.FrameLocator("iframe[id='aswift_1']").Locator("div[id='dismiss-button']");
        ILocator _btnClose2 => _page.FrameLocator("iframe[id='ad_iframe']").Locator("div[id='dismiss-button']");

        public async Task CloseAd()
        {
            _page.SetDefaultTimeout(3000);
            try
            { 
                await _btnClose.ClickAsync();
            }
            catch 
            { 
                try
                {
                    await _btnClose2.ClickAsync();
                }
                catch 
                {

                }
            }
            _page.SetDefaultTimeout(30000);
        }
    }
}
