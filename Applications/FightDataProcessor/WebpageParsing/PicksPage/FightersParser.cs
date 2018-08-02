using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class FightersParser
    {
        private HtmlDocument htmlDocument;
        private static int maxNoOfFights = 10;
        private int currentRow;
        private int columnNo;

        public FightersParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public List<string> ParseFighters(int currentRow)
        {
            this.currentRow = currentRow;
            List<string> fighters = new List<string>();
            foreach (HtmlNode fighterNode in ParseFighterNodes())
                fighters.Add(fighterNode.InnerText.Trim());
            return fighters;
        }

        private List<HtmlNode> ParseFighterNodes()
        {
            List<HtmlNode> fighterNodes = new List<HtmlNode>();
            for (int i = 1; i <= maxNoOfFights; i++)
            {
                columnNo = i;
                HtmlNode parsedFighter = ParseFighter();
                if (parsedFighter != null)
                    fighterNodes.Add(parsedFighter);
            }
            return fighterNodes;
        }

        private HtmlNode ParseFighter()
        {
            return htmlDocument.DocumentNode.SelectSingleNode(XpathGenerator.GetFighterXpath(currentRow, columnNo));
        }
    }
}
