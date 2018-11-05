using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestPickUpdater : TestDataLayer
    {
        private ExhibitionFinder exhibitionFinder;

        public TestPickUpdater()
        {
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestEmptyPickNotAdded()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            PickUpdater pickUpdater = new PickUpdater(context);
            int originalNoPicks = exhibition.Fights.Select(f => f.Picks).Count();

            pickUpdater.AddPick(new Pick(context) { Analyst = null, Fight = exhibition.Fights.First(), Fighter = null });

            Assert.IsTrue(originalNoPicks == exhibition.Fights.Select(f => f.Picks).Count());
        }
    }
}
