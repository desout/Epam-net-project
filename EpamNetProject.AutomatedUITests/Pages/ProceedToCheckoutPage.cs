using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class ProceedToCheckoutPage: BasePage
    {
        private const string CountOfTicketsId = "countOfTickets";

        private const string SelectedSeatClassname = "selectedSeat-item";

        private const string SubmitButtonClassname = "button__submit";

        private const string ErrorBlockTag = "h2";

        private const string ErrorBlockText = "Not enough money on balance";
        private static IWebElement ErrorBlock => FindElementById(ErrorBlockTag);

        private static IReadOnlyCollection<IWebElement> SelectedSeats => FindElementsByClassName(SelectedSeatClassname);

        private static IWebElement SubmitButton => FindElementByClassName(SubmitButtonClassname);

        private static IWebElement TicketCountField => FindElementById(CountOfTicketsId);


        public ProceedToCheckoutPage()
        {
            
        }
        
        public ProceedToCheckoutPage PressSubmitButton()
        {
            SubmitButton.Click();
            return this;
        }

        public bool SelectedSeatsExists()
        {
            return SelectedSeats.Any();
        }

        public bool IsSuccessfulPageWithTicketsExists()
        {
            return int.Parse(TicketCountField.Text) > 0;
        }

        public bool IsErrorNotEnoughPageOpen()
        {
            return ErrorBlockText.Equals(ErrorBlock.Text);
        }
    }
}
