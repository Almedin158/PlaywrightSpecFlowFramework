Feature: Footer

Scenario: Verify subscription in home page
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Scroll down to footer
	Then Verify text SUBSCRIPTION
	When Enter email address in input and click arrow button
	Then Verify success message You have been successfully subscribed! is visible


Scenario: Verify subscription in cart page
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click Cart button
	And Scroll down to footer
	Then Verify text SUBSCRIPTION
	When Enter email address in input and click arrow button
	Then Verify success message You have been successfully subscribed! is visible