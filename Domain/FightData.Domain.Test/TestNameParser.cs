using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void TestNoMiddleName()
        {
            string name = "first last";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetMiddleNames() == "");
        }
    }
}
