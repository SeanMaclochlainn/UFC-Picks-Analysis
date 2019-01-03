using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing.PicksPages;
using FightData.Processor.WebpageParsing.Reconciliation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestReconciledEntitiesUpdater : TestDataLayer
    {
        private AnalystFinder analystFinder;
        private FighterFinder fighterFinder;
        private FightFinder fightFinder;
        private ExhibitionFinder exhibitionFinder;
        private ReconciledEntitiesUpdater reconciledEntitiesUpdater;

        public TestReconciledEntitiesUpdater()
        {
            analystFinder = new AnalystFinder(context);
            fighterFinder = new FighterFinder(context);
            fightFinder = new FightFinder(context);
            exhibitionFinder = new ExhibitionFinder(context);
            reconciledEntitiesUpdater = new ReconciledEntitiesUpdater(context);
        }

        [TestMethod]
        public void TestAddReconciledPick()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            Analyst analyst = analystFinder.FindAnalyst("Mike Bohn").Result;
            Fighter fighter = fighterFinder.FindFighter("Luke Rockhold").Result;
            Fight fight = fightFinder.FindFight(fighter, exhibition).Result;
            int existingPickCount = context.Picks.Count();

            ReconciledPick reconciledPick = new ReconciledPick(fighter.Id, analyst.Id);
            ReconciledEntities reconciledEntities = new ReconciledEntities(new List<ReconciledPick>() { reconciledPick }, new List<ReconciledOdd>());
            reconciledEntitiesUpdater.AddReconciledEntities(reconciledEntities, exhibition);

            Assert.IsTrue(context.Picks.Count() == existingPickCount + 1);
        }

        [TestMethod]
        public void TestAddReconciledOdd()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            Fighter fighter = fighterFinder.FindFighter("Luke Rockhold").Result;
            int existingOddsCount = context.Odds.Count();

            ReconciledOdd reconciledOdd = new ReconciledOdd(fighter.Id, "+180");

            ReconciledEntities reconciledEntities = new ReconciledEntities(new List<ReconciledPick>(), new List<ReconciledOdd>() { reconciledOdd });
            reconciledEntitiesUpdater.AddReconciledEntities(reconciledEntities, exhibition);

            Assert.IsTrue(context.Odds.Count() == existingOddsCount + 1);

        }
    }
}
