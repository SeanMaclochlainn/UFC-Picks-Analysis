using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFightUpdater : TestDataLayer
    {
        private FightUpdater fightUpdater;

        public TestFightUpdater()
        {
            fightUpdater = new FightUpdater(context);
        }

        [TestMethod]
        public void TestAddingFights()
        {
            fightUpdater.AddFights(new List<RawFightResult>() { new RawFightResult("test winner", "test loser") }, entityGenerator.ExhibitionGenerator.GetParsedExhibition());

            Assert.IsTrue(context.Fighters.Count(f => f.FullName == "test winner") == 1);
        }
    }
}
