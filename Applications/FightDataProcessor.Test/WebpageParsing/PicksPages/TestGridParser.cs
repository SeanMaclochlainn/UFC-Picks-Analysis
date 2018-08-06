using FightData.Domain;
using FightData.Domain.Test;
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
            PicksPageParser picksPageParser = new PicksPageParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().GetHtml());

            List<RawUfcEventPicks> gridRowResults = picksPageParser.ParsePicksGrid();

            Assert.IsTrue(gridRowResults.First().AnalystName == "Mike Bohn");
        }

        [TestMethod]
        public void TestParseFighters()
        {
            PicksPageParser picksPageParser = new PicksPageParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().GetHtml());

            List<RawUfcEventPicks> gridRowResults = picksPageParser.ParsePicksGrid().ToList();

            Assert.IsTrue(gridRowResults.First().FighterNames.First() == "Rockhold");
        }
    }
}
