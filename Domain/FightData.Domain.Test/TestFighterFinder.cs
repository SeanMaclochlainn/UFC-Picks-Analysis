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
            FighterFinder fighterFinder = FighterFinder.WithCustomContext(context); 

            fighterFinder.FindFighter("testfname testlname");
            bool fighterExists = fighterFinder.Found;

            Assert.IsTrue(fighterExists);
        }

        private void AddTestFighters()
        {
            databaseDataGenerator.AddRegularFighter();
        }

    }
}
