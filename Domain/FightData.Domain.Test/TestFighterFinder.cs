using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            FighterFinder fighterFinder = new FighterFinder(context); 

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("testfname testlname");

            Assert.IsTrue(finderResult.IsFound());
        }

        private void AddTestFighters()
        {
            databaseDataGenerator.AddRegularFighter();
        }

    }
}
