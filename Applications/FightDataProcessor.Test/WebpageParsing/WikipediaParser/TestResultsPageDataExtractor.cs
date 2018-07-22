using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.TestData;
using FightData.Domain;
using System.Linq;

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
            resultsPageDataExtractor = new ResultsPageDataExtractor(entityDataGenerator.GetStandardUfcEvent(), context);

            resultsPageDataExtractor.ExtractResults();

            Assert.IsTrue(context.Fights.Count() == 11);
        }
    }
}
