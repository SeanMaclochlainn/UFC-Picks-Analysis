using FightData.TestData;
using FightDataProcessor.WebpageParsing.ResultsPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FightDataProcessor.Test.WebpageParsing.ResultsPage
{
    [TestClass]
    public class TestResultsTableParser
    {
        private XDocument htmlDocument;
        private ResultsTableParser resultsTableParser;

        public TestResultsTableParser()
        {
            htmlDocument = XDocument.Parse(HtmlPageGenerator.GetWikipediaPage());
            resultsTableParser = new ResultsTableParser(htmlDocument);
        }

        [TestMethod]
        public void TestRowContainsFight()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsTrue(parserResults.ElementAt(1).IsRowContainingFight);
        }

        [TestMethod]
        public void TestRowDoesNotContainFight()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsFalse(parserResults.ElementAt(0).IsRowContainingFight);
        }

        [TestMethod]
        public void TestWinner()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsTrue(parserResults.ElementAt(1).Winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            List<TableRowParserResult> parserResults = resultsTableParser.ParseTable();

            Assert.IsTrue(parserResults.ElementAt(2).Loser == "Ross Pearson");

        }
    }
}
