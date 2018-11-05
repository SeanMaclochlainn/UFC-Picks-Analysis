using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FightData.Processor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestRawPickEvaluator : TestDataLayer
    {
        private ExhibitionFinder exhibitionFinder;

        public TestRawPickEvaluator()
        {
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestInvalidPick()
        {
            RawPickEvaluator rawPickEvaluator = new RawPickEvaluator(context);
            RawAnalystPick invalidPick = new RawAnalystPick("test", "test");
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            rawPickEvaluator.EvaluatePicks(new List<RawAnalystPick>() { invalidPick }, exhibition);

            Assert.IsTrue(rawPickEvaluator.InvalidPicks.Count == 1);
        }
    }
}
