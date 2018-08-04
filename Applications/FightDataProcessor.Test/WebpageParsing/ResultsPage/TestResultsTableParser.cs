using FightData.Domain;
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
        private ResultsPageParser resultsTableParser;

        public TestResultsTableParser()
        {
            htmlDocument = XDocument.Parse(HtmlPageGenerator.GetWikipediaPage());
            resultsTableParser = new ResultsPageParser(htmlDocument);
        }

        [TestMethod]
        public void TestCorrectResultCount()
        {
            List<FightResult> fightResults = resultsTableParser.ParseTableRows();

            Assert.IsTrue(fightResults.Count() == 2);
        }

        [TestMethod]
        public void TestWinner()
        {
            List<FightResult> fightResults = resultsTableParser.ParseTableRows();

            Assert.IsTrue(fightResults.ElementAt(0).Winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            List<FightResult> fightResults = resultsTableParser.ParseTableRows();

            Assert.IsTrue(fightResults.ElementAt(1).Loser == "Ross Pearson");

        }
    }
}
