using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventPage: BasePage
    {
        private const string ProceedToCheckoutId = "proceed-to-checkout";

        private const string ReservedSeatSelector = "[data-seat-status='2']";

        private const string AvailableSeatSelector = "[data-seat-status='0']";

        private const string ErrorBlockClassName = "validation-summary-errors";

        private static IWebElement AvailableSeat => FindElementByCss(AvailableSeatSelector);

        private static IWebElement ErrorBlock => FindElementByClassName(ErrorBlockClassName);

        private static IWebElement ProceedToCheckoutButton => FindElementById(ProceedToCheckoutId);

        private static IWebElement ReservedSeat => FindElementByCss(ReservedSeatSelector);

        public EventPage()
        {
        }
        public bool IsErrorOccured()
        {
            return ErrorBlock.Displayed;
        }

        public EventPage SelectAvailableSeat()
        {
            AvailableSeat.Click();
            Thread.Sleep(1000);
            return this;
        }

        public EventPage SelectReservedSeat()
        {
            ReservedSeat.Click();
            Thread.Sleep(1000);
            return this;
        }

        public EventPage PressProceedToCheckoutButton()
        {
            ProceedToCheckoutButton.Click();
            return this;
        }
    }
}
