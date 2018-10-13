using HtmlAgilityPack;
using System.Linq;

namespace FightDataProcessor.WebpageParsing.PicksPages
{
    public class PicksPageAnalystParser
    {
        private HtmlDocument htmlDocument;

        public PicksPageAnalystParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public string ParseAnalyst(int rowNo)
        {
            HtmlNode analystNode = htmlDocument.DocumentNode.SelectNodes(XpathGenerator.PicksPageAnalystXpath(rowNo))?.FirstOrDefault();
            if (analystNode != null)
                return DataSanitizer.GetElementValue(analystNode);
            else
                return "";
        }
    }
}
