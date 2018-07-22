using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.FightData.Domain;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageDataExtractor
    {
        private ResultsTableParser resultsTableParser;
        private static int maxNoOfRows = 20;
        private FightAdder fightAdder;

        public ResultsPageDataExtractor(UfcEvent ufcEvent, FightPicksContext context)
        {
            HtmlDocument resultsPage = HtmlDocumentGenerator.FromWebpage(ufcEvent.GetResultsPage()).HtmlDocument;
            resultsTableParser = new ResultsTableParser(resultsPage);
            fightAdder = new FightAdder(ufcEvent, context);
        }

        public void ExtractResults()
        {
            for (int i = 1; i <= maxNoOfRows; i++)
            {
                ExtractTableRowData(i);
            }
        }

        private void ExtractTableRowData(int rowNo)
        {
            TableRowResult tableRowResult = resultsTableParser.ParseRow(rowNo);
            if (tableRowResult.IsRowContainingFight)
                fightAdder.AddFight(tableRowResult.Winner, tableRowResult.Loser);
        }

    }
}
