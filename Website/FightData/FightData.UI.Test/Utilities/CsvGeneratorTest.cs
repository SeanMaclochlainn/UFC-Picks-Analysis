using FightData.UI.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.UI.Test.Utilities
{
    [TestClass]
    class CsvGeneratorTest
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
