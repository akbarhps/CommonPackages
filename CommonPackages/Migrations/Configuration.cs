using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;
using CommonPackages.Utilities;

namespace CommonPackages.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SQLiteDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;

            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }
    }
}