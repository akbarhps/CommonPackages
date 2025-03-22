using System.Data.Entity;
using CommonPackages.Models;
using log4net;

namespace CommonPackages.Utilities
{
    public class SQLiteDatabaseContext : DbContext
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SQLiteDatabaseContext));

        public SQLiteDatabaseContext() : base("name=ProdSQLiteConnection")
        {
            Database.Log = s => Logger.Debug(s);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
    }
}