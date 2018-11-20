using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFighter : TestDataLayer
    {
        [TestMethod]
        public void TestAddFighter()
        {
            int originalFighterCount = context.Fighters.Count();
            Fighter fighter = new Fighter(context);
            fighter.PopulateNames("fname lname");

            fighter.Add();

            Assert.IsTrue(context.Fighters.Count() == originalFighterCount + 1);
        }

        [TestMethod]
        public void TestSanitizeFullName()
        {
            Fighter fighter = Fighter.GenerateFighter("José Aldo (c)", context);

            Assert.IsTrue(fighter.FullName == "jose aldo");
        }
    }
}
