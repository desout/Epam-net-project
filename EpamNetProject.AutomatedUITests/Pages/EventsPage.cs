using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventsPage
    {
        private IWebDriver _driver;

        public EventsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public static EventsPage GetPage(IWebDriver webDriver)
        {
            return new EventsPage(webDriver);
        }

        public EventsPage GoToPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Events/Events");
            return new EventsPage(_driver);
        }

        public ReadOnlyCollection<IWebElement> GetEventsByName(string name)
        {
            return _driver.FindElements(By.XPath($"//*[@class='event--item__name' and text()='{name}']"));
        }


        public EventPage SelectFirstEvent()
        {
            _driver.FindElement(By.ClassName("event--item__name")).Click();
            return EventPage.GetPage(_driver);
        }
    }
}
