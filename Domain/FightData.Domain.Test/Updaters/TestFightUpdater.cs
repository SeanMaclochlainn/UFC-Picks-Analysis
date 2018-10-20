using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFightUpdater : TestDataLayer
    {
        private FightUpdater fightAdder;

        public TestFightUpdater()
        {
            fightAdder = new FightUpdater(entityGenerator.ExhibitionGenerator.GetParsedExhibition());
        }

        [TestMethod]
        public void TestAddingFight()
        {
            fightAdder.AddFight(new RawFightResult("test winner", "test loser"));

            Assert.IsTrue(context.Fighters.Count(f => f.FullName == "test winner") == 1);
        }
    }
}
