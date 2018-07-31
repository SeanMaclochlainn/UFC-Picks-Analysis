using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.TestData;
using FightData.Domain;
using FightData.Domain.Entities;
using FightData.TestData.EntityGenerators;

namespace FightDataProcessor.Test.WebpageParsing.WikipediaParser
{
    [TestClass]
    public class TestResultsPageDataExtractor
    {
        private ResultsPageDataExtractor resultsPageDataExtractor;
        private UfcEventGenerator ufcEventGenerator;
        private FightPicksContext context;

        public TestResultsPageDataExtractor()
        {
            context = new TestDatabase().Context;
            ufcEventGenerator = new UfcEventGenerator(context);
        }

        [TestMethod]
        public void TestExtractResults()
        {
            UfcEvent ufcEvent = ufcEventGenerator.GetPopulatedUfcEvent();
            int existingFights = ufcEvent.Fights.Count;
            resultsPageDataExtractor = new ResultsPageDataExtractor(ufcEvent, context);

            resultsPageDataExtractor.ExtractResults();

            Assert.IsTrue(ufcEvent.Fights.Count == existingFights + 11);
        }
    }
}
