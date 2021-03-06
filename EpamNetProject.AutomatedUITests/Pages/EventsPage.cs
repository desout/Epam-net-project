using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventsPage:BasePage
    {

        private const string FirstEventClassName = "event--item__name";

        private static IWebElement FirstEvent => FindElementByClassName(FirstEventClassName);

        public EventsPage()
        {
        }
        public IReadOnlyCollection<IWebElement> GetEventsByName(string name)
        {
            return FindElementsByXPath($"//*[@class='event--item__name' and text()='{name}']");
        }


        public EventPage SelectFirstEvent()
        {
            FirstEvent.Click();
            Thread.Sleep(1000);
            return new EventPage();
        }
    }
}
