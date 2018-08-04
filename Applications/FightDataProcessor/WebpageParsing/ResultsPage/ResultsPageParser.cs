using FightData.Domain;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageParser
    {
        private XDocument document;
        private int currentRowNo;
        private XElement currentRowWinner;
        private XElement currentRowLoser;
        private static int maxNoOfRows = 20;

        public ResultsPageParser(XDocument document)
        {
            this.document = document;
        }

        public List<FightResult> ParseTableRows()
        {
            List<FightResult> parsedTableRows = new List<FightResult>();

            for (int i = 1; i < maxNoOfRows; i++)
            {
                currentRowNo = i;
                PopulateCurrentRowElements();
                if (AreElementsValid())
                    parsedTableRows.Add(new FightResult(currentRowWinner.Value, currentRowLoser.Value));
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
