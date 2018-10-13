using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageFightersParser
    {
        private static int maxNoOfFights = 10;
        private HtmlDocument htmlDocument;
        private int currentRow;

        public PicksPageFightersParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public List<string> ParseFighters(int rowNo)
        {
            currentRow = rowNo;
            List<string> fighters = new List<string>();
            foreach (HtmlNode fighterNode in GetCurrentRowFighterElements())
                fighters.Add(DataSanitizer.GetElementValue(fighterNode));
            return fighters;
        }

        private List<HtmlNode> GetCurrentRowFighterElements()
        {
            List<HtmlNode> fighterNodes = new List<HtmlNode>();
            for (int currentColumnNo = 1; currentColumnNo <= maxNoOfFights; currentColumnNo++)
            {
                HtmlNode parsedFighter = ParseFighter(currentColumnNo);
                if (parsedFighter != null)
                    fighterNodes.Add(parsedFighter);
            }
            return fighterNodes;
        }

        private HtmlNode ParseFighter(int columnNo)
        {
            return htmlDocument.DocumentNode.SelectNodes(XpathGenerator.PicksPageFighterXpath(currentRow, columnNo))?.FirstOrDefault();
        }

    }
}
