Feature: EventActions
	This feature responsible for editing events(create,update,delete)
Background: 
	Given I have logged in as manager
	Given I am on edit event page

@mytag
Scenario: Create Event
	Then I press add new button
	Then fill all Input on edit event page with data: Name - "Automation test event name", Description - "Automation test event description", Time(difference between today and time) - "100", Title- "Automation test event title"
	Then I press submit button
	Then I go to events page
	And Event with Name "Automation test event title" exists 

Scenario: Delete Event
	Then I press button in block with text "Second Event"
	Then I go to events page
	And Event with Name "Second Event" not exists 

Scenario: Update Event
	Then I press link in block with text "Second Event"
	And fill name input on edit event page with data: Name - "Automation test event title changed"
	Then I press submit button
	Then I go to events page
	And Event with Name "Automation test event title changed" exists 
