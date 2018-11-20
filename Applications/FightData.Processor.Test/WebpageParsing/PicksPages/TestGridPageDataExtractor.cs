using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestGridPageDataExtractor : TestDataLayer
    {
        [TestMethod]
        public void TestExtractPick()
        {
            Exhibition exhibition = entityFinder.ExhibitionFinder.FindExhibition("UFC 179");

            ExhibitionDataExtractor exhibitionDataExtractor = new ExhibitionDataExtractor(exhibition);
            exhibitionDataExtractor.ExtractAllWebpages();

            Pick mikeBohnsPick = context.Picks.First(p => p.Analyst.Name == "Mike Bohn" && p.Fight.Winner.LastName == "aldo");
            Assert.IsTrue(mikeBohnsPick.Fighter.LastName == "aldo");   
        }
    }
}
