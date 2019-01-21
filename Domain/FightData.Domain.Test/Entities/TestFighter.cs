using FightData.Domain.Builders;
using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFighter : TestDataLayer
    {
        private FighterBuilder fighterBuilder;

        public TestFighter()
        {
            fighterBuilder = new FighterBuilder(context);
        }

        [TestMethod]
        public void TestAddFighter()
        {
            int originalFighterCount = context.Fighters.Count();
            Fighter fighter = fighterBuilder.GenerateFighter("fname lname").Build();

            entityUpdater.FighterUpdater.Add(fighter);

            Assert.IsTrue(context.Fighters.Count() == originalFighterCount + 1);
        }

        [TestMethod]
        public void TestSanitizeFullName()
        {
            Fighter fighter = fighterBuilder.GenerateFighter("José Aldo (c)").Build();

            Assert.IsTrue(fighter.FullName == "jose aldo");
        }
    }
}
