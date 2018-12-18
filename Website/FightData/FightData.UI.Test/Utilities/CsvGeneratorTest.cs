using FightData.UI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FightData.UI.Test.Utilities
{
    [TestClass]
    public class CsvGeneratorTest
    {
        [TestMethod]
        public void TestAddRow()
        {
            CsvGenerator csvGenerator = new CsvGenerator();

            csvGenerator.AddRow(new List<string>() { "test1", "test2", "test3" });

            Assert.IsTrue(csvGenerator.GetContent() == "test1,test2,test3\r\n");
        }
    }
}
