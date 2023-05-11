Feature: Categories

Scenario: View category products
	When Navigate to url http://automationexercise.com
	Then Verify that categories are visible on left side bar
	When Click on Women category
	When Click on any category link under Women category, for example: Dress
	And Close ad
	Then Verify that category page is displayed and confirm text "WOMEN - TOPS PRODUCTS"
	When On left side bar, click on any sub-category link of Men category
	Then Verify that user is navigated to that category page

Scenario: Vuew cart brand products
	When Navigate to url http://automationexercise.com
	And Click on Products button
	And Close ad
	Then Verify that Brands are visible on left side bar
	When Click on any brand name
	Then Verify that user is navigated to brand page and brand products are displayed
	When On left side bar, click on any other brand link
	Then Verify that user is navigated to that brand page and can see products