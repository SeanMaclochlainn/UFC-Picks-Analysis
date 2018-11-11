using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFight : TestDataLayer
    {
        [TestMethod]
        public void TestAddFight()
        {
            Fight fight = entityGenerator.FightGenerator.GetEmptyFight();
            fight.Exhibition = entityGenerator.ExhibitionGenerator.GetEmptyExhibition();
            int currentFightCount = context.Fights.Count();

            fight.Add();

            Assert.IsTrue(context.Fights.Count() == currentFightCount + 1);
        }

    }
}
