using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing.OddsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Processor.Test.WebpageParsing.OddsPage
{
    [TestClass]
    public class TestRawFighterOddsEvaluator : TestDataLayer
    {
        [TestMethod]
        public void TestPositiveMoneylineOddConversion()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            RawFighterOddsEvaluator rawFighterOddsEvaluator = new RawFighterOddsEvaluator(context);

            RawFighterOdds rawFighterOdds = new RawFighterOdds("Luke Rockhold", "+180");
            EvaluatedOdds evaluatedOdds = rawFighterOddsEvaluator.EvaluateOdds(new List<RawFighterOdds>() { rawFighterOdds }, exhibition);

            Assert.IsTrue(evaluatedOdds.ValidOdds.First().Value == 2.80M);
        }

        [TestMethod]
        public void TestNegativeMoneylineOddConversion()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("FN 55");
            RawFighterOddsEvaluator rawFighterOddsEvaluator = new RawFighterOddsEvaluator(context);

            RawFighterOdds rawFighterOdds = new RawFighterOdds("Luke Rockhold", "-215");
            EvaluatedOdds evaluatedOdds = rawFighterOddsEvaluator.EvaluateOdds(new List<RawFighterOdds>() { rawFighterOdds }, exhibition);

            Assert.IsTrue(evaluatedOdds.ValidOdds.First().Value == 1.47M);
        }

        [TestMethod]
        public void TestUnfoundOdd()
        {
            RawFighterOddsEvaluator rawFighterOddsEvaluator = new RawFighterOddsEvaluator(context);
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");
            RawFighterOdds rawFighterOdds = new RawFighterOdds("xyz", "+100");
            List<RawFighterOdds> rawFighterOddsCollection = new List<RawFighterOdds>() { rawFighterOdds };

            EvaluatedOdds oddsEvaluatorResult = rawFighterOddsEvaluator.EvaluateOdds(rawFighterOddsCollection, exhibition);

            Assert.IsTrue(oddsEvaluatorResult.UnfoundOdds.Count == 1);
        }
    }
}
