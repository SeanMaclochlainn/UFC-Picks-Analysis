using FightData.Domain;
using FightData.Domain.Entities;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class ResultsPageParser
    {
        private HtmlDocument resultsPage;
        private EventUpdater eventUpdater;
        private static int maxNoOfRows = 20;

        public ResultsPageParser(UfcEvent ufcEvent)
        {
            resultsPage = HtmlDocumentGenerator.FromWebpage(ufcEvent.GetResultsPage()).HtmlDocument;
            eventUpdater = new EventUpdater(ufcEvent);
        }

        public void ParseResults()
        {
            for (int i = 1; i <= maxNoOfRows; i++)
            {
                ProcessTableRow(i);
            }
        }

        private void ProcessTableRow(int rowNo)
        {
            //TableRowParser tableRowParser = new TableRowParser(resultsPage, rowNo);
            //if (tableRowParser.ContainsResult())
            //{
            //    eventUpdater.AddFightData(tableRowParser.WinnersName, tableRowParser.LosersName);
            //}
        }

    }
}
