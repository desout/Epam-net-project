using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests
{
    public class BaseTest : IDisposable
    {
        protected static IWebDriver Driver;
        const string TestDbName = "epam-net-project-db";
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $@"EXEC [epam-net-project-db].dbo.CreateSnapshot ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $@"
ALTER DATABASE [{TestDbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE [{TestDbName}] FROM DATABASE_SNAPSHOT = '{TestDbName}-Snapshot';
ALTER DATABASE [{TestDbName}] SET MULTI_USER;";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected BaseTest()
        {
            if (Driver == null)
            {
                Driver = new ChromeDriver();
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
                Driver.Manage().Window.Maximize();
                Driver.Navigate().GoToUrl("http://localhost:5000");
            }

        }

        public void Dispose()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }
    }
}
