using HtmlAgilityPack;
using System.Collections.Generic;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsTableParser
    {
        private HtmlDocument document;
        private int currentRowNo;
        private HtmlNode currentRowWinner;
        private HtmlNode currentRowLoser;
        private static int maxNoOfRows = 20;

        public ResultsTableParser(HtmlDocument document)
        {
            this.document = document;
        }

        public List<TableRowParserResult> ParseTable()
        {
            List<TableRowParserResult> parserResults = new List<TableRowParserResult>();

            for (int i = 1; i < maxNoOfRows; i++)
            {
                currentRowNo = i;
                parserResults.Add(ParseCurrentRow());
            }

            return parserResults;
        }

        private TableRowParserResult ParseCurrentRow()
        {
            PopulateCurrentRowNodes();
            if (AreNodesValid())
                return TableRowParserResult.AsFightRow(currentRowWinner.InnerText, currentRowLoser.InnerText);
            return TableRowParserResult.AsNonFightRow();
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
