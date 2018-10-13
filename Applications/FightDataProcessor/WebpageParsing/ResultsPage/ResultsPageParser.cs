using FightData.Domain;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageParser
    {
        private static int maxNoOfRows = 20;
        private HtmlDocument resultsPageHtml;

        public ResultsPageParser(HtmlDocument resultsPageHtml)
        {
            this.resultsPageHtml = resultsPageHtml;
        }

        public List<RawFightResult> ParseResultTable()
        {
            List<RawFightResult> rawFightResults = new List<RawFightResult>();
            for (int currentRowNo = 1; currentRowNo <= maxNoOfRows; currentRowNo++)
            {
                HtmlNode winner = GetWinnerElement(currentRowNo);
                HtmlNode loser = GetLoserElement(currentRowNo);
                if (IsValidElementList(new List<HtmlNode>() { winner, loser })) 
                    rawFightResults.Add(new RawFightResult(DataSanitizer.GetElementValue(winner), DataSanitizer.GetElementValue(loser)));
            }
            return rawFightResults;
        }

        private HtmlNode GetWinnerElement(int rowNo)
        {
            return resultsPageHtml.DocumentNode.SelectNodes(XpathGenerator.ResultsPageWinnerXpath(rowNo))?.FirstOrDefault();
        }

        private HtmlNode GetLoserElement(int rowNo)
        {
            return resultsPageHtml.DocumentNode.SelectNodes(XpathGenerator.ResultsPageLoserXpath(rowNo))?.FirstOrDefault();
        }

        private bool IsValidElementList(List<HtmlNode> nodes)
        {
            return !nodes.Any(e => e == null);
        }
    }
}
