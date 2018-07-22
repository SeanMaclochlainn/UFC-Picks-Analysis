using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class FightersParser
    {
        private HtmlDocument htmlDocument;
        private int rowNo;
        private int maxNoOfFights = 10;
        private List<HtmlNode> fighterNodes;

        public FightersParser(HtmlDocument htmlDocument, int rowNo)
        {
            this.htmlDocument = htmlDocument;
            this.rowNo = rowNo;
            fighterNodes = new List<HtmlNode>();
        }

        public List<string> FighterNames { get; private set; }

        private void PopulateFighterNodes()
        {
            for (int columnNo = 0; columnNo <= maxNoOfFights; columnNo++)
            {
                fighterNodes.Add(htmlDocument.DocumentNode.SelectSingleNode(XpathGenerator.GetFighterXpath(rowNo, columnNo)));
            }
        }

        private void PopulateFighterNames()
        {
            RemoveEmptyNodes();
            foreach (HtmlNode node in fighterNodes)
                FighterNames.Add(node.InnerText);
        }

        private void RemoveEmptyNodes()
        {
            fighterNodes.RemoveAll(n => n == null);
        }
    }
}
