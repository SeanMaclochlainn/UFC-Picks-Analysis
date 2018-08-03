using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFighterFinder : TestDataLayer
    {
        private FighterFinder fighterFinder;

        public TestFighterFinder()
        {
            fighterFinder = new FighterFinder(context);
        }

        [TestMethod]
        public void TestFindByFullName()
        {
            Fighter.GenerateFighter("testfname testlname", context).Add();

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("testfname testlname");

            Assert.IsTrue(finderResult.IsFound());
        }

        [TestMethod]
        public void TestFindBySurname()
        {
            Fighter.GenerateFighter("Luke Rockhold", context).Add();

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("Rockhold");

            Assert.IsTrue(finderResult.IsFound());
        }

    }
}
