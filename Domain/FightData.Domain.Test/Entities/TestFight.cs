﻿using FightData.Domain.Entities;
using FightData.TestData.EntityGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFight : TestDomain
    {
        private FightGenerator fightGenerator;
        private UfcEventGenerator ufcEventGenerator;

        public TestFight()
        {
            fightGenerator = new FightGenerator(context);
            ufcEventGenerator = new UfcEventGenerator(context);
        }

        [TestMethod]
        public void TestAddFight()
        {
            Fight fight = fightGenerator.GetEmptyFight();
            fight.UfcEvent = ufcEventGenerator.GetEmptyUfcEvent();
            int currentFightCount = context.Fights.Count();

            fight.Add();

            Assert.IsTrue(context.Fights.Count() == currentFightCount + 1);
        }

        [TestMethod]
        public void TestGetFighters()
        {
            Fight fight = fightGenerator.GetFight();

            List<Fighter> fighters = fight.GetFighters();

            Assert.IsTrue(fighters.Count == 2);
        }
    }
}
