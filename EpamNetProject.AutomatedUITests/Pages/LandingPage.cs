using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class LandingPage : BasePage
    {
        private readonly string _pageLink;

        private const string EditEventsLinkSelector = ".navbar__navigation a[href='/Manager/EditEvent/EditEvents']";
        
        private const string LoginLinkSelector = ".navbar__navigation a[href='/User/Account/Login']";
        
        private const string EventsLinkSelector = ".navbar__navigation a[href='/Events/Events']";

        private const string UserNameTextFieldId = "userName-header";

        private static IReadOnlyCollection<IWebElement> EditEventsLink =>
           findElementsBy(EditEventsLinkSelector,SelectorType.Css);

        private static IWebElement EventsLink =>
            findElementBy(EventsLinkSelector,SelectorType.Css);

        private static IWebElement LoginLink =>
            findElementBy(LoginLinkSelector,SelectorType.Css);

        private static IWebElement UserNameElement => findElementBy(UserNameTextFieldId, SelectorType.Id);

        public LandingPage()
        {
            _pageLink = ConfigurationManager.AppSettings["rootUrl"];
        }

        public bool IsPageOpen()
        {
            return Driver.Url.Equals(_pageLink);
        }

        public bool IsEditEventsLinkPresent()
        {
            return EditEventsLink.Any();
        }

        public bool CheckUserName(string userName)
        {
            return UserNameElement.Text.Contains(userName);
        }

        public LoginPage ClickLoginButton()
        {
           LoginLink.Click();
           return new LoginPage();
        }

        public EventsPage ClickEventsLink()
        {
            EventsLink.Click();
            return new EventsPage();
        }

        public EditEventsPage ClickEditEventsLink()
        {
            EditEventsLink.FirstOrDefault()?.Click();
            return new EditEventsPage();
        }
    }
}
