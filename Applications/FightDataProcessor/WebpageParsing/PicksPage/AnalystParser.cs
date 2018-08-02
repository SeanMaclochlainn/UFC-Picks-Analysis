using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class AnalystParser
    {
        private HtmlDocument htmlDocument;

        public AnalystParser(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        public string ParseAnalyst(int currentRow)
        {
            HtmlNode analystNode = htmlDocument.DocumentNode.SelectSingleNode(XpathGenerator.GetAnalystXpath(currentRow));
            if (analystNode != null)
                return analystNode.InnerText;
            else
                return "";
        }
    }
}
