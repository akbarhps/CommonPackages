using CommonPackages.Examples;
using log4net;

namespace CommonPackages
{
    internal class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            SQLiteDatabaseContextExample.Run(args);
            MySQLDatabaseContextExample.Run(args);
        }
    }
}