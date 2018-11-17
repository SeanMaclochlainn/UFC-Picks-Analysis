using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.Domain.Updaters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FightData.Domain.Test.Entities
{
    [TestClass]
    public class TestPick : TestDataLayer
    {
        private ExhibitionFinder exhibitionFinder;
        private PickFinder pickFinder;
        private AnalystFinder analystFinder;
        private ExhibitionUpdater exhibitionUpdater;

        public TestPick()
        {
            exhibitionFinder = new ExhibitionFinder(context);
            pickFinder = new PickFinder(context);
            analystFinder = new AnalystFinder(context);
            exhibitionUpdater = new ExhibitionUpdater(context);
        }

        [TestMethod]
        public void TestIsCorrect()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();
            exhibitionUpdater.Add(exhibition);

            Assert.IsTrue(context.Picks.First().IsCorrect());
        }

        [TestMethod]
        public void TestIsIncorrect()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            Pick pick = pickFinder.FindPick(analystFinder.FindAnalyst("Dann Stupp").Result, exhibition.Fights.First()).Result;
            
            Assert.IsTrue(pick.IsCorrect() == false);
        }
    }
}
