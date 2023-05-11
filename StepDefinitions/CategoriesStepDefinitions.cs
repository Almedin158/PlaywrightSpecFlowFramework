using PSF.Pages;
using PSF.Support;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class CategoriesStepDefinitions
    {
        HomePage HomePage;
        ProductsPage ProductsPage;
        public CategoriesStepDefinitions(Hooks hooks)
        {
            HomePage = new HomePage(hooks);
            ProductsPage = new ProductsPage(hooks); 
        }

        [Then(@"Verify that categories are visible on left side bar")]
        public async Task ThenVerifyThatCategoriesAreVisibleOnLeftSideBar()
        {
            await HomePage.AssertCategoriesVisible();
        }

        [When(@"Click on Women category")]
        public async Task WhenClickOnWomenCategory()
        {
            await HomePage.ClickWomenCategory();
        }

        [When(@"Click on any category link under Women category, for example: Dress")]
        public async Task WhenClickOnAnyCategoryLinkUnderWomenCategoryForExampleDress()
        {
            await HomePage.ClickWomenCategoryTops();
        }

        [Then(@"Verify that category page is displayed and confirm text ""([^""]*)""")]
        public async Task ThenVerifyThatCategoryPageIsDisplayedAndConfirmText(string p0)
        {
            await ProductsPage.AssertProductsCategoryHeader(p0);
        }


        [When(@"On left side bar, click on any sub-category link of Men category")]
        public async Task WhenOnLeftSideBarClickOnAnySub_CategoryLinkOfMenCategory()
        {
            await HomePage.ClickMenCategory();
        }

        [Then(@"Verify that user is navigated to that category page")]
        public async Task ThenVerifyThatUserIsNavigatedToThatCategoryPage()
        {
            await HomePage.AssertProductCategoriesUrl();
        }
        [Then(@"Verify that Brands are visible on left side bar")]
        public async Task ThenVerifyThatBrandsAreVisibleOnLeftSideBar()
        {
            await ProductsPage.AssertProductBrandsVisible();
        }

        [When(@"Click on any brand name")]
        public async Task WhenClickOnAnyBrandName()
        {
            await ProductsPage.ClickBrandsPolo();
        }

        [Then(@"Verify that user is navigated to brand page and brand products are displayed")]
        public async Task ThenVerifyThatUserIsNavigatedToBrandPageAndBrandProductsAreDisplayed()
        {
            await ProductsPage.AssertProductBrandsUrl();
        }

        [When(@"On left side bar, click on any other brand link")]
        public async Task WhenOnLeftSideBarClickOnAnyOtherBrandLink()
        {
            await ProductsPage.ClickBrandsHM();
        }

        [Then(@"Verify that user is navigated to that brand page and can see products")]
        public async Task ThenVerifyThatUserIsNavigatedToThatBrandPageAndCanSeeProducts()
        {
            await ProductsPage.AssertUrlAndProducts();
        }

    }
}
