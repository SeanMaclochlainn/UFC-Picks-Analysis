using FightData.TestData;
using FightDataProcessor.WebpageParsing;
using HtmlAgilityPack;
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
            HtmlDocument htmlDocument = HtmlDocumentGenerator.FromWebpage(entityDataGenerator.GetWebpage());

            Assert.IsNotNull(htmlDocument);
        }
    }
}
