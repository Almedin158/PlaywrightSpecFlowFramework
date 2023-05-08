using Microsoft.Playwright;
using NUnit.Framework;
using PSF.Support;

namespace PSF.Pages
{
    internal class ContactUsPage
    {
        private IPage _page;
        private Utility _utility;

        public ContactUsPage(Hooks hooks)
        {
            _page = hooks.Page;
            _utility = new Utility();
        }

        ILocator _txtGetInTouch => _page.GetByRole(AriaRole.Heading, new() { Name = "Get In Touch" });
        ILocator _inputName => _page.GetByPlaceholder("Name");
        ILocator _inputEmail => _page.GetByPlaceholder("Email", new() { Exact = true });
        ILocator _inputSubject => _page.GetByPlaceholder("Subject");
        ILocator _inputMessage => _page.GetByPlaceholder("Your Message Here");
        ILocator _uploadChooseFile => _page.Locator("input[name=\"upload_file\"]");
        ILocator _btnSubmit => _page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
        ILocator _txtSuccessYourDetailsHaveBeenSubmittedSuccessfully => _page.Locator("#contact-page").GetByText("Success! Your details have been submitted successfully.");
        ILocator _lnkHome => _page.GetByRole(AriaRole.Link, new() { Name = "Home" });

        public async Task AssertGetInTouchVisible()
        {
            Assert.IsTrue(await _txtGetInTouch.IsVisibleAsync());
        }
        public async Task AssertSuccessYourDetailsHaveBeenSubmittedSuccessfullyVisible()
        {
            Assert.IsTrue(await _txtSuccessYourDetailsHaveBeenSubmittedSuccessfully.IsVisibleAsync());
        }
        public async Task EnterNameEmailSubjectMessage()
        {
            string name = _utility.GenerateName(5,12);
            await _inputName.FillAsync(name);
            await _inputEmail.FillAsync(_utility.GenerateEmail(name));
            await _inputSubject.FillAsync(_utility.GenerateSentence());
            await _inputMessage.FillAsync(_utility.GenerateSentence());
        }
        public async Task ChooseFile()
        {
            var a = Directory.GetCurrentDirectory();
            await _uploadChooseFile.SetInputFilesAsync(new[] { "../../../dummyUpload.txt" });
        }
        public async Task ClickSubmit()
        {
            _page.Dialog += (_, dialog) => dialog.AcceptAsync();
            await _btnSubmit.ClickAsync();
        }
        public async Task ClickHome()
        {
            await _lnkHome.Last.ClickAsync();
        }
    }
}
