using System.Threading;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class EventPage : BasePage
    {
        private const string ProceedToCheckoutId = "proceed-to-checkout";

        private const string ReservedSeatSelector = "[data-seat-status='2']";

        private const string AvailableSeatSelector = "[data-seat-status='0']";

        private const string ErrorBlockClassName = "validation-summary-errors";

        private static IWebElement AvailableSeat => findElementBy(AvailableSeatSelector, SelectorType.Css);

        private static IWebElement ErrorBlock => findElementBy(ErrorBlockClassName, SelectorType.ClassName);

        private static IWebElement ProceedToCheckoutButton => findElementBy(ProceedToCheckoutId, SelectorType.Id);

        private static IWebElement ReservedSeat => findElementBy(ReservedSeatSelector, SelectorType.Css);

        public static bool IsErrorOccured()
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
