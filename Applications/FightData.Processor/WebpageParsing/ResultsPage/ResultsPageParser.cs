using FightData.Domain;
using FightData.Domain.Finders;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
                FinderResult<HtmlNode> winnerResult = FindWinnerElement(currentRowNo);
                FinderResult<HtmlNode> loserResult = FindLoserElement(currentRowNo);

                if (IsValidElementList(new List<FinderResult<HtmlNode>>() { winnerResult, loserResult }))
                    rawFightResults.Add(new RawFightResult(DataSanitizer.GetNodeText(winnerResult.Result), DataSanitizer.GetNodeText(loserResult.Result)));

            }
            return rawFightResults;
        }

        private FinderResult<HtmlNode> FindWinnerElement(int rowNo)
        {
            string xpath = XpathGenerator.ResultsPageWinnerXpath(rowNo);
            return new FinderResult<HtmlNode>(resultsPageHtml.DocumentNode.SelectNodes(xpath)?.FirstOrDefault());
        }

        private FinderResult<HtmlNode> FindLoserElement(int rowNo)
        {
            string xpath = XpathGenerator.ResultsPageLoserXpath(rowNo);
            return new FinderResult<HtmlNode>(resultsPageHtml.DocumentNode.SelectNodes(xpath)?.FirstOrDefault());
        }

        private bool IsValidElementList(List<FinderResult<HtmlNode>> nodes)
        {
            return !nodes.Any(e => e.IsFound() == false);
        }
    }
}
