using System.Data.Entity.Migrations;
using CommonPackages.Utilities;
using MySql.Data.EntityFramework;

namespace CommonPackages.Migrations
{
    internal sealed class MySQLMigrationConfiguration : DbMigrationsConfiguration<MySQLDatabaseContext>
    {
        public MySQLMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;

            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
        }
    }
}