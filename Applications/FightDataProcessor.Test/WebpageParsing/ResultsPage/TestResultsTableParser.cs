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
        public void TestCorrectResultCount()
        {
            List<ParsedTableRow> parsedTableRows = resultsTableParser.ParseTableRows();

            Assert.IsTrue(parsedTableRows.Count() == 2);
        }

        [TestMethod]
        public void TestWinner()
        {
            List<ParsedTableRow> parsedTableRows = resultsTableParser.ParseTableRows();

            Assert.IsTrue(parsedTableRows.ElementAt(0).Winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            List<ParsedTableRow> parseTableRows = resultsTableParser.ParseTableRows();

            Assert.IsTrue(parseTableRows.ElementAt(1).Loser == "Ross Pearson");

        }
    }
}
