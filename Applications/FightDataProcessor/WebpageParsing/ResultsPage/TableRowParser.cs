using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsTableParser
    {
        private HtmlDocument document;
        private int currentRowNo;
        private HtmlNode currentRowWinner;
        private HtmlNode currentRowLoser;

        public ResultsTableParser(HtmlDocument document)
        {
            this.document = document;
        }

        public TableRowResult ParseRow(int rowNo)
        {
            currentRowNo = rowNo;
            PopulateCurrentRowNodes();
            if (AreNodesValid())
                return TableRowResult.AsFightRow(currentRowWinner.InnerText, currentRowLoser.InnerText);
            return TableRowResult.AsNonFightRow();
        }

        private void PopulateCurrentRowNodes()
        {
            currentRowWinner = document.DocumentNode.SelectSingleNode(ResultsTableXpathGenerator.GetWinnerXpath(currentRowNo));
            currentRowLoser = document.DocumentNode.SelectSingleNode(ResultsTableXpathGenerator.GetLoserXpath(currentRowNo));
        }

        private bool AreNodesValid()
        {
            return !(currentRowWinner == null && currentRowLoser == null);
        }
    }
}
