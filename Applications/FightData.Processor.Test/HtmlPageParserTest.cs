using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightDataProcessor.Test
{
    [TestClass]
    public class HtmlPageParserTest : TestDataLayer
    {
        [TestMethod]
        public void TestParsePageContainingRaquo()
        {
            Webpage picksPage = entityFinder.WebpageFinder.GetPicksPages(entityFinder.ExhibitionFinder.FindExhibition("FN 55")).First();
            HtmlPageParser htmlPageParser = new HtmlPageParser(picksPage.Data);

            HtmlDocument result = htmlPageParser.ParseHtml();

            Assert.IsTrue(result != null);
        }
    }
}
