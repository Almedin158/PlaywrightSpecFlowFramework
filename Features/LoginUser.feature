Feature: LoginUser

Scenario: Login user with correct email and password
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	Then Verify Login to your account is visible
	When Enter correct email address and password
	| Username                  | Password   |
	| ultradummy@ultradummy.com | ultradummy |
	And Click login button
	Then Verify that Logged in as username is visible

Scenario: Login user with incorrect email and password
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Signup / Login button
	Then Verify Login to your account is visible
	When Enter incorrect email address and password
	| Username                  | Password   |
	| ultra123dummy@ultradummy.com | ultry123dummy |
	And Click login button
	Then Verify error Your email or password is incorrect! is visible