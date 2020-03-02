using System;
using System.Configuration;
using System.Data.SqlClient;
using EpamNetProject.AutomatedUITests.Pages;
using TechTalk.SpecFlow;

namespace EpamNetProject.AutomatedUITests
{
    public class BaseTest : IDisposable
    {
        private const string TestDbName = "epam-net-project-db";

        public void Dispose()
        {
            BasePage.RemoveDriver();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"EXEC [epam-net-project-db].dbo.CreateSnapshot ";
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
    }
}
