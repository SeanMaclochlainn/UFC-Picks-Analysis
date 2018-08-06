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

        public List<RawFightResult> ParseResultTable()
        {
            List<RawFightResult> rawFightResults = new List<RawFightResult>();
            for (int currentRowNo = 1; currentRowNo <= maxNoOfRows; currentRowNo++)
            {
                XElement winner = GetWinnerElement(currentRowNo);
                XElement loser = GetLoserElement(currentRowNo);
                if (IsValidElementList(new List<XElement>() { winner, loser })) 
                    rawFightResults.Add(new RawFightResult(DataSanitizer.GetElementValue(winner), DataSanitizer.GetElementValue(loser)));
            }
            return rawFightResults;
        }

        private XElement GetWinnerElement(int rowNo)
        {
            return resultsPageHtml.XPathSelectElement(XpathGenerator.ResultsPageWinnerXpath(rowNo));
        }

        private XElement GetLoserElement(int rowNo)
        {
            return resultsPageHtml.XPathSelectElement(XpathGenerator.ResultsPageLoserXpath(rowNo));
        }

        private bool IsValidElementList(List<XElement> elements)
        {
            return !elements.Any(e => e == null);
        }
    }
}
