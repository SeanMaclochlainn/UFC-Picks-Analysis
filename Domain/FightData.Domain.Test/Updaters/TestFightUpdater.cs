using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFightUpdater : TestDataLayer
    {
        private FightUpdater fightUpdater;
        private ExhibitionFinder exhibitionFinder;

        public TestFightUpdater()
        {
            fightUpdater = new FightUpdater(context);
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestAddingFights()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            int originalFightCount = exhibition.Fights.Count();
            fightUpdater.AddFights(new List<RawFightResult>() { new RawFightResult("test winner", "test loser") }, exhibition);

            Assert.IsTrue(exhibition.Fights.Count() == originalFightCount + 1);
        }
    }
}
