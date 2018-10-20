using FightData.Domain.Entities;
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
        [TestMethod]
        public void TestIsCorrect()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();
            exhibition.Add();

            Assert.IsTrue(context.Picks.First().IsCorrect());
        }

        [TestMethod]
        public void TestIsIncorrect()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();
            exhibition.Add();

            Assert.IsTrue(context.Picks.ToList().ElementAt(1).IsCorrect() == false);
        }
    }
}
