Feature: MspAwsApi
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Get API response for MSP CreateCustomer post request
	Given I have provided an endpoint
	And I have a base url https://xf0lv66uc8.execute-api.eu-west-1.amazonaws.com
	When I call post method of api
	Then the result should be returned in json
