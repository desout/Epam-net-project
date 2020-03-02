using EpamNetProject.AutomatedUITests.Pages;

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
            return LandingPage.CheckUserName(AdminLogin);
        }

        public static bool LoginAsManager()
        {
            return LandingPage.CheckUserName(ManagerLogin);
        }

        public static bool LoginAsUser()
        {
            return LandingPage.CheckUserName(UserLogin);
        }
    }
}