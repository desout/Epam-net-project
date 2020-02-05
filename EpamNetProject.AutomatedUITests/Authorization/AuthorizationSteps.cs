using EpamNetProject.AutomatedUITests.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests.Authorization
{
    [Binding]
    public class AuthorizationSteps : BaseTest
    {
        [Given(@"I am on login page")]
        public void GivenIAmOnLoginPage()
        {
            LoginPage.GetPage(Driver).GoToPage();
        }

        [When(@"I enter  Username '(.*)' and Password '(.*)'")]
        public void WhenIEnterUsernameAndPassword(string username, string password)
        {
            LoginPage.GetPage(Driver).TypeUserName(username).TypePassword(password);
        }

        [When(@"I press button with class ""(.*)""")]
        public void WhenIPressButtonWithClass(string p0)
        {
            LoginPage.GetPage(Driver).ClickLoginButton();
        }

        [Then(@"main page will open")]
        public void ThenMainPageWillOpen()
        {
            Assert.IsTrue(LandingPage.GetPage(Driver).IsPageOpen());
        }

        [Then(@"I have possibility to select edit event menu")]
        public void ThenIHavePossibilityToSelectEditEventMenu()
        {
            Assert.IsTrue(LandingPage.GetPage(Driver).IsEditEventsLinkPresent());
        }

        [Then(@"""(.*)"" name exist in header\.")]
        public void ThenNameExistInHeader_(string username)
        {
            Assert.IsTrue(LandingPage.GetPage(Driver).CheckUserName(username));
        }

        [Then(@"I have not possibility to select edit event menu")]
        public void ThenIHaveNotPossibilityToSelectEditEventMenu()
        {
            Assert.IsTrue(!LandingPage.GetPage(Driver).IsEditEventsLinkPresent());
        }

        [Then(@"I am stayed on login page")]
        public void IAmStayedOnLoginPage()
        {
            Assert.IsTrue(LoginPage.GetPage(Driver).IsPageOpen());
        }

        [Then(@"Error Occurred")]
        public void ThenErrorOccurred()
        {
            Assert.IsTrue(LoginPage.GetPage(Driver).IsErrorOccured());
        }
    }
}