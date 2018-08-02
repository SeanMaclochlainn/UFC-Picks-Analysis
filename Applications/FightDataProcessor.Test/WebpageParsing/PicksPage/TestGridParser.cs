using FightData.Domain.Test;
using FightData.TestData.EntityGenerators;
using FightDataProcessor.WebpageParsing;
using FightDataProcessor.WebpageParsing.PicksPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.PicksPage
{
    [TestClass]
    public class TestGridParser : TestDataLayer
    {
        private WebpageGenerator webpageGenerator;

        public TestGridParser()
        {
            webpageGenerator = new WebpageGenerator(context);
        }

        [TestMethod]
        public void TestParseAnalyst()
        {
            GridParser gridParser = new GridParser(XDocumentGenerator.FromWebpage(webpageGenerator.GetPopulatedPicksPage()));

            List<GridRowResult> gridRowResults = gridParser.ParseRows();

            Assert.IsTrue(gridRowResults.First(grr => grr.IsValidRow()).AnalystName == "Mike Bohn");
        }

        [TestMethod]
        public void TestParseFighters()
        {
            GridParser gridParser = new GridParser(XDocumentGenerator.FromWebpage(webpageGenerator.GetPopulatedPicksPage()));

            List<GridRowResult> gridRowResults = gridParser.ParseRows().Where(r=>r.IsValidRow()).ToList();

            Assert.IsTrue(gridRowResults.First().FighterNames.First() == "Rockhold");
        }
    }
}
