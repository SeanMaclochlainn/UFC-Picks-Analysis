using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class EntityRowParser
    {
        private static int maxNoOfFights = 10;
        private HtmlDocument htmlDocument;
        private int currentRow;
        private int currentColumnNo;
        private string xpath;

        public EntityRowParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public List<string> ParseEntities(int rowNo, string xpath)
        {
            currentRow = rowNo;
            this.xpath = xpath;
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
                this.currentColumnNo = currentColumnNo;
                FinderResult<HtmlNode> fighterResult = FindFighter();
                if (fighterResult.IsFound())
                    fighterNodes.Add(fighterResult.Result);
            }
            return fighterNodes;
        }

        private FinderResult<HtmlNode> FindFighter()
        {
            FinderResult<HtmlNode> result = new FinderResult<HtmlNode>(htmlDocument.DocumentNode.SelectNodes(GetFormattedXpath())?.FirstOrDefault());
            Debug.WriteLine($"Searched for fighter with xpath: {xpath} \r\n Successful result: {result.IsFound()}");
            return result;
        }

        private string GetFormattedXpath()
        {
            return XpathGenerator.FormatXpath(xpath, currentRow, currentColumnNo);
        }

    }
}
