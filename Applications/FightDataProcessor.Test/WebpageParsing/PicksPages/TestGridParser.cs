using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing.PicksPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FightDataProcessor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestGridParser : TestDataLayer
    {
        [TestMethod]
        public void TestParseAnalyst()
        {
            GridParser gridParser = new GridParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().GetHtml());

            List<GridRowResult> gridRowResults = gridParser.ParseRows();

            Assert.IsTrue(gridRowResults.First(grr => grr.IsValidRow()).AnalystName == "Mike Bohn");
        }

        [TestMethod]
        public void TestParseFighters()
        {
            GridParser gridParser = new GridParser(entityGenerator.WebpageGenerator.GetPopulatedPicksPage().GetHtml());

            List<GridRowResult> gridRowResults = gridParser.ParseRows().Where(r=>r.IsValidRow()).ToList();

            Assert.IsTrue(gridRowResults.First().FighterNames.First() == "Rockhold");
        }
    }
}
