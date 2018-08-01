using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.TestData;
using FightData.Domain;
using FightData.Domain.Entities;
using FightData.TestData.EntityGenerators;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor
    {
        private ResultsPageFightExtractor resultsPageDataExtractor;
        private UfcEventGenerator ufcEventGenerator;
        private FightPicksContext context;

        public TestResultsPageFightExtractor()
        {
            context = new TestDatabase().Context;
            ufcEventGenerator = new UfcEventGenerator(context);
        }

        [TestMethod]
        public void TestExtractResults()
        {
            UfcEvent ufcEvent = ufcEventGenerator.GetPopulatedUfcEvent();
            int existingFights = ufcEvent.Fights.Count;
            resultsPageDataExtractor = new ResultsPageFightExtractor(ufcEvent, context);

            resultsPageDataExtractor.ExtractFights();

            Assert.IsTrue(ufcEvent.Fights.Count == existingFights + 11);
        }
    }
}
