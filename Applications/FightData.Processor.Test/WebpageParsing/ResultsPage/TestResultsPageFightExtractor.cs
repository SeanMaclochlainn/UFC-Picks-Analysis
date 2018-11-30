﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.Domain.Entities;
using FightData.Domain.Test;
using System.Linq;
using FightDataProcessor.WebpageParsing;
using FightData.Domain.Updaters;
using FightData.Processor.WebpageParsing;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor : TestDataLayer
    {
        private ExhibitionUpdater exhibitionUpdater;
        private ExhibitionWebpagesParser exhibitionWebpagesParser;
        private RawEntitiesUpdater rawEntitiesUpdater;

        public TestResultsPageFightExtractor()
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

            RawExhibitionEntities rawExhibitionEntities = exhibitionWebpagesParser.ParseAllWebpages(exhibition);
            rawEntitiesUpdater.UpdateEntities(rawExhibitionEntities, exhibition);

            Assert.IsTrue(context.Fights.Count() == existingFights + 2);
        }
    }
}
