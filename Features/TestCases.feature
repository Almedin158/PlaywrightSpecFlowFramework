Feature: TestCases

Scenario: Verify test cases page
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Test Cases button
	And Close ad
	Then Verify user is navigated to test cases page successfully