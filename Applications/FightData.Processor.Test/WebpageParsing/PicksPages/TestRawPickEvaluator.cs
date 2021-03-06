﻿using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Test;
using FightData.Processor.WebpageParsing.PicksPages;
using FightData.WebpageParsing.PicksPages;
using FightDataProcessor.PicksPages.WebpageParsing;
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

            EvaluatedPicks picksEvaluationResult = rawPickEvaluator.EvaluatePicks(new List<RawAnalystPick>() { invalidPick }, exhibition);

            Assert.IsTrue(picksEvaluationResult.UnfoundPicks.Count == 1);
        }

        [TestMethod]
        public void TestBlankAnalyst()
        {
            RawPickEvaluator rawPickEvaluator = new RawPickEvaluator(context);
            RawAnalystPick blankAnalystPick = new RawAnalystPick("", "test");
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            EvaluatedPicks picksEvaluationResult = rawPickEvaluator.EvaluatePicks(new List<RawAnalystPick>() { blankAnalystPick }, exhibition);

            Assert.IsTrue(picksEvaluationResult.ValidPicks.Count == 0 && picksEvaluationResult.UnfoundPicks.Count == 0);
        }
    }
}
