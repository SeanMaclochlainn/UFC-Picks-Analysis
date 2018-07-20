using FightDataProcessor.WebpageParsing.ResultsPage;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FightDataProcessor.Test.WikipediaParser
{
    [TestClass]
    public class TestTableRowParser
    {
        private HtmlDocument htmlDocument;

        public TestTableRowParser()
        {
            htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(MockWikipediaPages.GetStandardPage());
        }

        [TestMethod]
        public void TestIsFight()
        {
            TableRowParser lineParser = new TableRowParser(htmlDocument, 3);

            bool isfight = lineParser.ContainsResult();

            Assert.IsTrue(isfight);
        }

        [TestMethod]
        public void TestWinner()
        {
            TableRowParser lineParser = new TableRowParser(htmlDocument, 3);

            string winner = lineParser.WinnersName;

            Assert.IsTrue(winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            TableRowParser lineParser = new TableRowParser(htmlDocument, 4);

            string loser = lineParser.LosersName;

            Assert.IsTrue(loser == "Ross Pearson");
        }
    }
}
