using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightData.WebpageParsing.PicksPages;
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

            Assert.IsTrue(rawPickEvaluator.UnfoundPicks.Count == 1);
        }

        [TestMethod]
        public void TestBlankAnalyst()
        {
            RawPickEvaluator rawPickEvaluator = new RawPickEvaluator(context);
            RawAnalystPick blankAnalystPick = new RawAnalystPick("", "test");
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            rawPickEvaluator.EvaluatePicks(new List<RawAnalystPick>() { blankAnalystPick }, exhibition);

            Assert.IsTrue(rawPickEvaluator.ValidPicks.Count == 0 && rawPickEvaluator.UnfoundPicks.Count == 0);
        }
    }
}
