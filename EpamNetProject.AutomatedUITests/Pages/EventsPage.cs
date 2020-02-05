using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventsPage
    {
        private readonly IWebDriver _driver;
        
        private readonly IWebElement _firstEvent;
        private EventsPage(IWebDriver driver)
        {
            _driver = driver;
            _firstEvent = _driver.FindElement(By.ClassName(FirstEventClassName));
        }

        public static EventsPage GetPage(IWebDriver webDriver)
        {
            return new EventsPage(webDriver);
        }

        public EventsPage GoToPage()
        {
            _driver.Navigate().GoToUrl(PageLink);
            return new EventsPage(_driver);
        }

        public ReadOnlyCollection<IWebElement> GetEventsByName(string name)
        {
            return _driver.FindElements(By.XPath($"//*[@class='event--item__name' and text()='{name}']"));
        }


        public EventPage SelectFirstEvent()
        {
            _firstEvent.Click();
            Thread.Sleep(1000);
            return EventPage.GetPage(_driver);
        }

        private const string PageLink = "http://localhost:5000/Events/Events";
        private const string FirstEventClassName = "event--item__name";
    }
}
