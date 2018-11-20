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

            Assert.IsTrue(nameParser.GetLastName() == "aldo");
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

            Assert.IsTrue(nameParser.GetFirstName() == "jose");
        }

        [TestMethod]
        public void TestParseSurname()
        {
            string name = "Aldo";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetFullName() == "aldo");
        }

        [TestMethod]
        public void TestParseThreeNames()
        {
            string name = "Carlos Diego Ferreira";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetFullName() == "carlos diego ferreira");
        }

        [TestMethod]
        public void TestParseFourNames()
        {
            string name = "Antônio dos Santos Jr.";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetFullName() == "antonio dos santos jr");
        }

        [TestMethod]
        public void TestMakeNameLowercase()
        {
            string name = "José Aldo";

            NameParser nameParser = new NameParser(name);

            Assert.IsTrue(nameParser.GetFullName() == "jose aldo");
        }
    }
}
