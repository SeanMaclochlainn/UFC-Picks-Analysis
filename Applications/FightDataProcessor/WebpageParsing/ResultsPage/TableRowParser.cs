﻿using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsTableParser
    {
        public ResultsTableParser(HtmlDocument document)
        {

        }
    }

    public class TableRowParser
    {
        HtmlDocument document;
        private int rowNo;
        private HtmlNode winnerNode;
        private HtmlNode loserNode;

        public TableRowParser(HtmlDocument document, int rowNo)
        {
            this.document = document;
            this.rowNo = rowNo;
            PopulateFighterNodes();
            PopulateFighterNames();
        }

        public string WinnersName { get; private set; }
        public string LosersName { get; private set; }

        public bool ContainsResult()
        {
            return !(winnerNode == null);
        }

        private void PopulateFighterNodes()
        {
            winnerNode = document.DocumentNode.SelectSingleNode(XpathGenerator.GetWinnerXpath(rowNo));
            loserNode = document.DocumentNode.SelectSingleNode(XpathGenerator.GetLoserXpath(rowNo));
        }

        private void PopulateFighterNames()
        {
            if (ContainsResult())
            {
                WinnersName = GetWinner();
                LosersName = GetLoser();
            }
        }

        private string GetWinner()
        {
            return winnerNode.InnerText;
        }

        private string GetLoser()
        {
            return loserNode.InnerText;
        }

    }
}
