using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestPickAdder : TestDataLayer
    {
        [TestMethod]
        public void TestEmptyPickNotAdded()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetEmptyExhibition();
            PickAdder pickAdder = new PickAdder(exhibition);
            int originalNoPicks = exhibition.Fights.Select(f => f.Picks).Count();

            RawExhibitionPicks rawExhibitionPicks = new RawExhibitionPicks("", new List<string>() { "" });
            pickAdder.AddPicks(rawExhibitionPicks);

            Assert.IsTrue(originalNoPicks == exhibition.Fights.Select(f => f.Picks).Count());
        }
    }
}
