Feature: LogoutUser

Scenario: Logout User
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	Then Verify Login to your account is visible
	When Enter correct email address and password
	| Email                  | Password   |
	| ultradummy@ultradummy.com | ultradummy |
	And Click login button
	Then Verify that Logged in as username is visible
	When Click Logout button
	Then Verify that user is navigated to login page
