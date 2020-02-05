using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        private readonly IWebElement _userNameTextField;

        private readonly IWebElement _passwordTextField;

        private readonly IWebElement _loginButton;

        private readonly IReadOnlyCollection<IWebElement> _validationErrors;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            if (!driver.Url.Equals(PageLink))
            {
                return;
            }

            _userNameTextField = _driver.FindElement(By.Id(UserNameTextFieldId));
            _passwordTextField = _driver.FindElement(By.Id(PasswordTextFieldId));
            _loginButton = _driver.FindElement(By.ClassName(LoginButtonClassname));
            _validationErrors = _driver.FindElements(By.CssSelector(ValidationSummarySelector));
        }

        public static LoginPage GetPage(IWebDriver webDriver)
        {
            return new LoginPage(webDriver);
        }

        public LoginPage GoToPage()
        {
            _driver.Navigate().GoToUrl(PageLink);
            return new LoginPage(_driver);
        }

        public LoginPage TypeUserName(string userName)
        {
            _userNameTextField.SendKeys(userName);
            return this;
        }

        public LoginPage TypePassword(string password)
        {
            _passwordTextField.SendKeys(password);

            return this;
        }

        public LandingPage ClickLoginButton()
        {
            _loginButton.Click();
            return LandingPage.GetPage(_driver);
        }

        public bool IsPageOpen()
        {
            return _driver.Url.Equals(PageLink);
        }

        public bool IsErrorOccured()
        {
            return _validationErrors.Any();
        }

        private const string ValidationSummarySelector = ".validation-summary-errors li, .field-validation-error";

        private const string PageLink = "http://localhost:5000/User/Account/Login";

        private const string UserNameTextFieldId = "UserName";

        private const string PasswordTextFieldId = "Password";

        private const string LoginButtonClassname = "button__submit";
    }
}
