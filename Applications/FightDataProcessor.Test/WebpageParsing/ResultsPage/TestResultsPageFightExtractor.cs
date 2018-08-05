using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.Domain.Entities;
using FightData.Domain.Test;
using System.Linq;
using FightDataProcessor.WebpageParsing;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor : TestDataLayer
    {
        [TestMethod]
        public void TestExtractResults()
        {
            UfcEvent ufcEvent = GetResultsPageEvent();
            ufcEvent.Add();
            EventDataExtractor eventDataExtractor = new EventDataExtractor(ufcEvent);
            int existingFights = context.Fights.Count();

            eventDataExtractor.ExtractResultsPageData();

            Assert.IsTrue(context.Fights.Count() == existingFights + 2);
        }

        private UfcEvent GetResultsPageEvent()
        {
            UfcEvent ufcEvent = entityGenerator.UfcEventGenerator.GetEmptyUfcEvent();
            ufcEvent.Webpages.Add(entityGenerator.WebpageGenerator.GetPopulatedResultsPage());
            return ufcEvent;
        }
    }
}
