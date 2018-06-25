using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.WikipediaParser
{
    public class LineParser
    {
        HtmlDocument document;
        private string winnerXpath = @"//*[@class='toccolours']/tbody/tr[{0}]/td[2]/a|//*[@class='toccolours']/tr[{0}]/td[2]/a";
        private string loserXpath = @"//*[@class='toccolours']/tbody/tr[{0}]/td[4]/a|//*[@class='toccolours']/tr[{0}]/td[4]/a";
        private int lineNo;
        private HtmlNode winnerNode;
        private HtmlNode loserNode;

        public LineParser(HtmlDocument document, int lineNo)
        {
            this.document = document;
            this.lineNo = lineNo;
            PopulateNodes();
            PopulateFighterNames();
        }

        public string WinnersName { get; private set; }
        public string LosersName { get; private set; }

        public bool ValidLine()
        {
            return !(winnerNode == null);
        }

        private void PopulateNodes()
        {
            winnerNode = document.DocumentNode.SelectSingleNode(FormattedWinnerXpath());
            loserNode = document.DocumentNode.SelectSingleNode(FormattedLoserXpath());
        }

        private string FormattedWinnerXpath()
        {
            return String.Format(winnerXpath, lineNo);
        }

        private string FormattedLoserXpath()
        {
            return string.Format(loserXpath, lineNo);
        }

        private void PopulateFighterNames()
        {
            if (ValidLine())
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
