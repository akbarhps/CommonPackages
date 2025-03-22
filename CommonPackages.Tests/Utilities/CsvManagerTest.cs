using System.Collections.Generic;
using System.Data;
using CommonPackages.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonPackages.Tests.Utilities
{
    [TestClass]
    public class CsvManagerTest
    {
        private const string FilePath = @"..\..\Temp\CsvManagerTest.csv";

        [TestMethod]
        public void WriteDataTableTest()
        {
            var data = new DataTable();
            data.Columns.Add("ID", typeof(string));
            data.Columns.Add("Name", typeof(string));
            data.Rows.Add("1", "Hello");
            data.Rows.Add("2", "World");

            CsvManager.Write(data, FilePath);
        }

        [TestMethod]
        public void WriteDictionaryTest()
        {
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>() { { "ID", "1" }, { "Name", "Hello" } },
                new Dictionary<string, string>() { { "ID", "2" }, { "Name", "World" } }
            };

            CsvManager.Write(data, FilePath);
        }

        [TestMethod]
        public void ReadTest()
        {
            var data = CsvManager.Read<CsvManagerTestModel>(FilePath);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count > 0);
        }
    }

    public class CsvManagerTestModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}