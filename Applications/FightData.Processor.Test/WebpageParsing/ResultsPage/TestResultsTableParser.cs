using FightData.Domain;
using FightData.Domain.Entities;
using FightData.Domain.Test;
using FightDataProcessor.WebpageParsing;
using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsTableParser : TestDataLayer
    {
        private ResultsPageParser resultsTableParser;

        public TestResultsTableParser()
        {
            Webpage resultsPage = entityFinder.WebpageFinder.GetResultsPage(entityFinder.ExhibitionFinder.FindExhibition("FN 55"));
            resultsTableParser = new ResultsPageParser(new HtmlPageParser(resultsPage.Data).ParseHtml());
        }

        [TestMethod]
        public void TestCorrectResultCount()
        {
            List<RawFightResult> rawFightResults = resultsTableParser.ParseResultTable();

            Assert.IsTrue(rawFightResults.Count() == 2);
        }

        [TestMethod]
        public void TestWinner()
        {
            List<RawFightResult> rawFightResults = resultsTableParser.ParseResultTable();

            Assert.IsTrue(rawFightResults.ElementAt(0).Winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            List<RawFightResult> rawFightResults = resultsTableParser.ParseResultTable();

            Assert.IsTrue(rawFightResults.ElementAt(1).Loser == "Ross Pearson");

        }
    }
}
