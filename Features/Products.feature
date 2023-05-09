Feature: Products

Scenario: Verify all products and product detail page
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Products button
    And Close ad
	Then Verify user is navigated to ALL PRODUCTS page successfully
	And The products list is visible
	When Click on View Product of first product
	Then User is landed to product detail page
	And Verify that detail is visible: product name, category, price, availability, condition, brand

Scenario: Search product
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Products button
	And Close ad
	Then Verify user is navigated to ALL PRODUCTS page successfully
	When Enter product name in search input and click search button
	| Name |
	| Blue |
	Then Verify SEARCHED PRODUCTS is visible
	And Verify all the products related to search are visible
	| Name |
	| Blue |

Scenario: Add products in cart
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Products button
	And Close ad
	And Hover over first product and click Add to cart
	And Click Continue Shopping button
	And Hover over second product and click Add to cart
	And Click View Cart button
	Then Verify both products are added to Cart
	And Verify their prices, quantity and total price

Scenario: Verify product quantity in cart
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click View Product for any product on home page
	And Close ad
	Then Verify product detail is opened
	When Increase quantity to 4
	And Click Add to cart button
	And Click View Cart button
	Then Verify that product is displayed in cart page with exact quantity 4