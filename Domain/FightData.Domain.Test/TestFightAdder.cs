using FightDataProcessor.FightData.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFightAdder : TestDataLayer
    {
        private FightAdder fightAdder;

        public TestFightAdder()
        {
            fightAdder = new FightAdder(entityGenerator.UfcEventGenerator.GetPopulatedUfcEvent());
        }

        [TestMethod]
        public void TestAddingFight()
        {
            fightAdder.AddFight("test winner", "test loser");

            Assert.IsTrue(context.Fighters.Count(f => f.FullName == "test winner") == 1);
        }
    }
}
