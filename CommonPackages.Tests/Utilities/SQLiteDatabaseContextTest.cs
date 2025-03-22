using System;
using System.Linq;
using CommonPackages.Models;
using CommonPackages.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonPackages.Tests.Utilities
{
    [TestClass]
    public class SQLiteDatabaseContextTest
    {
        [TestMethod]
        public void CreateUsersTest()
        {
            using (var context = new SQLiteDatabaseContext())
            {
                var user = context.Users.Add(new User { Name = "Hello" });
                context.SaveChanges();

                Assert.IsNotNull(user);
                Assert.Equals(user.Name, "Hello");
            }
        }

        [TestMethod]
        public void FindAllUsersTest()
        {
            using (var context = new SQLiteDatabaseContext())
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.ID}: {user.Name}");
                }
            }
        }
    }
}