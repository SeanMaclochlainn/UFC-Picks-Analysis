using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFighterFinder : TestDomain
    {

        public TestFighterFinder()
        {
            AddTestFighters();
        }

        [TestMethod]
        public void TestSimpleName()
        {
            FighterFinder fighterFinder = new FighterFinder("testfname testlname", context);

            bool fighterExists = fighterFinder.FighterExists;

            Assert.IsTrue(fighterExists);
        }

        private void AddTestFighters()
        {
            databaseDataGenerator.AddRegularFighter();
        }

    }
}
