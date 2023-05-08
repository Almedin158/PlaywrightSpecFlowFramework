Feature: ContactUs

Scenario: Contact us form
	When Navigate to url http://automationexercise.com
	Then Verify that home page is visible successfully
	When Click on Contact Us button
	Then Verify GET IN TOUCH is visible
	When Enter name, email, subject and message
	And Upload file
	And Click Submit button
	Then Verify success message Success! Your details have been submitted successfully. is visible
	When Click Home button
#	And Close ad
	Then Verify that user is navigated to home page