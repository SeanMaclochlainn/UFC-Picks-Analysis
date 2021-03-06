﻿using FightData.Domain.Builders;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using FightData.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test.Finders
{
    [TestClass]
    public class TestFighterFinder : TestDataLayer
    {
        private FighterFinder fighterFinder;
        private ExhibitionFinder exhibitionFinder;
        private FighterBuilder fighterBuilder;

        public TestFighterFinder()
        {
            fighterFinder = new FighterFinder(context);
            exhibitionFinder = new ExhibitionFinder(context);
            fighterBuilder = new FighterBuilder(context);
        }

        [TestMethod]
        public void TestFindByFullName()
        {
            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("Luke Rockhold");

            Assert.IsTrue(finderResult.IsFound());
        }

        [TestMethod]
        public void TestFindWithinExhibition()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("Luke Rockhold", exhibition);

            Assert.IsTrue(finderResult.IsFound());
        }

        [TestMethod]
        public void TestFindBySurname()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            FinderResult<Fighter> finderResult = fighterFinder.FindFighter("Rockhold", exhibition);

            Assert.IsTrue(finderResult.IsFound());
        }

        [TestMethod]
        public void TestGetFightersInExhibition()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");

            List<Fighter> fighters = FighterFinder.GetFighters(exhibition);

            Assert.IsTrue(fighters.Count == 6);
        }

        [TestMethod]
        public void TestGetFightersInFight()
        {
            Fight fight = exhibitionFinder.FindExhibition("FN 55").Fights.First();

            List<Fighter> fighters = fighterFinder.GetFighters(fight);

            Assert.IsTrue(fighters.Count == 2);
        }

        [TestMethod]
        public void TestDuplicateFighters()
        {
            List<Fighter> fighters = new List<Fighter>();
            fighters.Add(fighterBuilder.GenerateFighter("Luke Rockhold").Build());
            fighters.Add(fighterBuilder.GenerateFighter("Luke Rockhold").Build());

            FinderResult<Fighter> result = FighterFinder.FindFighter(fighters, "Rockhold");

            Assert.IsTrue(result.IsFound() == false);
        }

        [TestMethod]
        public void TestFindUsingAccentedName()
        {
            List<Fighter> fighters = new List<Fighter>();
            fighters.Add(fighterBuilder.GenerateFighter("Ildemar Alcântara").Build());

            FinderResult<Fighter> result = FighterFinder.FindFighter(fighters, "Alcântara");

            Assert.IsTrue(result.IsFound() == true);
        }

        [TestMethod]
        public void TestFindIncludingMiddleName()
        {
            List<Fighter> fighters = new List<Fighter>();
            fighters.Add(fighterBuilder.GenerateFighter("Rafael dos Anjos").Build());
            FinderResult<Fighter> result = FighterFinder.FindFighter(fighters, "Dos Anjos");

            Assert.IsTrue(result.IsFound() == true);
        }

        [TestMethod]
        public void TestFindByParsedSurname()
        {
            List<Fighter> fighters = new List<Fighter>();
            fighters.Add(fighterBuilder.GenerateFighter("Urijah Hall").Build());

            FinderResult<Fighter> result = FighterFinder.FindFighter(fighters, "Uriah Hall");

            Assert.IsTrue(result.IsFound());
        }
    }
}
