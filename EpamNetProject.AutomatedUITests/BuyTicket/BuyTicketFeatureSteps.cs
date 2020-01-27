using OpenQA.Selenium;
using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests
{
    [Binding]
    public class BuyTicketFeatureSteps : IDisposable
    {
        private readonly IWebDriver _driver;

        public BuyTicketFeatureSteps()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
        }

        [Given(@"I have logged in with Username '(.*)' and Password '(.*)'")]
        public void GivenIHaveLoggedIn(string username, string password)
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/User/Account/Login");
            var usernameBox = _driver.FindElement(By.Id("UserName"));
            var passwordBox = _driver.FindElement(By.Id("Password"));
            var submitBtn = _driver.FindElement(By.ClassName("button__submit"));

            usernameBox.SendKeys(username);
            passwordBox.SendKeys(password);
            submitBtn.Click();
            var actualUser = _driver.FindElement(By.Id("userName-header")).Text;

            Assert.IsTrue(actualUser.Contains(username));
        }

        [Given(@"Account balance not zero")]
        public void GivenAccountBalanceNotZero()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/User/Account/Profile");

            var balance = int.Parse(_driver.FindElement(By.Id("Balance")).Text);

            Assert.IsTrue(balance > 0);
        }

        [When(@"I go to event and select seat")]
        public void WhenIGoToEventAndSelectSeat()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Events/Events");

            _driver.FindElement(By.ClassName("event--item__name")).Click();
            _driver.FindElement(By.CssSelector("[data-seat-status='0']")).Click();
        }

        [When(@"I press button ""(.*)""")]
        public void WhenIPressButton(string p0)
        {
            _driver.FindElement(By.Id("proceed-to-checkout")).Click();
        }

        [Then(@"Page with selected seats will open\.")]
        public void ThenPageWithSelectedSeatsWillOpen_()
        {
            Assert.IsTrue(_driver.FindElements(By.ClassName("selectedSeat-item")).Count > 0);
        }

        [Then(@"I press button ""(.*)""")]
        public void ThenIPressButton(string p0)
        {
            _driver.FindElement(By.ClassName("button__submit")).Click();
        }

        [Then(@"Successful payment page opened")]
        public void ThenSuccessfulPaymentPageOpened()
        {
            var countOfTickets = int.Parse(_driver.FindElement(By.Id("countOfTickets")).Text);

            Assert.IsTrue(countOfTickets > 0);
        }

        [Given(@"Account balance is zero")]
        public void GivenAccountBalanceIsZero()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/User/Account/Profile");

            var balance = int.Parse(_driver.FindElement(By.Id("Balance")).Text);

            Assert.AreEqual(0, balance);
        }

        [Then(@"Failure payment page opened")]
        public void ThenFailurePaymentPageOpened()
        {
            Assert.AreEqual("Not enough money on balance", _driver.FindElement(By.TagName("h2")).Text);
        }

        [When(@"I go to event and select reserved seat")]
        public void WhenIGoToEventAndSelectReservedSeat()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Events/Events");

            _driver.FindElement(By.ClassName("event--item__name")).Click();
            _driver.FindElement(By.CssSelector("[data-seat-status='2']")).Click();
        }

        [Then(@"Error with classname ""(.*)"" is shown")]
        public void ThenErrorWithClassnameIsShown(string p0)
        {
            var element = _driver.FindElement(By.ClassName("ErrorClass"));
            Assert.AreEqual(true, element.Displayed);
        }

        [Given(@"At least one seat bought or buy seat")]
        public void GivenAtLeastOneSeatBoughtOrBuySeat()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Events/Events");

            _driver.FindElement(By.ClassName("event--item__name")).Click();
            var elementsCount = _driver.FindElements(By.CssSelector("[data-seat-status='2']")).Count;

            if (elementsCount != 0)
            {
                return;
            }

            _driver.FindElement(By.ClassName("event--item__name")).Click();
            _driver.FindElement(By.CssSelector("[data-seat-status='0']")).Click();
            WhenIPressButton("Proceed to checkout");
            ThenPageWithSelectedSeatsWillOpen_();
            ThenIPressButton("Proceed to checkout");
            ThenSuccessfulPaymentPageOpened();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
