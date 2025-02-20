using System;
using System.IO;
using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TestTemplate14.Migrations
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var connectionString = string.Empty;
            var dbUser = string.Empty;
            var dbPassword = string.Empty;
            var scriptsPath = string.Empty;
            var sqlUsersGroupName = string.Empty;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Production";
            Console.WriteLine($"Environment: {env}.");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            InitializeParameters();
            var connectionStringBuilderTestTemplate14 = new SqlConnectionStringBuilder(connectionString);
            if (env.Equals("Development"))
            {
                connectionStringBuilderTestTemplate14.UserID = dbUser;
                connectionStringBuilderTestTemplate14.Password = dbPassword;
            }
            else
            {
                connectionStringBuilderTestTemplate14.UserID = dbUser;
                connectionStringBuilderTestTemplate14.Password = dbPassword;
                connectionStringBuilderTestTemplate14.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
            }
            var upgraderTestTemplate14 =
                DeployChanges.To
                    .SqlDatabase(connectionStringBuilderTestTemplate14.ConnectionString)
                    .WithVariable("SqlUsersGroupNameVariable", sqlUsersGroupName)    // This is necessary to perform template variable replacement in the scripts.
                    .WithScriptsFromFileSystem(
                        !string.IsNullOrWhiteSpace(scriptsPath)
                                ? Path.Combine(scriptsPath, "TestTemplate14Scripts")
                            : Path.Combine(Environment.CurrentDirectory, "TestTemplate14Scripts"))
                    .LogToConsole()
                    .Build();
            Console.WriteLine($"Now upgrading TestTemplate14.");
            if (env == "Development")
            {
                upgraderTestTemplate14.MarkAsExecuted("0000_AzureSqlContainedUser.sql");
            }
            var resultTestTemplate14 = upgraderTestTemplate14.PerformUpgrade();

            if (!resultTestTemplate14.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TestTemplate14 upgrade error: {resultTestTemplate14.Error}");
                Console.ResetColor();
                return -1;
            }

            // Uncomment the below sections if you also have an Identity Server project in the solution.
            /*
            var connectionStringTestTemplate14Identity = string.IsNullOrWhiteSpace(args.FirstOrDefault())
                ? config["ConnectionStrings:TestTemplate14IdentityDb"]
                : args.FirstOrDefault();

            var upgraderTestTemplate14Identity =
                DeployChanges.To
                    .SqlDatabase(connectionStringTestTemplate14Identity)
                    .WithScriptsFromFileSystem(
                        scriptsPath != null
                            ? Path.Combine(scriptsPath, "TestTemplate14IdentityScripts")
                            : Path.Combine(Environment.CurrentDirectory, "TestTemplate14IdentityScripts"))
                    .LogToConsole()
                    .Build();
            Console.WriteLine($"Now upgrading TestTemplate14 Identity.");
            if (env != "Development")
            {
                upgraderTestTemplate14Identity.MarkAsExecuted("0004_InitialData.sql");
                Console.WriteLine($"Skipping 0004_InitialData.sql since we are not in Development environment.");
                upgraderTestTemplate14Identity.MarkAsExecuted("0005_Initial_Configuration_Data.sql");
                Console.WriteLine($"Skipping 0005_Initial_Configuration_Data.sql since we are not in Development environment.");
            }
            var resultTestTemplate14Identity = upgraderTestTemplate14Identity.PerformUpgrade();

            if (!resultTestTemplate14Identity.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TestTemplate14 Identity upgrade error: {resultTestTemplate14Identity.Error}");
                Console.ResetColor();
                return -1;
            }
            */

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;

            void InitializeParameters()
            {
                // Local database, populated from .env file.
                if (args.Length == 0)
                {
                    connectionString = config["TestTemplate14Db_Migrations_Connection"];
                    dbUser = config["DbUser"];
                    dbPassword = config["DbPassword"];
                }

                // Deployed database
                else if (args.Length == 5)
                {
                    connectionString = args[0];
                    dbUser = args[1];
                    dbPassword = args[2];
                    scriptsPath = args[3];
                    sqlUsersGroupName = args[4];
                }
            }
        }
    }
}
