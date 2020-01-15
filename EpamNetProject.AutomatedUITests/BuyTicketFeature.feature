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
