using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class LoginPage : BasePage
    {
        private const string ValidationSummarySelector = ".validation-summary-errors li, .field-validation-error";

        private const string UserNameTextFieldId = "UserName";

        private const string PasswordTextFieldId = "Password";

        private const string LoginButtonClassname = "button__submit";

        private readonly string _pageLink;

        public LoginPage()
        {
            _pageLink = $"{ConfigurationManager.AppSettings["rootUrl"]}User/Account/Login";
        }

        private static IWebElement LoginButton => findElementBy(LoginButtonClassname, SelectorType.ClassName);

        private static IWebElement PasswordTextField => findElementBy(PasswordTextFieldId, SelectorType.Id);

        private static IWebElement UserNameTextField => findElementBy(UserNameTextFieldId, SelectorType.Id);

        private static IReadOnlyCollection<IWebElement> ValidationErrors =>
            findElementsBy(ValidationSummarySelector, SelectorType.Css);

        public LoginPage TypeUserName(string userName)
        {
            UserNameTextField.SendKeys(userName);
            return this;
        }

        public LoginPage TypePassword(string password)
        {
            PasswordTextField.SendKeys(password);

            return this;
        }

        public static LandingPage ClickLoginButton()
        {
            LoginButton.Click();
            return new LandingPage();
        }

        public bool IsPageOpen()
        {
            return Driver.Url.Equals(_pageLink);
        }

        public static bool IsErrorOccured()
        {
            return ValidationErrors.Any();
        }
    }
}
