using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.Domain.Entities;
using FightData.Domain.Test;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor : TestDataLayer
    {
        [TestMethod]
        public void TestExtractResults()
        {
            UfcEvent ufcEvent = entityGenerator.UfcEventGenerator.GetPopulatedUfcEvent();
            int existingFights = ufcEvent.Fights.Count;
            ResultsPageFightExtractor resultsPageFightExtractor = new ResultsPageFightExtractor(ufcEvent);

            resultsPageFightExtractor.ExtractFights();

            Assert.IsTrue(ufcEvent.Fights.Count == existingFights + 2);
        }
    }
}
