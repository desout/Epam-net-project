using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventsPage : BasePage
    {
        private const string FirstEventClassName = "event--item__name";

        private static IWebElement FirstEvent => Driver.FindElement(By.ClassName(FirstEventClassName));

        public static IReadOnlyCollection<IWebElement> GetEventsByName(string name)
        {
            return findElementsBy($"//*[@class='event--item__name' and text()='{name}']", SelectorType.Xpath);
        }


        public static EventPage SelectFirstEvent()
        {
            FirstEvent.Click();
            Thread.Sleep(1000);
            return new EventPage();
        }
    }
}
