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
            new LandingPage().ClickLoginButton();
        }

        [When(@"I enter  Username '(.*)' and Password '(.*)'")]
        public void WhenIEnterUsernameAndPassword(string username, string password)
        {
            new LoginPage().TypeUserName(username).TypePassword(password);
        }

        [When(@"I press submit button")]
        public void WhenIPressButtonWithClass()
        {
            new LoginPage().ClickLoginButton();
        }

        [Then(@"main page will open")]
        public void ThenMainPageWillOpen()
        {
            Assert.IsTrue(new LandingPage().IsPageOpen());
        }

        [Then(@"I have possibility to select edit event menu")]
        public void ThenIHavePossibilityToSelectEditEventMenu()
        {
            Assert.IsTrue(new LandingPage().IsEditEventsLinkPresent());
        }

        [Then(@"""(.*)"" name exist in header\.")]
        public void ThenNameExistInHeader_(string username)
        {
            Assert.IsTrue(new LandingPage().CheckUserName(username));
        }

        [Then(@"I have not possibility to select edit event menu")]
        public void ThenIHaveNotPossibilityToSelectEditEventMenu()
        {
            Assert.IsFalse(new LandingPage().IsEditEventsLinkPresent());
        }

        [Then(@"I am stayed on login page")]
        public void AmStayedOnLoginPage()
        {
            Assert.IsTrue(new LoginPage().IsPageOpen());
        }

        [Then(@"Error Occurred")]
        public void ThenErrorOccurred()
        {
            Assert.IsTrue(new LoginPage().IsErrorOccured());
        }
    }
}
