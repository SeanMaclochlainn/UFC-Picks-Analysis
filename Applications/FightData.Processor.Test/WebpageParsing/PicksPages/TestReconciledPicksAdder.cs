using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing.PicksPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestReconciledPicksAdder : TestDataLayer
    {
        private AnalystFinder analystFinder;
        private FighterFinder fighterFinder;
        private FightFinder fightFinder;
        private ExhibitionFinder exhibitionFinder;

        public TestReconciledPicksAdder()
        {
            analystFinder = new AnalystFinder(context);
            fighterFinder = new FighterFinder(context);
            fightFinder = new FightFinder(context);
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestAddReconciledPick()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            Analyst analyst = analystFinder.FindAnalyst("Mike Bohn").Result;
            Fighter fighter = fighterFinder.FindFighter("Luke Rockhold").Result;
            Fight fight = fightFinder.FindFight(fighter, exhibition).Result;
            ReconciledPicksAdder reconciledPicksAdder = new ReconciledPicksAdder(context);
            int existingPickCount = context.Picks.Count();

            ReconciledPick reconciledPick = new ReconciledPick();
            reconciledPick.CorrectAnalystId = analyst.Id;
            reconciledPick.CorrectFighterPickId = fighter.Id;
            reconciledPick.Cancelled = false;
            reconciledPicksAdder.AddReconciledPicks(new List<ReconciledPick>() { reconciledPick }, exhibition);

            Assert.IsTrue(context.Picks.Count() == existingPickCount + 1);
        }
    }
}
