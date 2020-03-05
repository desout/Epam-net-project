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

        public static bool LoginAsAdmin()
        {
            return new LandingPage()
                .ClickLoginButton()
                .TypeUserName(AdminLogin)
                .TypePassword(AdminPassword)
                .ClickLoginButton()
                .CheckUserName(AdminLogin);
        }

        public static bool LoginAsManager()
        {
            return new LandingPage()
                .ClickLoginButton()
                .TypeUserName(ManagerLogin)
                .TypePassword(ManagerPassword)
                .ClickLoginButton()
                .CheckUserName(ManagerLogin);
        }

        public static bool LoginAsUser()
        {
            return new LandingPage()
                .ClickLoginButton()
                .TypeUserName(UserLogin)
                .TypePassword(UserPassword)
                .ClickLoginButton()
                .CheckUserName(UserLogin);
        }
    }
}
