using FightData.Domain.Test;
using FightData.TestData;
using FightData.TestData.EntityGenerators;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace FightDataProcessor.Test.WebpageParsing
{
    [TestClass]
    public class TestHtmlDocumentGenerator : TestDataLayer
    {
        private WebpageGenerator webpageGenerator;

        public TestHtmlDocumentGenerator()
        {
            webpageGenerator = new WebpageGenerator(context);
        }

        [TestMethod]
        public void GenerateHtmlDocument()
        {
            XDocument htmlDocument = XDocumentGenerator.FromWebpage(webpageGenerator.GetPopulatedResultsPage());

            Assert.IsNotNull(htmlDocument);
        }
    }
}
