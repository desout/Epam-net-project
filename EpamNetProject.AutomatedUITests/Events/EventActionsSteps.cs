using System;
using System.Linq;
using EpamNetProject.AutomatedUITests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests.Events
{
    [Binding]
    public class EventActionsSteps : BaseTest
    {
        [Given(@"I am on edit event page")]
        public void GivenIAmOnEditEventPage()
        {
            new LandingPage().ClickEditEventsLink();
        }

        [Then(@"I press submit button")]
        public void ThenIPressButtonWithClassname()
        {
            new EditEventPage().ClickSubmitButton();
        }

        [Then(@"I press add new button")]
        public void ThenIPressAddNewButton()
        {
            new EditEventsPage().ClickAddNewButton();
        }

        [Then(
            @"fill all Input on edit event page with data: Name - ""(.*)"", Description - ""(.*)"", Time\(difference between today and time\) - ""(.*)"", Title- ""(.*)""")]
        public void ThenFillAllInputOnEditEventPageWithDataName_Description_TimeDifferenceBeetwenTodayAndTime_Title_(
            string name, string description, int time, string title)
        {
            new EditEventPage()
                .TypeNameField(name)
                .TypeDescriptionField(description)
                .TypeTimeField(DateTime.Now.AddDays(time).ToString("MM/dd/yyyy hh:mm"))
                .TypeTitleField(title)
                .TypeImgField("img")
                .CloseDatepicker();
        }

        [Then(@"I go to events page")]
        public void ThenIGoToEventsPage()
        {
            new LandingPage().ClickEventsLink();
        }

        [Then(@"Event with Name ""(.*)"" exists")]
        public void ThenEventWithNameExists(string name)
        {
            var items = new EventsPage().GetEventsByName(name);
            Assert.IsTrue(items.Any());
        }

        [Then(@"I press button in block with text ""(.*)""")]
        public void ThenIPressButtonInBlockWithText(string text)
        {
            new EditEventsPage().ClickDeleteButtonOnEvent(text);
        }

        [Then(@"Event with Name ""(.*)"" not exists")]
        public void ThenEventWithNameNotExists(string name)
        {
            var items = new EventsPage().GetEventsByName(name);
            Assert.IsFalse(items.Any());
        }

        [Then(@"I press link in block with text ""(.*)""")]
        public void ThenIPressLinkInBlockWithText(string text)
        {
            new EditEventsPage().ClickEditLinkOnEvent(text);
        }

        [Then(@"fill name input on edit event page with data: Name - ""(.*)""")]
        public void ThenFillNameInputOnEditEventPageWithDataName_(string name)
        {
           new EditEventPage().TypeTitleField(name).TypeImgField("img");
        }
    }
}
