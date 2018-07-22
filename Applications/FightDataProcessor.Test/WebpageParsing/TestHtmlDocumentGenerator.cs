using FightData.TestData;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightDataProcessor.Test.WebpageParsing
{
    [TestClass]
    public class TestHtmlDocumentGenerator
    {
        private TestEntityGenerator entityDataGenerator;

        public TestHtmlDocumentGenerator()
        {
            entityDataGenerator = new TestEntityGenerator(new TestDatabase().Context);
        }

        [TestMethod]
        public void GenerateHtmlDocument()
        {
            HtmlDocumentGenerator htmlDocumentGenerator = HtmlDocumentGenerator.FromWebpage(entityDataGenerator.GetWebpage());

            Assert.IsNotNull(htmlDocumentGenerator.HtmlDocument);
        }
    }
}
