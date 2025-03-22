using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonPackages.Tests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void RunTest()
        {
            Assert.IsTrue(TimeSpan.Zero == new TimeSpan());
        }
    }
}