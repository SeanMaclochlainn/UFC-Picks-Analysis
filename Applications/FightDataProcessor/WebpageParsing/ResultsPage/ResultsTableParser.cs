using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsTableParser
    {
        private XDocument document;
        private int currentRowNo;
        private XElement currentRowWinner;
        private XElement currentRowLoser;
        private static int maxNoOfRows = 20;

        public ResultsTableParser(XDocument document)
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
            PopulateCurrentRowElements();
            if (AreElementsValid())
                return TableRowParserResult.AsFightRow(currentRowWinner.Value, currentRowLoser.Value);
            return TableRowParserResult.AsNonFightRow();
        }

        private void PopulateCurrentRowElements()
        {
            currentRowWinner = document.XPathSelectElement(ResultsTableXpathGenerator.GetWinnerXpath(currentRowNo));
            currentRowLoser = document.XPathSelectElement(ResultsTableXpathGenerator.GetLoserXpath(currentRowNo));
        }

        private bool AreElementsValid()
        {
            return !(currentRowWinner == null && currentRowLoser == null);
        }
    }
}
