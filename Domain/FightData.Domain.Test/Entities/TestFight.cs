using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

        [TestMethod]
        public void TestGetFighters()
        {
            Fight fight = entityGenerator.FightGenerator.GetPopulatedFight();

            List<Fighter> fighters = fight.GetFighters();

            Assert.IsTrue(fighters.Count == 2);
        }
    }
}
