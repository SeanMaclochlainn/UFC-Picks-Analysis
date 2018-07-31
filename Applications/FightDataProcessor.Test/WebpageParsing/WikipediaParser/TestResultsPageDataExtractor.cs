using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.TestData;
using FightData.Domain;
using System.Linq;
using FightData.Domain.Entities;

namespace FightDataProcessor.Test.WebpageParsing.WikipediaParser
{
    [TestClass]
    public class TestResultsPageDataExtractor
    {
        private ResultsPageDataExtractor resultsPageDataExtractor;
        private TestEntityGenerator entityDataGenerator;
        private FightPicksContext context;

        public TestResultsPageDataExtractor()
        {
            context = new TestDatabase().Context;
            entityDataGenerator = new TestEntityGenerator(context);
        }

        [TestMethod]
        public void TestExtractResults()
        {
            UfcEvent ufcEvent = entityDataGenerator.GetPopulatedUfcEvent();
            int existingFights = ufcEvent.Fights.Count;
            resultsPageDataExtractor = new ResultsPageDataExtractor(ufcEvent, context);

            resultsPageDataExtractor.ExtractResults();

            Assert.IsTrue(ufcEvent.Fights.Count == existingFights + 11);
        }
    }
}
