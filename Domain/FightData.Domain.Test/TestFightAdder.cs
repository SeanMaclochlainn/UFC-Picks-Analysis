using FightData.TestData.EntityGenerators;
using FightDataProcessor.FightData.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFightAdder : TestDomain
    {
        private FightAdder fightAdder;
        private UfcEventGenerator ufcEventGenerator;

        public TestFightAdder()
        {
            ufcEventGenerator = new UfcEventGenerator(context);
            fightAdder = new FightAdder(ufcEventGenerator.GetPopulatedUfcEvent(), context);
        }

        [TestMethod]
        public void TestAddingFight()
        {
            fightAdder.AddFight("test winner", "test loser");

            Assert.IsTrue(context.Fighters.Count(f => f.FullName == "test winner") == 1);
        }
    }
}
