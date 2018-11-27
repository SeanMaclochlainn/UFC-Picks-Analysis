using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing.OddsPage;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FightData.Processor.Test.WebpageParsing.OddsPage
{
    [TestClass]
    public class TestRawFighterOddsEvaluator : TestDataLayer
    {
        [TestMethod]
        public void TestPositiveMoneylineOddConversion()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");

            new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();

            Odd odd = entityFinder.OddsFinder.FindFighterOdd(entityFinder.FighterFinder.FindFighter("Chad Mendes").Result, exhibition).Result;
            Assert.IsTrue(odd.Value == 2.80M);
        }

        [TestMethod]
        public void TestNegativeMoneylineOddConversion()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");

            new ExhibitionDataExtractor(exhibition).ExtractAllWebpages();

            Odd odd = entityFinder.OddsFinder.FindFighterOdd(entityFinder.FighterFinder.FindFighter("Jose Aldo").Result, exhibition).Result;
            Assert.IsTrue(odd.Value == 1.47M);
        }

        [TestMethod]
        public void TestUnfoundOdd()
        {
            RawFighterOddsEvaluator rawFighterOddsEvaluator = new RawFighterOddsEvaluator(context);
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");
            RawFighterOdds rawFighterOdds = new RawFighterOdds("xyz", "+100");
            List<RawFighterOdds> rawFighterOddsCollection = new List<RawFighterOdds>() { rawFighterOdds };

            OddsEvaluatorResult oddsEvaluatorResult = rawFighterOddsEvaluator.GetOddEntities(rawFighterOddsCollection, exhibition);

            Assert.IsTrue(oddsEvaluatorResult.UnfoundOdds.Count == 1);
        }
    }
}
