using FightData.TestData;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightDataProcessor.Test.WebpageParsing
{
    [TestClass]
    public class TestHtmlDocumentGenerator
    {
        private EntityDataGenerator entityDataGenerator;

        public TestHtmlDocumentGenerator()
        {
            entityDataGenerator = new EntityDataGenerator(new Database().Context);
        }

        [TestMethod]
        public void GenerateHtmlDocument()
        {
            HtmlDocumentGenerator htmlDocumentGenerator = HtmlDocumentGenerator.FromWebpage(entityDataGenerator.GetWebpage());

            Assert.IsNotNull(htmlDocumentGenerator.HtmlDocument);
        }
    }
}
