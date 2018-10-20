using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestNameParser
    {
        [TestMethod]
        public void TestParseChampionName()
        {
            string name = "José Aldo(c)";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetLastName() == "Aldo");
        }
    }
}
