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
            fightAdder = new FightAdder(entityGenerator.ExhibitionGenerator.GetParsedExhibition());
        }

        [TestMethod]
        public void TestAddingFight()
        {
            fightAdder.AddFight(new RawFightResult("test winner", "test loser"));

            Assert.IsTrue(context.Fighters.Count(f => f.FullName == "test winner") == 1);
        }
    }
}
