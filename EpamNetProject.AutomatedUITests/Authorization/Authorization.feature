Feature: Authorization
	This feature test authorization flow.

@mytag
Scenario: Successful authorization by admin
	Given I am on login page
	When I enter  Username 'desout' and Password 'Desoutside1'
	And I press button with class "button__submit"
	Then main page will open
	And I have possibility to select edit event menu
	And "desout" name exist in header.

Scenario: Successful authorization by user
	Given I am on login page
	When I enter  Username 'desout1' and Password 'Desoutside1'
	And I press button with class "button__submit"
	Then main page will open
	And I have not possibility to select edit event menu
	And "desout1" name exist in header.

	Scenario Outline: Unsuccessful authorization by admin
		Given I am on login page
		When I enter  Username '<Login>' and Password '<Password>'
		And I press button with class "button__submit"
		Then I am stayed on login page
		And Error Occurred

		Examples:
			| Login  | Password |
			| desout | drogba   |
			| desout |          |
			|        | drogba   |
			|        |          |   
