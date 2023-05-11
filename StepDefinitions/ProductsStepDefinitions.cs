using PSF.Pages;
using PSF.Support;
using TechTalk.SpecFlow;

namespace PSF.StepDefinitions
{
    [Binding]
    public class ProductsStepDefinitions
    {
        HeaderPage HeaderPage;
        ProductsPage ProductsPage;
        ProductDetailsPage ProductDetailsPage;
        CartPage CartPage;
        public ProductsStepDefinitions(Hooks hooks)
        {
            HeaderPage = new HeaderPage(hooks);
            ProductsPage = new ProductsPage(hooks);
            ProductDetailsPage = new ProductDetailsPage(hooks);
            CartPage = new CartPage(hooks);
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
            await ProductsPage.ClickViewProduct(1);
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

        [When(@"Hover over first product and click Add to cart")]
        public async Task WhenHoverOverFirstProductAndClickAddToCart()
        {
            await ProductsPage.HoverOverProductAddToCart(0);
        }

        [When(@"Click Continue Shopping button")]
        public async Task WhenClickContinueShoppingButton()
        {
            await ProductsPage.ClickContinueShopping();
        }

        [When(@"Hover over second product and click Add to cart")]
        public async Task WhenHoverOverSecondProductAndClickAddToCart()
        {
            await ProductsPage.HoverOverProductAddToCart(1);
        }

        [When(@"Click View Cart button")]
        public async Task WhenClickViewCartButton()
        {
            await ProductsPage.ClickViewCart();
        }

        [Then(@"Verify both products are added to Cart")]
        public async Task ThenVerifyBothProductsAreAddedToCart()
        {
            await CartPage.AssertProducts();
        }

        [Then(@"Verify their prices, quantity and total price")]
        public async Task ThenVerifyTheirPricesQuantityAndTotalPrice()
        {
            await CartPage.AssertProductsDetails();
        }

        [When(@"Click View Product for any product on home page")]
        public async Task WhenClickViewProductForAnyProductOnHomePage()
        {
            await ProductsPage.ClickViewProduct(1);
        }

        [Then(@"Verify product detail is opened")]
        public async Task ThenVerifyProductDetailIsOpened()
        {
            await ProductDetailsPage.AssertUrl();
        }

        [When(@"Increase quantity to (.*)")]
        public async Task WhenIncreaseQuantityTo(int p0)
        {
            await ProductDetailsPage.ChangeQuantity(p0);
        }

        [When(@"Click Add to cart button")]
        public async Task WhenClickAddToCartButton()
        {
            await ProductDetailsPage.ClickAddToCart();
        }

        [Then(@"Verify that product is displayed in cart page with exact quantity (.*)")]
        public async Task ThenVerifyThatProductIsDisplayedInCartPageWithExactQuantity(int p0)
        {
            await CartPage.AssertProductQuantity(p0);
        }

        [When(@"Click X button corresponding to particular product")]
        public async Task WhenClickXButtonCorrespondingToParticularProduct()
        {
            await CartPage.ClickRemoveProduct();
        }

        [Then(@"Verify that product is removed from the cart")]
        public async Task ThenVerifyThatProductIsRemovedFromTheCart()
        {
            await CartPage.AssertEmptyCart();
        }
    }
}
