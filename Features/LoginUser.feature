Feature: LoginUser

Scenario: Login user with correct email and password
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	Then Verify Login to your account is visible
	When Enter correct email address and password
	| Email                     | Password   |
	| ultradummy@ultradummy.com | ultradummy |
	And Click login button
	Then Verify that Logged in as username is visible

Scenario: Login user with incorrect email and password
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	Then Verify Login to your account is visible
	When Enter incorrect email address and password
	| Email                        | Password      |
	| ultra123dummy@ultradummy.com | ultry123dummy |
	And Click login button
	Then Verify error Your email or password is incorrect! is visible

Scenario: Place order: Login before checkout
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	And Fill email, password and click Login button
	| Email                        | Password      |
	| ultradummy@ultradummy.com | ultradummy |
	Then Verify that Logged in as username is visible
	When Add products to cart
	And Click Cart button
	Then Verify that cart page is displayed
	When Click Proceed To Checkout
	Then Verify Address Details and Review Your Order
	When Enter description in comment text area and click Place Order
	And Close ad
	And Enter payment details: Name on Card, Card Number, CVC, Expiration date
	And Click Pay and Confirm Order button
	Then Verify success message Your order has been placed successfully!