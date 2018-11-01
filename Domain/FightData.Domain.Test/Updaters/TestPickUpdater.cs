using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestPickAdder : TestDataLayer
    {
        private ExhibitionFinder exhibitionFinder;

        public TestPickAdder()
        {
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestEmptyPickNotAdded()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            PickUpdater pickAdder = new PickUpdater(exhibition);
            int originalNoPicks = exhibition.Fights.Select(f => f.Picks).Count();

            List<RawAnalystsPicks> rawAnalystPicks = new List<RawAnalystsPicks>() { new RawAnalystsPicks("", new List<string>() { "" }) };
            pickAdder.AddPicks(rawAnalystPicks);

            Assert.IsTrue(originalNoPicks == exhibition.Fights.Select(f => f.Picks).Count());
        }
    }
}
