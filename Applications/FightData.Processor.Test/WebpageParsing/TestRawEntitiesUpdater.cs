using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing;
using FightData.Processor.WebpageParsing.OddsPage;
using FightData.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.Test.WebpageParsing
{
    [TestClass]
    public class TestRawEntitiesUpdater : TestDataLayer
    {
        private ExhibitionUpdater exhibitionUpdater;
        private ExhibitionWebpagesParser exhibitionWebpagesParser;
        private RawEntitiesUpdater rawEntitiesUpdater;

        public TestRawEntitiesUpdater()
        {
            exhibitionUpdater = new ExhibitionUpdater(context);
            exhibitionWebpagesParser = new ExhibitionWebpagesParser(context);
            rawEntitiesUpdater = new RawEntitiesUpdater(context);
        }

        [TestMethod]
        public void TestExtractResults()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");
            int existingFights = context.Fights.Count();

            RawExhibitionData rawExhibitionData = exhibitionWebpagesParser.ParseAllWebpages(exhibition);
            rawEntitiesUpdater.UpdateEntities(rawExhibitionData, exhibition);

            Assert.IsTrue(context.Fights.Count() == existingFights + 2);
        }

        [TestMethod]
        public void TestUpdateDate()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");

            RawResultsPageData rawResultsPageData = new RawResultsPageData(new List<RawFightResult>(), "2014-10-25");
            RawExhibitionData rawExhibitionData = new RawExhibitionData(rawResultsPageData, new List<RawAnalystPick>(), new List<RawFighterOdds>());
            rawEntitiesUpdater.UpdateEntities(rawExhibitionData, exhibition);

            Assert.IsTrue(entityFinder.ExhibitionFinder.FindExhibition("UFC 179").Date.ToShortDateString() == "25/10/2014");
        }
    }
}
