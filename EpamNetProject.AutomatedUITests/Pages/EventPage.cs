using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventPage
    {
        private readonly IWebDriver _driver;

        private readonly IWebElement _errorBlock;

        private readonly IWebElement _availableSeat;

        private readonly IWebElement _reservedSeat;

        private readonly IWebElement _proceedToCheckoutButton;

        private EventPage(IWebDriver driver)
        {
            _driver = driver;
            
            _errorBlock = _driver.FindElement(By.ClassName(ErrorBlockClassName));
            _availableSeat = _driver.FindElement(By.CssSelector(AvailableSeatSelector));
            _reservedSeat = _driver.FindElement(By.CssSelector(ReservedSeatSelector));
            _proceedToCheckoutButton = _driver.FindElement(By.Id(ProceedToCheckoutId));
        }

        public static EventPage GetPage(IWebDriver webDriver)
        {
            return new EventPage(webDriver);
        }

        public bool isErrorOccured()
        {
            return _errorBlock.Displayed;
        }

        public EventPage SelectAvailableSeat()
        {
            _availableSeat.Click();
            Thread.Sleep(1000);
            return this;
        }

        public EventPage SelectReservedSeat()
        {
            _reservedSeat.Click();
            Thread.Sleep(1000);
            return this;
        }

        public EventPage PressProceedToCheckoutButton()
        {
            _proceedToCheckoutButton.Click();
            return this;
        }

        private const string ProceedToCheckoutId = "proceed-to-checkout";

        private const string ReservedSeatSelector = "[data-seat-status='2']";

        private const string AvailableSeatSelector = "[data-seat-status='0']";

        private const string ErrorBlockClassName = "ErrorClass";
    }
}
