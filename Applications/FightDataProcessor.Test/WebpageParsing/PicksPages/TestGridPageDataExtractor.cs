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
            UfcEvent ufcEvent = entityGenerator.UfcEventGenerator.GetPopulatedUfcEvent();
            ufcEvent.Webpages.Add(entityGenerator.WebpageGenerator.GetPopulatedPicksPage());
            entityGenerator.AnalystGenerator.GetPopulatedAnalyst().Add();

            EventDataExtractor eventDataExtractor = new EventDataExtractor(ufcEvent);
            eventDataExtractor.ExtractPicksPagesData();

            Pick mikeBohnsPick = context.Picks.First(p => p.Analyst.Name == "Mike Bohn" && p.Fight.Winner.LastName == "Rockhold");
            Assert.IsTrue(mikeBohnsPick.Fighter.LastName == "Rockhold");
            
        }
    }
}
