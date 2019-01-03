using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightData.Domain.Test.Finders
{
    [TestClass]
    public class TestFightFinder : TestDataLayer
    {
        private FighterUpdater fighterUpdater;

        public TestFightFinder()
        {
            fighterUpdater = new FighterUpdater(context);
        }

        [TestMethod]
        public void TestFindFightWhenDuplicateSurnames()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            Fighter dhiego = Fighter.GenerateFighter("Dhiego Lima", context);
            dhiego.Add();
            Fighter dhiegoOpponent = Fighter.GenerateFighter("xyz def", context);
            dhiegoOpponent.Add();
            Fighter juliana = Fighter.GenerateFighter("Juliana Lima", context);
            juliana.Add();
            Fighter julianaOpponent = Fighter.GenerateFighter("fsdf fda", context);
            julianaOpponent.Add();
            exhibition.Fights.Add(new Fight(context) { Winner = dhiego, Loser = dhiegoOpponent });
            exhibition.Fights.Add(new Fight(context) { Winner = juliana, Loser = julianaOpponent });

            Fight fight = entityFinder.FightFinder.FindFight(juliana, exhibition).Result;

            Assert.IsTrue(fight.Winner == juliana);

        }
    }
}
