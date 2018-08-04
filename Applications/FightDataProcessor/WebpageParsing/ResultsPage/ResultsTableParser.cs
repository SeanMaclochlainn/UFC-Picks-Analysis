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

        public List<ParsedTableRow> ParseTableRows()
        {
            List<ParsedTableRow> parsedTableRows = new List<ParsedTableRow>();

            for (int i = 1; i < maxNoOfRows; i++)
            {
                currentRowNo = i;
                PopulateCurrentRowElements();
                if (AreElementsValid())
                    parsedTableRows.Add(new ParsedTableRow(currentRowWinner.Value, currentRowLoser.Value));
            }

            return parsedTableRows;
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
