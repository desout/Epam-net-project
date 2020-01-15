Feature: EventActions
	This feature responsible for editing events(create,update,delete)

@mytag
Scenario: Create Event
	Given I have logged in with Username 'desout' and Password 'Desoutside1' like manager
	And I am on edit event page
	Then I press button with classname "editEvent__block__AddNew"
	And fill all Input on edit event page with data: Name - "Automation test event name", Description - "Automation test event description", Time(difference beetwen today and time) - "100", Title- "Automation test event title"
	Then I press button with classname "button__submit"
	Then I go to events page
	And Event with Name "Automation test event title" exists 

Scenario: Delete Event
	Given I have logged in with Username 'desout' and Password 'Desoutside1' like manager
	And I am on edit event page
	And Event with Name "Automation test event title" exists on page or created new one
	Then I press button in block with classname "event--item__actions" near paragrah with classname 'event--item__name' and text "Automation test event title"
	Then I go to events page
	And Event with Name "Automation test event title" not exists 

Scenario: Update Event
	Given I have logged in with Username 'desout' and Password 'Desoutside1' like manager
	And I am on edit event page
	And Event with Name "Automation test event title" exists on page or created new one
	Then I press link in block with classname "event--item__actions" near paragrah with classname 'event--item__name' and text "Automation test event title"
	And fill name input on edit event page with data: Name - "Automation test event title changed"
	Then I press button with classname "button__submit"
	Then I go to events page
	And Event with Name "Automation test event title changed" exists 