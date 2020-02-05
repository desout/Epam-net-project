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
        [Given(@"I have logged in as manager")]
        public void GivenIHaveLoggedInWithUsernameAndPasswordLikeManager()
        {
            var result = LoginUtils.LoginAsManager(Driver);
            Assert.IsTrue(result);
        }

        [Given(@"I am on edit event page")]
        public void GivenIAmOnEditEventPage()
        {
            EditEventsPage.GetPage(Driver).GoToPage();
        }

        [Then(@"I press button with classname ""(.*)""")]
        public void ThenIPressButtonWithClassname(string classname)
        {
            EditEventPage.GetPage(Driver).ClickSubmitButton();
        }

        [Then(@"I press add new button")]
        public void ThenIPressAddNewButton()
        {
            EditEventsPage.GetPage(Driver).ClickAddNewButton();
        }

        [Then(
            @"fill all Input on edit event page with data: Name - ""(.*)"", Description - ""(.*)"", Time\(difference between today and time\) - ""(.*)"", Title- ""(.*)""")]
        public void ThenFillAllInputOnEditEventPageWithDataName_Description_TimeDifferenceBeetwenTodayAndTime_Title_(
            string name, string description, int time, string title)
        {
            EditEventPage.GetPage(Driver)
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
            EventsPage.GetPage(Driver).GoToPage();
        }

        [Then(@"Event with Name ""(.*)"" exists")]
        public void ThenEventWithNameExists(string name)
        {
            var items = EventsPage.GetPage(Driver).GetEventsByName(name);
            Assert.IsTrue(items.Any());
        }

        [Then(@"I press button in block with text ""(.*)""")]
        public void ThenIPressButtonInBlockWithText(string text)
        {
            EditEventsPage.GetPage(Driver).ClickDeleteButtonOnEvent(text);
        }

        [Then(@"Event with Name ""(.*)"" not exists")]
        public void ThenEventWithNameNotExists(string name)
        {
            var items = EventsPage.GetPage(Driver).GetEventsByName(name);
            Assert.IsTrue(!items.Any());
        }

        [Then(@"I press link in block with text ""(.*)""")]
        public void ThenIPressLinkInBlockWithText(string text)
        {
            EditEventsPage.GetPage(Driver).ClickEditLinkOnEvent(text);
        }

        [Then(@"fill name input on edit event page with data: Name - ""(.*)""")]
        public void ThenFillNameInputOnEditEventPageWithDataName_(string name)
        {
            var nameBox = Driver.FindElement(By.Id("Title"));
            EditEventPage.GetPage(Driver).TypeTitleField(name).TypeImgField("img");
            nameBox.Clear();
            nameBox.SendKeys(name);
        }
    }
}