using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.Domain.Entities;
using FightData.TestData.EntityGenerators;
using FightData.Domain.Test;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor : TestDataLayer
    {
        private ResultsPageFightExtractor resultsPageDataExtractor;
        private UfcEventGenerator ufcEventGenerator;

        public TestResultsPageFightExtractor()
        {
            ufcEventGenerator = new UfcEventGenerator(context);
        }

        [TestMethod]
        public void TestExtractResults()
        {
            UfcEvent ufcEvent = ufcEventGenerator.GetPopulatedUfcEvent();
            int existingFights = ufcEvent.Fights.Count;
            resultsPageDataExtractor = new ResultsPageFightExtractor(ufcEvent);

            resultsPageDataExtractor.ExtractFights();

            Assert.IsTrue(ufcEvent.Fights.Count == existingFights + 2);
        }
    }
}
