using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightData.TestData.EntityGenerators;
using FightDataProcessor.WebpageParsing.PicksPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.PicksPages
{
    [TestClass]
    public class TestGridPageDataExtractor : TestDataLayer
    {
        private UfcEventGenerator ufcEventGenerator;
        private WebpageGenerator webpageGenerator;
        private AnalystGenerator analystGenerator;

        public TestGridPageDataExtractor()
        {
            ufcEventGenerator = new UfcEventGenerator(context);
            webpageGenerator = new WebpageGenerator(context);
            analystGenerator = new AnalystGenerator(context);
        }

        [TestMethod]
        public void TestExtractPick()
        {
            UfcEvent ufcEvent = ufcEventGenerator.GetPopulatedUfcEvent();
            ufcEvent.Webpages.Add(webpageGenerator.GetPopulatedPicksPage());
            analystGenerator.GetPopulatedAnalyst().Add();

            PicksPagesDataExtractor picksPageDataExtractor = new PicksPagesDataExtractor(ufcEvent);
            picksPageDataExtractor.ExtractAllPages();

            Pick mikeBohnsPick = context.Picks.First(p => p.Analyst.Name == "Mike Bohn" && p.Fight.Winner.LastName == "Rockhold");
            Assert.IsTrue(mikeBohnsPick.Fighter.LastName == "Rockhold");
            
        }
    }
}
