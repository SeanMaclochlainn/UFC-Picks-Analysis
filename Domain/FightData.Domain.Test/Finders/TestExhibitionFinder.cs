﻿
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.TestData.EntityGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test.Finders
{
    [TestClass]
    public class TestExhibitionFinder : TestDataLayer
    {
        private ExhibitionFinder exhibitionFinder;

        public TestExhibitionFinder()
        {
            exhibitionFinder = new ExhibitionFinder(context);
        }

        [TestMethod]
        public void TestFindAllExhibitions()
        {
            new ExhibitionGenerator(context).GetPopulatedExhibition().Add();

            List<Exhibition> exhibitions = exhibitionFinder.FindAllExhibitions();

            Assert.IsNotNull(exhibitions.First().Webpages.First().Website);
        }

    }
}