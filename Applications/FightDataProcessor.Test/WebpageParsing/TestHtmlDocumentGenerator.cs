using FightData.TestData;
using FightData.TestData.EntityGenerators;
using FightDataProcessor.WebpageParsing;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightDataProcessor.Test.WebpageParsing
{
    [TestClass]
    public class TestHtmlDocumentGenerator
    {
        private WebpageGenerator webpageGenerator;

        public TestHtmlDocumentGenerator()
        {
            webpageGenerator = new WebpageGenerator(new TestDatabase().Context);
        }

        [TestMethod]
        public void GenerateHtmlDocument()
        {
            HtmlDocument htmlDocument = HtmlDocumentGenerator.FromWebpage(webpageGenerator.GetWebpage());

            Assert.IsNotNull(htmlDocument);
        }
    }
}
