using System.Data.Entity;
using System.Linq;
using CommonPackages.Migrations;
using CommonPackages.Utilities;
using log4net;
using Newtonsoft.Json;

namespace CommonPackages
{
    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SQLiteDatabaseContext, Configuration>());

            var context = new SQLiteDatabaseContext();
            context.Database.Initialize(false);

            var users = context.Users
                .Include(u => u.Address)
                .OrderByDescending(u => u.ID)
                .Take(1)
                .ToList();
            users.ForEach(user =>
            {
                Logger.Info(JsonConvert.SerializeObject(user));
                Logger.Info(user.Address.Count);
            });
        }
    }
}