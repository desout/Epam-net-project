using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class ProceedToCheckoutPage : BasePage
    {
        private const string CountOfTicketsId = "countOfTickets";

        private const string SelectedSeatClassname = "selectedSeat-item";

        private const string SubmitButtonClassname = "button__submit";

        private const string ErrorBlockTag = "h2";

        private const string ErrorBlockText = "Not enough money on balance";


        private static IWebElement ErrorBlock => findElementBy(ErrorBlockTag, SelectorType.Id);

        private static IReadOnlyCollection<IWebElement> SelectedSeats =>
            findElementsBy(SelectedSeatClassname, SelectorType.ClassName);

        private static IWebElement SubmitButton => findElementBy(SubmitButtonClassname, SelectorType.ClassName);

        private static IWebElement TicketCountField => findElementBy(CountOfTicketsId, SelectorType.Id);

        public ProceedToCheckoutPage PressSubmitButton()
        {
            SubmitButton.Click();
            return this;
        }

        public static bool SelectedSeatsExists()
        {
            return SelectedSeats.Any();
        }

        public static bool IsSuccessfulPageWithTicketsExists()
        {
            return int.Parse(TicketCountField.Text) > 0;
        }

        public static bool IsErrorNotEnoughPageOpen()
        {
            return ErrorBlockText.Equals(ErrorBlock.Text);
        }
    }
}
