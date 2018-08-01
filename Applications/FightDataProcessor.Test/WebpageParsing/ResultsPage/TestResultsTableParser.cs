using FightData.TestData;
using FightDataProcessor.WebpageParsing.ResultsPage;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
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
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsTrue(parserResults.ElementAt(3).IsRowContainingFight);
        }

        [TestMethod]
        public void TestRowDoesNotContainFight()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsFalse(parserResults.ElementAt(1).IsRowContainingFight);
        }

        [TestMethod]
        public void TestWinner()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsTrue(parserResults.ElementAt(2).Winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsTrue(parserResults.ElementAt(3).Loser == "Ross Pearson");

        }
    }
}
