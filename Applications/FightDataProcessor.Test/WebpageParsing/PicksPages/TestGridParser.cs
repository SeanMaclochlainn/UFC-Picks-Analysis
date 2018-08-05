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
            PicksPageGridParser gridParser = new PicksPageGridParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().GetHtml());

            List<ParsedGridRow> gridRowResults = gridParser.ParseRows();

            Assert.IsTrue(gridRowResults.First().AnalystName == "Mike Bohn");
        }

        [TestMethod]
        public void TestParseFighters()
        {
            PicksPageGridParser gridParser = new PicksPageGridParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().GetHtml());

            List<ParsedGridRow> gridRowResults = gridParser.ParseRows().ToList();

            Assert.IsTrue(gridRowResults.First().FighterNames.First() == "Rockhold");
        }
    }
}
