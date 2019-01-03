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
            Fight fight = new Fight(context);
            fight.Winner = entityFinder.FighterFinder.FindFighter("Luke Rockhold").Result;
            fight.Loser = entityFinder.FighterFinder.FindFighter("Michael Bisping").Result;
            fight.Exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            int currentFightCount = context.Fights.Count();

            fight.Add();

            Assert.IsTrue(context.Fights.Count() == currentFightCount + 1);
        }

    }
}
