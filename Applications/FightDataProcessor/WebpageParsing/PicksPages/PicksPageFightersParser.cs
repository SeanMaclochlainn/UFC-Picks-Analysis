using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;
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
                FinderResult<HtmlNode> fighterResult = FindFighter(currentColumnNo);
                if (fighterResult.IsFound())
                    fighterNodes.Add(fighterResult.Result);
            }
            return fighterNodes;
        }

        private FinderResult<HtmlNode> FindFighter(int columnNo)
        {
            string xpath = XpathGenerator.PicksPageFighterXpath(currentRow, columnNo);
            FinderResult<HtmlNode> result = new FinderResult<HtmlNode>(htmlDocument.DocumentNode.SelectNodes(xpath)?.FirstOrDefault());
            Debug.WriteLine($"Searched with xpath: {xpath} \r\n Successful result: {result.IsFound()}");
            return result;
        }

    }
}
