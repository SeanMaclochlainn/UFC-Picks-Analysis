using FightData.Domain.Builders;
using FightData.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightData.Domain.Test.Finders
{
    [TestClass]
    public class TestFightFinder : TestDataLayer
    {
        private FighterUpdater fighterUpdater;
        private FighterBuilder fighterBuilder;

        public TestFightFinder()
        {
            fighterUpdater = new FighterUpdater(context);
            fighterBuilder = new FighterBuilder(context);
        }

        [TestMethod]
        public void TestFindFightWhenDuplicateSurnames()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            Fighter dhiego = fighterBuilder.GenerateFighter("Dhiego Lima").Build();
            entityUpdater.FighterUpdater.Add(dhiego);
            Fighter dhiegoOpponent = fighterBuilder.GenerateFighter("xyz def").Build();
            entityUpdater.FighterUpdater.Add(dhiegoOpponent);
            Fighter juliana = fighterBuilder.GenerateFighter("Juliana Lima").Build();
            entityUpdater.FighterUpdater.Add(juliana);
            Fighter julianaOpponent = fighterBuilder.GenerateFighter("fsdf fda").Build();
            entityUpdater.FighterUpdater.Add(julianaOpponent);
            exhibition.Fights.Add(new Fight(context) { Winner = dhiego, Loser = dhiegoOpponent });
            exhibition.Fights.Add(new Fight(context) { Winner = juliana, Loser = julianaOpponent });

            Fight fight = entityFinder.FightFinder.FindFight(juliana, exhibition).Result;

            Assert.IsTrue(fight.Winner == juliana);

        }
    }
}
