using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestFight : TestDataLayer
    {
        private FightUpdater fightUpdater;

        public TestFight()
        {
            fightUpdater = new FightUpdater(context);
        }

        [TestMethod]
        public void TestAddFight()
        {
            Fight fight = new Fight(context);
            fight.Winner = entityFinder.FighterFinder.FindFighter("Luke Rockhold").Result;
            fight.Loser = entityFinder.FighterFinder.FindFighter("Michael Bisping").Result;
            fight.Exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            int currentFightCount = context.Fights.Count();

            fightUpdater.AddFight(fight);

            Assert.IsTrue(context.Fights.Count() == currentFightCount + 1);
        }

    }
}
