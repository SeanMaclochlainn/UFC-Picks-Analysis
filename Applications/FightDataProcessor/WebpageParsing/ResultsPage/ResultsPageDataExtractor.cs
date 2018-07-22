using FightData.Domain;
using FightData.Domain.Entities;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageDataExtractor
    {
        private ResultsTableParser resultsTableParser;
        private EventUpdater eventUpdater;
        private static int maxNoOfRows = 20;

        public ResultsPageDataExtractor(UfcEvent ufcEvent, FightPicksContext context)
        {
            HtmlDocument resultsPage = HtmlDocumentGenerator.FromWebpage(ufcEvent.GetResultsPage()).HtmlDocument;
            resultsTableParser = new ResultsTableParser(resultsPage);
            eventUpdater = new EventUpdater(ufcEvent, context);
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
                eventUpdater.AddFightData(tableRowResult.Winner, tableRowResult.Loser);
        }

    }
}
