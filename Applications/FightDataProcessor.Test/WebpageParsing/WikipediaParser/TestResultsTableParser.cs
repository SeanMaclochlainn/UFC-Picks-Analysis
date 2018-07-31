using FightData.TestData;
using FightDataProcessor.WebpageParsing.ResultsPage;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightDataProcessor.Test.WikipediaParser
{
    [TestClass]
    public class TestResultsTableParser
    {
        private HtmlDocument htmlDocument;
        private ResultsTableParser resultsTableParser;

        public TestResultsTableParser()
        {
            htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(MockWikipediaPageGenerator.GetHtml());
            resultsTableParser = new ResultsTableParser(htmlDocument);
        }

        [TestMethod]
        public void TestRowContainsFight()
        {
            TableRowResult result = resultsTableParser.ParseRow(3);

            Assert.IsTrue(result.IsRowContainingFight);
        }

        [TestMethod]
        public void TestRowDoesNotContainFight()
        {
            TableRowResult result = resultsTableParser.ParseRow(1);

            Assert.IsFalse(result.IsRowContainingFight);
        }

        [TestMethod]
        public void TestWinner()
        {
            TableRowResult result = resultsTableParser.ParseRow(3);

            Assert.IsTrue(result.Winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            TableRowResult result = resultsTableParser.ParseRow(4);

            Assert.IsTrue(result.Loser == "Ross Pearson");
        }
    }
}
