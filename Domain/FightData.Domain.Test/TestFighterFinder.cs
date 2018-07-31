using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFighterFinder : TestDomain
    {
        private TestDatabaseDataAdder databaseDataAdder;

        public TestFighterFinder()
        {
            databaseDataAdder = new TestDatabaseDataAdder(context);
            AddTestFighters();
        }

        [TestMethod]
        public void TestSimpleName()
        {
            FighterFinder fighterFinder = new FighterFinder(context); 

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("testfname testlname");

            Assert.IsTrue(finderResult.IsFound());
        }

        private void AddTestFighters()
        {
            databaseDataAdder.AddRegularFighter();
        }

    }
}
