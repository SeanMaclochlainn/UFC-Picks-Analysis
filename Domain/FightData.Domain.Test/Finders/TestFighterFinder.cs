using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FightData.Domain.Test.Finders
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
        public void TestFindWithinExhibition()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();
            exhibition.Add();

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("Luke Rockhold", exhibition);

            Assert.IsTrue(finderResult.IsFound());
        }

        [TestMethod]
        public void TestFindBySurname()
        {
            Fighter.GenerateFighter("Luke Rockhold", context).Add();

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("Rockhold");

            Assert.IsTrue(finderResult.IsFound());
        }

        [TestMethod]
        public void TestGetFightersInExhibition()
        {
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();

            List<Fighter> fighters = fighterFinder.GetFighters(exhibition);

            Assert.IsTrue(fighters.Count == 2);
        }

        [TestMethod]
        public void TestGetFightersInFight()
        {
            Fight fight = entityGenerator.FightGenerator.GetPopulatedFight();

            List<Fighter> fighters = fighterFinder.GetFighters(fight);

            Assert.IsTrue(fighters.Count == 2);
        }

    }
}
