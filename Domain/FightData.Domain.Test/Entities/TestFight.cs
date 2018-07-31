using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFight : TestDomain
    {
        [TestMethod]
        public void TestAddFight()
        {
            Fight fight = entityDataGenerator.GetEmptyFight();
            fight.UfcEvent = entityDataGenerator.GetEmptyUfcEvent();
            int currentFightCount = context.Fights.Count();

            fight.Add();

            Assert.IsTrue(context.Fights.Count() == currentFightCount + 1);
        }

        [TestMethod]
        public void TestGetFighters()
        {
            Fight fight = entityDataGenerator.GetFight();

            List<Fighter> fighters = fight.GetFighters();

            Assert.IsTrue(fighters.Count == 2);
        }
    }
}
