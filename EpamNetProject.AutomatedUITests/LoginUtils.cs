using EpamNetProject.AutomatedUITests.Pages;
using OpenQA.Selenium;

namespace EpamNetProject.AutomatedUITests
{
    public static class LoginUtils
    {
        private const string AdminLogin = "desout";

        private const string UserLogin = "desout1";

        private const string ManagerLogin = "desout";

        private const string AdminPassword = "Desoutside1";

        private const string UserPassword = "Desoutside1";

        private const string ManagerPassword = "Desoutside1";

        public static bool LoginAsAdmin(IWebDriver driver)
        {
            return LoginPage
                .GetPage(driver)
                .GoToPage()
                .TypeUserName(AdminLogin)
                .TypePassword(AdminPassword)
                .ClickLoginButton()
                .CheckUserName(AdminLogin);
        }

        public static bool LoginAsManager(IWebDriver driver)
        {
            return LoginPage
                .GetPage(driver)
                .GoToPage()
                .TypeUserName(ManagerLogin)
                .TypePassword(ManagerPassword)
                .ClickLoginButton()
                .CheckUserName(ManagerLogin);
        }

        public static bool LoginAsUser(IWebDriver driver)
        {
            return LoginPage
                .GetPage(driver)
                .GoToPage()
                .TypeUserName(UserLogin)
                .TypePassword(UserPassword)
                .ClickLoginButton()
                .CheckUserName(UserLogin);
        }
    }
}
