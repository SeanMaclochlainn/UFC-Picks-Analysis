using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FightData.Domain.Entities;
using FightData.Domain.Test;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsPageFightExtractor : TestDataLayer
    {
        [TestMethod]
        public void TestExtractResults()
        {
            Webpage resultsPage = entityGenerator.WebpageGenerator.GetPopulatedResultsPage();
            resultsPage.Event.Add();
            int existingFights = context.Fights.Count();
            ResultsPageFightExtractor resultsPageFightExtractor = new ResultsPageFightExtractor(resultsPage);

            resultsPageFightExtractor.ExtractResults();

            Assert.IsTrue(context.Fights.Count() == existingFights + 2);
        }
    }
}
