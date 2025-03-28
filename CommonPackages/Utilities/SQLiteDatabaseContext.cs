// This file requires the following packages:
// - log4net (Logging)
// - EntityFramework (ORM)
// - System.Data.SQLite (SQLite)
// - System.Data.SQLite.EF6 (SQLite ORM)
// - System.Data.SQLite.EF6.Migrations (SQLite Migrations)
//
// Update the App.config or Web.config file (check the CommonPackages project App.config)
// look for the connectionStrings, entityFramework, DbProviderFactories
//
// Also you need to enable migration with:
// Right-click on the project -> Entity Framework -> Enable Migrations

using System.Data.Entity;
using CommonPackages.Models;
using log4net;

namespace CommonPackages.Utilities
{
    public class SQLiteDatabaseContext : DbContext
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SQLiteDatabaseContext));

        public SQLiteDatabaseContext() : base("name=SQLiteConnection")
        {
            Database.Log = s => Logger.Debug(s);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
    }
}