using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.TestData;

namespace FightDataProcessor.Test.WebpageParsing.WikipediaParser
{
    [TestClass]
    public class TestResultsPageDataExtractor
    {
        private ResultsPageDataExtractor resultsPageDataExtractor;
        private TestEntityGenerator entityDataGenerator;

        public TestResultsPageDataExtractor()
        {
            entityDataGenerator = new TestEntityGenerator();

        }

        //[TestMethod]
        //public void TestExtractResults()
        //{
        //    resultsPageDataExtractor = new ResultsPageDataExtractor(entityDataGenerator.GetUfcEvent());
        //}
    }
}
