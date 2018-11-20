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

        [TestMethod]
        public void TestRemoveAccents()
        {
            string name = "José Aldo";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetFirstName() == "Jose");
        }

        [TestMethod]
        public void TestParseSurname()
        {
            string name = "Aldo";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetFullName() == "Aldo");
        }
    }
}
