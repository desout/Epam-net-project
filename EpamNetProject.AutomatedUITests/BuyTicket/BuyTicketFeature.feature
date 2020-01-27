Feature: BuyTicketFeature
	This feature test buy ticket flow.

@mytag
Scenario: Buy ticket
	Given I have logged in with Username 'desout' and Password 'Desoutside1'
	And Account balance not zero
	When I go to event and select seat
	And I press button "Proceed to checkout"
	Then Page with selected seats will open.
	And I press button "Buy"
	Then Successful payment page opened

Scenario: Buy ticket failure zero balance
	Given I have logged in with Username 'desout1' and Password 'Desoutside1'
	And Account balance is zero
	When I go to event and select seat
	And I press button "Proceed to checkout"
	Then Page with selected seats will open.
	And I press button "Buy"
	Then Failure payment page opened

Scenario: Buy ticket failure seat reserved
	Given I have logged in with Username 'desout' and Password 'Desoutside1'
	And Account balance not zero
	Given At least one seat bought or buy seat
	When I go to event and select reserved seat
	Then Error with classname "ErrorClass" is shown
