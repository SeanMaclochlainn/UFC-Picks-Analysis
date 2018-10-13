using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace FightDataProcessor.Test
{
    [TestClass]
    public class HtmlPageParserTest : TestDataLayer
    {
        [TestMethod]
        public void TestParsePageContainingRaquo()
        {
            HtmlPageParser htmlPageParser = new HtmlPageParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().Data);

            HtmlDocument result = htmlPageParser.ParseHtml();

            Assert.IsTrue(result != null);
        }
    }
}
