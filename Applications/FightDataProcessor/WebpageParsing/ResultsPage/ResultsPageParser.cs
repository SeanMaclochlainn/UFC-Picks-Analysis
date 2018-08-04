using FightData.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageParser
    {
        private static int maxNoOfRows = 20;
        private XDocument resultsPageHtml;

        public ResultsPageParser(XDocument resultsPageHtml)
        {
            this.resultsPageHtml = resultsPageHtml;
        }

        public List<FightResult> ParseTableRows()
        {
            List<FightResult> fightResults = new List<FightResult>();
            for (int currentRowNo = 1; currentRowNo <= maxNoOfRows; currentRowNo++)
            {
                XElement winner = GetWinnerElement(currentRowNo);
                XElement loser = GetLoserElement(currentRowNo);
                if (IsValidElementList(new List<XElement>() { winner, loser })) 
                    fightResults.Add(new FightResult(winner.Value, loser.Value));
            }
            return fightResults;
        }

        private XElement GetWinnerElement(int rowNo)
        {
            return resultsPageHtml.XPathSelectElement(ResultsTableXpathGenerator.GetWinnerXpath(rowNo));
        }

        private XElement GetLoserElement(int rowNo)
        {
            return resultsPageHtml.XPathSelectElement(ResultsTableXpathGenerator.GetLoserXpath(rowNo));
        }

        private bool IsValidElementList(List<XElement> elements)
        {
            return !elements.Any(e => e == null);
        }
    }
}
