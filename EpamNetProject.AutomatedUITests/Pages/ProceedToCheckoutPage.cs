using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class ProceedToCheckoutPage
    {
        private IWebDriver _driver;

        private readonly IWebElement _submitButton;

        private readonly ReadOnlyCollection<IWebElement> _selectedSeats;

        private readonly IWebElement _ticketCountField;

        private readonly IWebElement _errorBlock;


        public ProceedToCheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            try
            {
                _submitButton = _driver.FindElement(By.ClassName(SubmitButtonClassname));
                _selectedSeats = _driver.FindElements(By.ClassName(SelectedSeatClassname));
            }
            catch
            {
                // ignored
            }

            try
            {
                _errorBlock = _driver.FindElement(By.TagName(ErrorBlockTag));
            }
            catch
            {
                // ignored
            }
            try
            {
                _ticketCountField = _driver.FindElement(By.Id(CountOfTicketsId));
            }
            catch
            {
                // ignored
            }
        }

        public static ProceedToCheckoutPage GetPage(IWebDriver webDriver)
        {
            return new ProceedToCheckoutPage(webDriver);
        }

        public bool IsPageOpen()
        {
            return _driver.Url.Equals("http://localhost:5000/Events/Events/Buy");
        }

        public ProceedToCheckoutPage PressSubmitButton()
        {
            _submitButton.Click();
            return this;
        }

        public bool SelectedSeatsExists()
        {
            return _selectedSeats.Any();
        }

        public bool IsSuccessfulPageWithTicketsExists()
        {
            return int.Parse(_ticketCountField.Text) > 0;
        }

        public bool IsErrorNotEnoughPageOpen()
        {
            return "Not enough money on balance".Equals(_errorBlock.Text);
        }

        private const string CountOfTicketsId = "countOfTickets";

        private const string SelectedSeatClassname = "selectedSeat-item";

        private const string SubmitButtonClassname = "button__submit";

        private const string ErrorBlockTag = "h2";
    }
}
