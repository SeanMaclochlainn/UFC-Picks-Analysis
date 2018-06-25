using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFighterFinder : TestDataEntity
    {

        public TestFighterFinder()
        {
            AddTestFighters();
        }

        [TestMethod]
        public void TestSimpleName()
        {
            FighterFinder fighterFinder = new FighterFinder(context, "testfname testlname");

            bool fighterExists = fighterFinder.FighterExists;

            Assert.IsTrue(fighterExists);
        }

        private void AddTestFighters()
        {
            databaseDataGenerator.AddRegularFighter();
        }

    }
}
