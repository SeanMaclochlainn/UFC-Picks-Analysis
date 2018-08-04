using FightData.Domain;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageParser
    {
        private XDocument resultsPageHtml;
        private int currentRowNo;
        private XElement currentRowWinner;
        private XElement currentRowLoser;
        private static int maxNoOfRows = 20;

        public ResultsPageParser(XDocument resultsPageHtml)
        {
            this.resultsPageHtml = resultsPageHtml;
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
            currentRowWinner = resultsPageHtml.XPathSelectElement(ResultsTableXpathGenerator.GetWinnerXpath(currentRowNo));
            currentRowLoser = resultsPageHtml.XPathSelectElement(ResultsTableXpathGenerator.GetLoserXpath(currentRowNo));
        }

        private bool AreElementsValid()
        {
            return !(currentRowWinner == null && currentRowLoser == null);
        }
    }
}
