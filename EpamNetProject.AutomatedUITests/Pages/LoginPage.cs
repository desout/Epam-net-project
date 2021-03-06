using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class LoginPage: BasePage
    {
        private const string ValidationSummarySelector = ".validation-summary-errors li, .field-validation-error";

        private readonly string _pageLink;

        private const string UserNameTextFieldId = "UserName";

        private const string PasswordTextFieldId = "Password";

        private const string LoginButtonClassname = "button__submit";

        private static IWebElement LoginButton => FindElementByClassName(LoginButtonClassname);

        private static IWebElement PasswordTextField => FindElementById(PasswordTextFieldId);

        private static IWebElement UserNameTextField => FindElementById(UserNameTextFieldId);

        private static IReadOnlyCollection<IWebElement> ValidationErrors => FindElementsByCss(ValidationSummarySelector);

        public LoginPage()
        {
            _pageLink = $"{ConfigurationManager.AppSettings["rootUrl"]}User/Account/Login";
        }

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

        public LandingPage ClickLoginButton()
        {
            LoginButton.Click();
            return new LandingPage();
        }

        public bool IsPageOpen()
        {
            return Driver.Url.Equals(_pageLink);
        }

        public bool IsErrorOccured()
        {
            return ValidationErrors.Any();
        }
    }
}
