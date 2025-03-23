using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;
using CommonPackages.Utilities;

namespace CommonPackages.Migrations
{
    internal sealed class SQLiteMigrationConfiguration : DbMigrationsConfiguration<SQLiteDatabaseContext>
    {
        public SQLiteMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;

            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }
    }
}