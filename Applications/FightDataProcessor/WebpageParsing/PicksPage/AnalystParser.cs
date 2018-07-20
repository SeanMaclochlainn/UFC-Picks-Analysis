using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.PicksPage
{
    public class AnalystParser
    {
        private HtmlDocument htmlDocument;
        private int rowNo;
        private HtmlNode analystNode;

        public AnalystParser(HtmlDocument htmlDocument, int rowNo)
        {
            this.htmlDocument = htmlDocument;
            this.rowNo = rowNo;
            PopulateAnalystNode();
            PopulateAnalystName();
        }

        public string AnalystName { get; private set; }

        public bool IsValidRow()
        {
            return !(analystNode == null);
        }

        private void PopulateAnalystNode()
        {
            analystNode = htmlDocument.DocumentNode.SelectSingleNode(XpathGenerator.GetAnalystXpath(rowNo));
        }

        private void PopulateAnalystName()
        {
            if(IsValidRow())
                AnalystName = analystNode.InnerText;
        }
    }
}
