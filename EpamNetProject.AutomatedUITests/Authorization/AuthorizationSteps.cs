using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests
{
    [Binding]
    public class AuthorizationSteps: IDisposable
    {
        private readonly IWebDriver _driver;
        public AuthorizationSteps()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();

        }

        [Given(@"I am on login page")]
        public void GivenIAmOnLoginPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/User/Account/Login");
        }
        
        [When(@"I enter  Username '(.*)' and Password '(.*)'")]
        public void WhenIEnterUsernameAndPassword(string username, string password)
        {
            var usernameBox = _driver.FindElement(By.Id("UserName"));
            var passwordBox = _driver.FindElement(By.Id("Password"));
            
            usernameBox.SendKeys(username);
            passwordBox.SendKeys(password);
           
        }
        
        [When(@"I press button with class ""(.*)""")]
        public void WhenIPressButtonWithClass(string p0)
        {
            var submitBtn = _driver.FindElement(By.ClassName(p0));
            submitBtn.Click();
        }
        
        [Then(@"main page will open")]
        public void ThenMainPageWillOpen()
        {
            Assert.AreEqual("http://localhost:5000/",_driver.Url);
        }
        
        [Then(@"I have possibility to select edit event menu")]
        public void ThenIHavePossibilityToSelectEditEventMenu()
        {
            var linkCount = _driver.FindElements(By.CssSelector(".navbar__navigation a[href='/Manager/EditEvents']")).Count;
            
            Assert.AreEqual(1, linkCount);
        }
        
        [Then(@"""(.*)"" name exist in header\.")]
        public void ThenNameExistInHeader_(string username)
        {
            var actualUser = _driver.FindElement(By.Id("userName-header")).Text;
            
            Assert.IsTrue(actualUser.Contains(username));
        }
        
        [Then(@"I have not possibility to select edit event menu")]
        public void ThenIHaveNotPossibilityToSelectEditEventMenu()
        {
            var linkCount = _driver.FindElements(By.CssSelector(".navbar__navigation a[href='/Manager/EditEvents']")).Count;
            
            Assert.AreEqual(0, linkCount);
        }
        
        [Then(@"Nothing happens")]
        public void ThenNothingHappens()
        {
            Assert.AreEqual("http://localhost:5000/User/Account/Login", _driver.Url);
        }

        [Then(@"Error Occurred")]
        public void ThenErrorOccurred()
        {
            var errorCount = _driver.FindElements(By.CssSelector(".validation-summary-errors li")).Count;
            
            Assert.IsTrue(errorCount > 0);
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
