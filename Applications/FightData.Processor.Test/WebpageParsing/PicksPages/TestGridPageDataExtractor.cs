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
            Exhibition exhibition = entityGenerator.ExhibitionGenerator.GetParsedExhibition();
            exhibition.Webpages.Add(entityGenerator.WebpageGenerator.GetPopulatedPicksPage());
            entityGenerator.AnalystGenerator.GetPopulatedAnalyst().Add();

            ExhibitionDataExtractor exhibitionDataExtractor = new ExhibitionDataExtractor(exhibition);
            exhibitionDataExtractor.ExtractPicksPagesData();

            Pick mikeBohnsPick = context.Picks.First(p => p.Analyst.Name == "Mike Bohn" && p.Fight.Winner.LastName == "Rockhold");
            Assert.IsTrue(mikeBohnsPick.Fighter.LastName == "Rockhold");
            
        }
    }
}
