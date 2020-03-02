using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class LandingPage : BasePage
    {
        private const string EditEventsLinkSelector = ".navbar__navigation a[href='/Manager/EditEvent/EditEvents']";

        private const string LoginLinkSelector = ".navbar__navigation a[href='/User/Account/Login']";

        private const string EventsLinkSelector = ".navbar__navigation a[href='/Events/Events']";

        private const string UserNameTextFieldId = "userName-header";

        private readonly string _pageLink;

        public LandingPage()
        {
            _pageLink = ConfigurationManager.AppSettings["rootUrl"];
        }

        private static IReadOnlyCollection<IWebElement> EditEventsLink =>
            findElementsBy(EditEventsLinkSelector, SelectorType.Css);

        private static IWebElement EventsLink =>
            findElementBy(EventsLinkSelector, SelectorType.Css);

        private static IWebElement LoginLink =>
            findElementBy(LoginLinkSelector, SelectorType.Css);

        private static IWebElement UserNameElement => findElementBy(UserNameTextFieldId, SelectorType.Id);

        public bool IsPageOpen()
        {
            return Driver.Url.Equals(_pageLink);
        }

        public static bool IsEditEventsLinkPresent()
        {
            return EditEventsLink.Any();
        }

        public static bool CheckUserName(string userName)
        {
            return UserNameElement.Text.Contains(userName);
        }

        public static LoginPage ClickLoginButton()
        {
            LoginLink.Click();
            return new LoginPage();
        }

        public static EventsPage ClickEventsLink()
        {
            EventsLink.Click();
            return new EventsPage();
        }

        public static EditEventsPage ClickEditEventsLink()
        {
            EditEventsLink.FirstOrDefault()?.Click();
            return new EditEventsPage();
        }
    }
}
