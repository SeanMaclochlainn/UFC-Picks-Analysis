using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
