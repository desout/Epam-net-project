using EpamNetProject.AutomatedUITests.Pages;
using EpamNetProject.AutomatedUITests.Services;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests.BuyTicket
{
    [Binding]
    public class BuyTicketFeatureSteps : BaseTest
    {
        [Given(@"I have logged in as manager")]
        public void GivenIHaveLoggedInAsManager()
        {
            LoginService.LoginAsManager();
        }

        [Given(@"I have logged in as user")]
        public void GivenIHaveLoggedInAsUser()
        {
            LoginService.LoginAsUser();
        }

        [Given(@"I have logged in as admin")]
        public void GivenIHaveLoggedInAsAdmin()
        {
            LoginService.LoginAsAdmin();
        }

        [When(@"I go to event and select seat")]
        public void WhenIGoToEventAndSelectSeat()
        {
            new LandingPage()
                .ClickEventsLink()
                .SelectFirstEvent()
                .SelectAvailableSeat();
        }

        [When(@"I press proceed to checkout button")]
        public void WhenIPressButton()
        {
            new EventPage()
                .PressProceedToCheckoutButton();
        }

        [Then(@"Page with selected seats will open\.")]
        public void ThenPageWithSelectedSeatsWillOpen_()
        {
            var page = new ProceedToCheckoutPage();
            Assert.IsTrue(page.SelectedSeatsExists());
        }

        [Then(@"I press buy button")]
        public void ThenIPressButton()
        {
            new ProceedToCheckoutPage().PressSubmitButton();
        }

        [Then(@"Successful payment page opened")]
        public void ThenSuccessfulPaymentPageOpened()
        {
            Assert.IsTrue(new ProceedToCheckoutPage().IsSuccessfulPageWithTicketsExists());
        }

        [Then(@"Failure payment page opened")]
        public void ThenFailurePaymentPageOpened()
        {
            Assert.IsTrue(new ProceedToCheckoutPage().IsErrorNotEnoughPageOpen());
        }

        [When(@"I go to event and select reserved seat")]
        public void WhenIGoToEventAndSelectReservedSeat()
        {
            new LandingPage().ClickEventsLink().SelectFirstEvent().SelectReservedSeat();
        }

        [Then(@"Error is shown")]
        public void ThenErrorWithClassnameIsShown()
        {
            Assert.IsTrue(new EventPage().IsErrorOccured());
        }
    }
}
