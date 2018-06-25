using FightDataProcessor.WikipediaParser;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.Test.WikipediaParser
{
    [TestClass]
    public class TestLineParser
    {
        private HtmlDocument htmlDocument;

        public TestLineParser()
        {
            htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(MockWikipediaPages.GetStandardPage());
        }

        [TestMethod]
        public void TestIsFight()
        {
            LineParser lineParser = new LineParser(htmlDocument, 3);

            bool isfight = lineParser.ValidLine();

            Assert.IsTrue(isfight);
        }

        [TestMethod]
        public void TestWinner()
        {
            LineParser lineParser = new LineParser(htmlDocument, 3);

            string winner = lineParser.WinnersName;

            Assert.IsTrue(winner == "Luke Rockhold");
        }

        [TestMethod]
        public void TestLoser()
        {
            LineParser lineParser = new LineParser(htmlDocument, 4);

            string loser = lineParser.LosersName;

            Assert.IsTrue(loser == "Ross Pearson");
        }
    }
}
