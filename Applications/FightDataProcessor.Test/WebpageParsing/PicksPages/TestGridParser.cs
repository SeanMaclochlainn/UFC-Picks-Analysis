using FightData.Domain;
using FightData.Domain.Test;
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
            PicksPageParser picksPageParser = new PicksPageParser(new HtmlPageParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().Data).ParseHtml());

            List<RawExhibitionPicks> gridRowResults = picksPageParser.ParsePicksGrid();

            Assert.IsTrue(gridRowResults.First().AnalystName == "Mike Bohn");
        }

        [TestMethod]
        public void TestParseFighters()
        {
            PicksPageParser picksPageParser = new PicksPageParser(new HtmlPageParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().Data).ParseHtml());

            List<RawExhibitionPicks> gridRowResults = picksPageParser.ParsePicksGrid().ToList();

            Assert.IsTrue(gridRowResults.First().FighterNames.First() == "Rockhold");
        }
    }
}
