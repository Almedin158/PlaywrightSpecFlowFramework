Feature: RegisterUser

Scenario: Register user
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	Then Verify New User Signup! is visible
	When Enter name and email address
	And Click Signup button
	Then Verify that ENTER ACCOUNT INFORMATION is visible
	When Fill details: Title, Name, Email, Password, Date of birth
	And Select checkbox Sign up for our newsletter!
	And Select checkbox Receive special offers from our partners!
	And Fill details: First name, Last name, Company, Address, Address2, Country, State, City, Zipcode, Mobile Number
	And Click Create Account button
	Then Verify that ACCOUNT CREATED! is visible
	When Click Continue button
	And Close ad
	Then Verify that Logged in as username is visible
	When Click Delete Account button
	Then Verify that ACCOUNT DELETED! is visible and click Continue button

Scenario: Register User with existing email
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	Then Verify New User Signup! is visible
	When Enter name and already registered email address
	| Name       | Email                     |
	| Ultradummy | ultradummy@ultradummy.com |
	And Click Signup button
	Then Verify error Email Address already exist! is visible
	
Scenario: Place order: register while checkout
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Add products to cart
	And Click Cart button
	Then Verify that cart page is displayed
	When Click Proceed To Checkout
	And Click Register / Login button 
	And Fill all details in Signup and create account
	Then Verify ACCOUNT CREATED! and click Continue button
	When Close ad
	Then Verify that Logged in as username is visible
	When Click Cart button
	And Click Proceed To Checkout button
	Then Verify Address Details and Review Your Order
	When Enter description in comment text area and click Place Order
	And Enter payment details: Name on Card, Card Number, CVC, Expiration date
	And Click Pay and Confirm Order button
	Then Verify success message Your order has been placed successfully!
	When Click Delete Account button
	Then Verify ACCOUNT DELETED! and click Continue button

Scenario: Place order: register before checkout
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	And Fill all details in Signup and create account
	Then Verify ACCOUNT CREATED! and click Continue button
	When Close ad
	Then Verify that Logged in as username is visible
	When Add products to cart
	And Click Cart button
	Then Verify that cart page is displayed
	When Click Proceed To Checkout
	Then Verify Address Details and Review Your Order
	When Enter description in comment text area and click Place Order
	And Enter payment details: Name on Card, Card Number, CVC, Expiration date
	And Click Pay and Confirm Order button
	Then Verify success message Your order has been placed successfully!
	When Click Delete Account button
	Then Verify ACCOUNT DELETED! and click Continue button