using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class LandingPage
    {
        private const string PageLink = "http://localhost:5000/";

        private const string EditEventsLinkSelector = ".navbar__navigation a[href='/Manager/EditEvent/EditEvents']";

        private const string UserNameTextFieldId = "userName-header";

        private readonly IWebDriver _driver;

        private readonly IReadOnlyCollection<IWebElement> _editEventsLink;

        private readonly IWebElement _userNameElement;

        private LandingPage(IWebDriver driver)
        {
            _driver = driver;
            if (!driver.Url.Equals(PageLink))
            {
                return;
            }

            _userNameElement = _driver.FindElement(By.Id(UserNameTextFieldId));
            _editEventsLink = _driver.FindElements(By.CssSelector(EditEventsLinkSelector));
        }

        public static LandingPage GetPage(IWebDriver webDriver)
        {
            return new LandingPage(webDriver);
        }

        public LandingPage GoToPage()
        {
            _driver.Navigate().GoToUrl(PageLink);
            return this;
        }

        public bool IsPageOpen()
        {
            return _driver.Url.Equals(PageLink);
        }

        public bool IsEditEventsLinkPresent()
        {
            return _editEventsLink.Any();
        }

        public bool CheckUserName(string userName)
        {
            return _userNameElement.Text.Contains(userName);
        }
    }
}
