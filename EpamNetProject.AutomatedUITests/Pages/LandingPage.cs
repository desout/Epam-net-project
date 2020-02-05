using System.Linq;
using OpenQA.Selenium;
namespace EpamNetProject.AutomatedUITests.Pages
{
    public class LandingPage
    {
        private IWebDriver _driver;
        private readonly IWebElement _userNameElement;

        public LandingPage(IWebDriver driver)
        {
            _driver = driver;
            if (driver.Url.Equals("http://localhost:5000/"))
            {
                _userNameElement = _driver.FindElement(By.Id(UserNameTextFieldId));
            }
        }
        
        public static LandingPage GetPage(IWebDriver webDriver)
        {
            return new LandingPage(webDriver);
        }

        public LandingPage GoToPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/User/Account/Login");
            return this;
        }

        public bool IsPageOpen()
        {
            return _driver.Url.Equals("http://localhost:5000/");
        }

        public bool isEditEventsLinkPresent()
        {
            return _driver.FindElements(By.CssSelector(".navbar__navigation a[href='/Manager/EditEvent/EditEvents']")).Any();
        }
        public bool CheckUserName(string userName)
        {
            return _userNameElement.Text.Contains(userName);
        }
        
        private const string UserNameTextFieldId = "userName-header";
    }
}
