using CommonPackages.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonPackages.Tests.Utilities
{
    [TestClass]
    public class PIServiceManagerTest
    {
        private const string ServiceName = "MySQL80";
        private readonly PIServiceManager _piServiceManager = new PIServiceManager();

        [TestMethod]
        public void FindServiceTest()
        {
            var service = _piServiceManager.Find(ServiceName);
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void RestartServiceUsingServiceObjectTest()
        {
            var service = _piServiceManager.Find(ServiceName);

            _piServiceManager.Stop(service);
            _piServiceManager.Start(service);
        }

        [TestMethod]
        public void RestartServiceUsingServiceNameTest()
        {
            _piServiceManager.Stop(ServiceName);
            _piServiceManager.Start(ServiceName);
        }
    }
}