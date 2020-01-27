using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests
{
    [Binding]
    public class EventActionsSteps : IDisposable
    {
        private readonly IWebDriver _driver;

        public EventActionsSteps()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
        }

        [Given(@"I have logged in with Username '(.*)' and Password '(.*)' like manager")]
        public void GivenIHaveLoggedInWithUsernameAndPasswordLikeManager(string username, string password)
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/User/Account/Login");
            var usernameBox = _driver.FindElement(By.Id("UserName"));
            var passwordBox = _driver.FindElement(By.Id("Password"));
            var submitBtn = _driver.FindElement(By.ClassName("button__submit"));

            usernameBox.SendKeys(username);
            passwordBox.SendKeys(password);
            submitBtn.Click();
            var actualUser = _driver.FindElement(By.Id("userName-header")).Text;

            Assert.IsTrue(actualUser.Contains(username));
        }

        [Given(@"I am on edit event page")]
        public void GivenIAmOnEditEventPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Manager/EditEvents");
        }

        [Given(@"Event with Name ""(.*)"" exists on page or created new one")]
        public void GivenEventWithNameExistsOnPageOrCreatedNewOne(string name)
        {
            var items = _driver.FindElements(By.XPath($"//*[@class='event--item__name' and text()='{name}']"));

            if (items.Count != 0)
            {
                return;
            }

            ThenIPressButtonWithClassname("editEvent__block__AddNew");
            ThenFillAllInputOnEditEventPageWithDataName_Description_TimeDifferenceBeetwenTodayAndTime_Title_(
                "Automation test event name", "Automation test event description", 100, "Automation test event title");
            ThenIPressButtonWithClassname("button__submit");
        }

        [Then(@"I press button with classname ""(.*)""")]
        public void ThenIPressButtonWithClassname(string classname)
        {
            var submitBtn = _driver.FindElement(By.ClassName(classname));

            submitBtn.Click();
        }

        [Then(
            @"fill all Input on edit event page with data: Name - ""(.*)"", Description - ""(.*)"", Time\(difference beetwen today and time\) - ""(.*)"", Title- ""(.*)""")]
        public void ThenFillAllInputOnEditEventPageWithDataName_Description_TimeDifferenceBeetwenTodayAndTime_Title_(
            string name, string description, int time, string title)
        {
            var nameBox = _driver.FindElement(By.Id("Name"));
            var descriptionBox = _driver.FindElement(By.Id("Description"));
            var timeBox = _driver.FindElement(By.Id("Time"));
            var titleBox = _driver.FindElement(By.Id("Title"));
            var imgUrlBox = _driver.FindElement(By.Id("ImgUrl"));

            nameBox.SendKeys(name);
            descriptionBox.SendKeys(description);
            timeBox.Clear();
            timeBox.SendKeys(DateTime.Now.AddDays(time).ToString("MM/dd/yyyy hh:mm"));
            titleBox.SendKeys(title);
            imgUrlBox.SendKeys("img");
            _driver.FindElement(By.ClassName("ui-datepicker-close")).Click();
        }

        [Then(@"I go to events page")]
        public void ThenIGoToEventsPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Events/Events");
        }

        [Then(@"Event with Name ""(.*)"" exists")]
        public void ThenEventWithNameExists(string name)
        {
            var items = _driver.FindElements(By.XPath($"//*[@class='event--item__name' and text()='{name}']"));

            Assert.IsTrue(items.Count > 0);
        }

        [Then(@"I press button in block with classname ""(.*)"" near paragrah with classname '(.*)' and text ""(.*)""")]
        public void ThenIPressButtonInBlockWithClassnameNearParagrahWithClassnameAndText(string blockClassname,
            string paragraphClassname, string text)
        {
            var element = _driver
                .FindElement(By.XPath(
                    $"//*[@class='{paragraphClassname}' and text()='{text}']/following-sibling::div[@class='{blockClassname}']"))
                .FindElement(By.TagName("button"));

            element.Click();
            Thread.Sleep(1000);
        }

        [Then(@"Event with Name ""(.*)"" not exists")]
        public void ThenEventWithNameNotExists(string name)
        {
            var elements = _driver.FindElements(By.XPath($"//*[@class='event--item__name' and text()='{name}']"));

            Assert.AreEqual(0, elements.Count);
        }

        [Then(@"I press link in block with classname ""(.*)"" near paragrah with classname '(.*)' and text ""(.*)""")]
        public void ThenIPressLinkInBlockWithClassnameNearParagrahWithClassnameAndText(string blockClassname,
            string paragraphClassname, string text)
        {
            var element = _driver
                .FindElement(By.XPath(
                    $"//*[@class='{paragraphClassname}' and text()='{text}']/following-sibling::div[@class='{blockClassname}']"))
                .FindElement(By.TagName("a"));

            element.Click();
        }

        [Then(@"fill name input on edit event page with data: Name - ""(.*)""")]
        public void ThenFillNameInputOnEditEventPageWithDataName_(string name)
        {
            var nameBox = _driver.FindElement(By.Id("Title"));

            nameBox.Clear();
            nameBox.SendKeys(name);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
