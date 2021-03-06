﻿using FightData.Domain.Builders;
using FightData.Domain.Entities;
using FightData.Domain.Finders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightData.Domain.Test
{
    [TestClass]
    public class TestFightUpdater : TestDataLayer
    {
        private FightUpdater fightUpdater;
        private ExhibitionFinder exhibitionFinder;
        private FighterBuilder fighterBuilder;

        public TestFightUpdater()
        {
            fightUpdater = new FightUpdater(context);
            exhibitionFinder = new ExhibitionFinder(context);
            fighterBuilder = new FighterBuilder(context);
        }

        [TestMethod]
        public void TestAddingFights()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("FN 55");
            int originalFightCount = exhibition.Fights.Count();
            fightUpdater.AddFights(new List<RawFightResult>() { new RawFightResult("test winner", "test loser") }, exhibition);

            Assert.IsTrue(exhibition.Fights.Count() == originalFightCount + 1);
        }

        [TestMethod]
        public void TestAddFighterWithAccent()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("UFC 179");

            fightUpdater.AddFights(new List<RawFightResult>() { new RawFightResult("José Aldo", "Chad Mendes") }, exhibition);

            Assert.IsTrue(exhibitionFinder.FindExhibition("UFC 179").Fights.First().Winner.FullName == "jose aldo");
        }

        [TestMethod]
        public void TestAddFighterWithExistingSurname()
        {
            Exhibition exhibition = exhibitionFinder.FindExhibition("UFC 179");

            Fighter fighter = fighterBuilder.GenerateFighter("Junior dos Santos").Build();
            entityUpdater.FighterUpdater.Add(fighter);
            fightUpdater.AddFights(new List<RawFightResult>() { new RawFightResult("Daniel Sarafian", "Antônio dos Santos Jr." ) }, exhibition);

            Assert.IsTrue(context.Fighters.Any(f => f.FullName == "antonio dos santos"));
        }
    }
}
