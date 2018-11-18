using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightData.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing;
using FightDataProcessor.WebpageParsing.PicksPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestGridParser : TestDataLayer
    {
        [TestMethod]
        public void TestParseAnalyst()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            Webpage picksPage = entityFinder.WebpageFinder.GetPicksPages(exhibition).First();
            PicksPageParser picksPageParser = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml());

            List<RawAnalystPick> gridRowResults = picksPageParser.ParsePicksGrid();

            Assert.IsTrue(gridRowResults.First().Analyst == "Mike Bohn");
        }

        [TestMethod]
        public void TestParseFighters()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            Webpage picksPage = entityFinder.WebpageFinder.GetPicksPages(exhibition).First();
            PicksPageParser picksPageParser = new PicksPageParser(new HtmlPageParser(picksPage.Data).ParseHtml());

            List<RawAnalystPick> gridRowResults = picksPageParser.ParsePicksGrid().ToList();

            Assert.IsTrue(gridRowResults.First().Pick == "Rockhold");
        }
    }
}
