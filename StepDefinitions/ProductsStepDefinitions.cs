using PSF.Pages;
using PSF.Support;
using System;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class ProductsStepDefinitions
    {
        HeaderPage HeaderPage;
        ProductsPage ProductsPage;
        ProductDetailsPage ProductDetailsPage;

        public ProductsStepDefinitions(Hooks hooks)
        {
            HeaderPage = new HeaderPage(hooks);
            ProductsPage = new ProductsPage(hooks);
            ProductDetailsPage = new ProductDetailsPage(hooks);
        }

        [When(@"Click on Products button")]
        public async Task WhenClickOnProductsButton()
        {
            await HeaderPage.ClickProducts();
        }

        [Then(@"Verify user is navigated to ALL PRODUCTS page successfully")]
        public async Task ThenVerifyUserIsNavigatedToALLPRODUCTSPageSuccessfully()
        {
            await ProductsPage.AssertUrl();
        }

        [Then(@"The products list is visible")]
        public async Task ThenTheProductsListIsVisible()
        {
            await ProductsPage.AssertProductsListVisible();
        }

        [When(@"Click on View Product of first product")]
        public async Task WhenClickOnViewProductOfFirstProduct()
        {
            await ProductsPage.ClickViewProductFirstProduct();
        }

        [Then(@"User is landed to product detail page")]
        public async Task ThenUserIsLandedToProductDetailPage()
        {
            await ProductDetailsPage.AssertUrl();
        }

        [Then(@"Verify that detail is visible: product name, category, price, availability, condition, brand")]
        public async Task ThenVerifyThatDetailIsVisibleProductNameCategoryPriceAvailabilityConditionBrand()
        {
            await ProductDetailsPage.AssertProductNameCategoryPriceAvailabilityConditionBrandVisible();
        }

        [When(@"Enter product name in search input and click search button")]
        public async Task WhenEnterProductNameInSearchInputAndClickSearchButton(Table table)
        {
            await ProductsPage.EnterSearchProduct(table);
        }

        [Then(@"Verify SEARCHED PRODUCTS is visible")]
        public async Task ThenVerifySEARCHEDPRODUCTSIsVisible()
        {
            await ProductsPage.AssertSearchedProductsVisible();
        }

        [Then(@"Verify all the products related to search are visible")]
        public async Task ThenVerifyAllTheProductsRelatedToSearchAreVisible(Table table)
        {
            await ProductsPage.AssertProductsRelatedToSearch(table);
        }
    }
}
