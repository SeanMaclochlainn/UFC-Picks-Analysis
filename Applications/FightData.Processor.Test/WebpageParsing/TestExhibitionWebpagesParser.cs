using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing
{
    [TestClass]
    public class TestExhibitionWebpagesParser : TestDataLayer
    {
        private WebpageFinder webpageFinder;
        private ExhibitionWebpagesParser exhibitionWebpagesParser;
        private RawEntitiesUpdater rawEntitiesUpdater;

        public TestExhibitionWebpagesParser()
        {
            webpageFinder = new WebpageFinder(context);
            exhibitionWebpagesParser = new ExhibitionWebpagesParser(context);
            rawEntitiesUpdater = new RawEntitiesUpdater(context);
        }

        [TestMethod]
        public void TestSkipParsedPage()
        {
            int originalNoFights = context.Fights.Count();
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");

            RawExhibitionData rawExhibitionData = exhibitionWebpagesParser.ParseAllWebpages(exhibition);
            rawEntitiesUpdater.UpdateEntities(rawExhibitionData, exhibition);

            Assert.IsTrue(originalNoFights == context.Fights.Count());
        }

        [TestMethod]
        public void TestWebpageIsMarkedAsParsed()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");

            exhibitionWebpagesParser.ParseAllWebpages(exhibition);

            Assert.IsTrue(webpageFinder.GetResultsPage(exhibition).Parsed == true);
        }

        [TestMethod]
        public void TestExtractOddsPage()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");
            int originalOddsCount = context.Odds.Count();

            RawExhibitionData rawWebpageEntities = exhibitionWebpagesParser.ParseAllWebpages(exhibition);
            rawEntitiesUpdater.UpdateEntities(rawWebpageEntities, exhibition);

            Assert.IsTrue(context.Odds.Count() == originalOddsCount + 2);
        }
    }
}
