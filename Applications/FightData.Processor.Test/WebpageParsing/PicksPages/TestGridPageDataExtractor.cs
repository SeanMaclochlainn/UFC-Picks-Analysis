using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestGridPageDataExtractor : TestDataLayer
    {
        private ExhibitionWebpagesParser exhibitionWebpagesParser;
        private RawEntitiesUpdater rawEntitiesUpdater;

        public TestGridPageDataExtractor()
        {
            exhibitionWebpagesParser = new ExhibitionWebpagesParser(context);
            rawEntitiesUpdater = new RawEntitiesUpdater(context);
        }

        [TestMethod]
        public void TestExtractPick()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");

            RawExhibitionEntities rawExhibitionEntities = exhibitionWebpagesParser.ParseAllWebpages(exhibition);
            rawEntitiesUpdater.UpdateEntities(rawExhibitionEntities, exhibition);

            Pick mikeBohnsPick = context.Picks.First(p => p.Analyst.Name == "Mike Bohn" && p.Fight.Winner.LastName == "aldo");
            Assert.IsTrue(mikeBohnsPick.Fighter.LastName == "aldo");   
        }
    }
}
