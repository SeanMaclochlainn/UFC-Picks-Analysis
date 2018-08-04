using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing.PicksPages;
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
            UfcEvent ufcEvent = entityGenerator.UfcEventGenerator.GetPopulatedUfcEvent();
            ufcEvent.Webpages.Add(entityGenerator.WebpageGenerator.GetPopulatedPicksPage());
            entityGenerator.AnalystGenerator.GetPopulatedAnalyst().Add();

            PicksPagesDataExtractor picksPageDataExtractor = new PicksPagesDataExtractor(ufcEvent);
            picksPageDataExtractor.ExtractAllPages();

            Pick mikeBohnsPick = context.Picks.First(p => p.Analyst.Name == "Mike Bohn" && p.Fight.Winner.LastName == "Rockhold");
            Assert.IsTrue(mikeBohnsPick.Fighter.LastName == "Rockhold");
            
        }
    }
}
