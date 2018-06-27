using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFight : TestDomain
    {
        [TestMethod]
        public void TestAddFight()
        {
            Fight fight = entityDataGenerator.GetFight();

            fight.Add();

            Assert.IsTrue(context.Fights.Count() == 1);
        }
    }
}
