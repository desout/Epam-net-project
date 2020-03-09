Feature: BuyTicketFeature
	This feature test buy ticket flow.

@mytag
Scenario: Buy ticket
	Given I have logged in as manager
	When I go to event and select seat
	And I press proceed to checkout button
	Then Page with selected seats will open.
	And I press buy button
	Then Successful payment page opened

Scenario: Buy ticket failure zero balance
	Given I have logged in as user
	When I go to event and select seat
	And I press proceed to checkout button
	Then Page with selected seats will open.
	And I press buy button
	Then Failure payment page opened

Scenario: Buy ticket failure seat reserved
	Given I have logged in as admin
	When I go to event and select reserved seat
	Then Error is shown
