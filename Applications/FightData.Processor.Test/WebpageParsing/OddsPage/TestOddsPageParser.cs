using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing.OddsPage;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.Test.WebpageParsing.OddsPage
{
    [TestClass]
    public class TestOddsPageParser : TestDataLayer
    {
        private WebpageFinder webpageFinder;
        private ExhibitionFinder exhibitionFinder;

        public TestOddsPageParser()
        {
            webpageFinder = new WebpageFinder(context);
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestParseFighterOdds()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            Webpage oddsPage = webpageFinder.GetOddsPage(exhibition);
            OddsPageParser oddsPageParser = new OddsPageParser(new HtmlPageParser(oddsPage.Data).ParseHtml());

            List<RawFighterOdds> rawFighterOdds = oddsPageParser.Parse();

            Assert.IsTrue(rawFighterOdds.First().FighterName == "Luke Rockhold" && rawFighterOdds.First().Odds == "1.18");
        }
    }
}
