using EpamNetProject.AutomatedUITests.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests.BuyTicket
{
    [Binding]
    public class BuyTicketFeatureSteps : BaseTest
    {
        [Given(@"I have logged in as manager")]
        public void GivenIHaveLoggedInAsManager(string username, string password)
        {
            LoginUtils.LoginAsManager(Driver);
        }

        [Given(@"I have logged in as user")]
        public void GivenIHaveLoggedInAsUser()
        {
            LoginUtils.LoginAsUser(Driver);
        }

        [Given(@"I have logged in as admin")]
        public void GivenIHaveLoggedInAsAdmin()
        {
            LoginUtils.LoginAsAdmin(Driver);
        }

        [When(@"I go to event and select seat")]
        public void WhenIGoToEventAndSelectSeat()
        {
            EventsPage
                .GetPage(Driver)
                .GoToPage()
                .SelectFirstEvent()
                .SelectAvailableSeat();
        }

        [When(@"I press button ""(.*)""")]
        public void WhenIPressButton(string p0)
        {
            EventPage
                .GetPage(Driver)
                .PressProceedToCheckoutButton();
        }

        [Then(@"Page with selected seats will open\.")]
        public void ThenPageWithSelectedSeatsWillOpen_()
        {
            var page = ProceedToCheckoutPage.GetPage(Driver);
            Assert.IsTrue(page.SelectedSeatsExists());
        }

        [Then(@"I press button ""(.*)""")]
        public void ThenIPressButton(string p0)
        {
            ProceedToCheckoutPage.GetPage(Driver).PressSubmitButton();
        }

        [Then(@"Successful payment page opened")]
        public void ThenSuccessfulPaymentPageOpened()
        {
            Assert.IsTrue(ProceedToCheckoutPage.GetPage(Driver).IsSuccessfulPageWithTicketsExists());
        }

        [Then(@"Failure payment page opened")]
        public void ThenFailurePaymentPageOpened()
        {
            Assert.IsTrue(ProceedToCheckoutPage.GetPage(Driver).IsErrorNotEnoughPageOpen());
        }

        [When(@"I go to event and select reserved seat")]
        public void WhenIGoToEventAndSelectReservedSeat()
        {
            EventsPage.GetPage(Driver).GoToPage().SelectFirstEvent().SelectReservedSeat();
        }

        [Then(@"Error with classname ""(.*)"" is shown")]
        public void ThenErrorWithClassnameIsShown(string p0)
        {
            Assert.IsTrue(EventPage.GetPage(Driver).isErrorOccured());
        }
    }
}
