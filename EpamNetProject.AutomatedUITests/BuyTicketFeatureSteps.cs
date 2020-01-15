using OpenQA.Selenium;
using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests
{
    [Binding]
    public class BuyTicketFeatureSteps: IDisposable
    {
        private readonly IWebDriver _driver;
        public BuyTicketFeatureSteps()
        {
            _driver = new ChromeDriver();

        }

        [Given(@"I have logged in with Username '(.*)' and Password '(.*)'")]
        public void GivenIHaveLoggedIn(string username, string password)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:5000/User/Account/Login");
            var usernameBox = _driver.FindElement(By.Id("UserName"));            
            var passwordBox = _driver.FindElement(By.Id("Password"));           
            var submitBtn = _driver.FindElement(By.ClassName("button__submit"));
          
            usernameBox.SendKeys(username);
            Thread.Sleep(1000);
         
            passwordBox.SendKeys(password);
            Thread.Sleep(1000);
         
            submitBtn.Click(); 
            Thread.Sleep(1000);
            
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
           Thread.Sleep(1000);
           
           _driver.FindElement(By.CssSelector("[data-seat-status='0']")).Click();
           Thread.Sleep(1000);
        }
        
        [When(@"I press button ""(.*)""")]
        public void WhenIPressButton(string p0)
        {
            _driver.FindElement(By.Id("proceed-to-checkout")).Click();
            Thread.Sleep(1000);

        }
        
        [Then(@"Page with selected seats will open\.")]
        public void ThenPageWithSelectedSeatsWillOpen_()
        {
            Assert.IsTrue( _driver.FindElements(By.ClassName("selectedSeat-item")).Count > 0);
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

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
