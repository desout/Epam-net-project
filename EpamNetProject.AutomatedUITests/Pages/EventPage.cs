using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventPage
    {
        private IWebDriver _driver;

        public EventPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public static EventPage GetPage(IWebDriver webDriver)
        {
            return new EventPage(webDriver);
        }

        public bool isErrorOccured()
        {
            return _driver.FindElement(By.ClassName("ErrorClass")).Displayed;
        }

        public EventPage SelectAvailableSeat()
        {
            _driver.FindElement(By.CssSelector("[data-seat-status='0']")).Click();
            Thread.Sleep(1000);
            return this;
        }

        public EventPage SelectReservedSeat()
        {
            _driver.FindElement(By.CssSelector("[data-seat-status='2']")).Click();
            Thread.Sleep(1000);
            return this;
        }

        public EventPage PressProceedToCheckoutButton()
        {
            _driver.FindElement(By.Id("proceed-to-checkout")).Click();
            return this;
        }
    }
}
