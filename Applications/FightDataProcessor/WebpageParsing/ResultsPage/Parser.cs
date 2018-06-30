using FightData.Domain;
using FightData.Domain.Entities;
using HtmlAgilityPack;

namespace FightDataProcessor.WebpageParsing.ResultsPage
{
    public class Parser
    {
        private HtmlDocument document;
        private EventDataUpdater eventDataUpdater;
        private static int maxNoOfRows = 20;

        public Parser(HtmlDocument document, UfcEvent ufcEvent)
        {
            this.document = document;
            eventDataUpdater = new EventDataUpdater(ufcEvent);
        }

        public void ParseResultsTableRows()
        {
            for (int i = 1; i <= maxNoOfRows; i++)
            {
                ProcessRow(i);
            }
        }

        private void ProcessRow(int rowNo)
        {
            TableRowParser tableRowParser = new TableRowParser(document, rowNo);
            if (tableRowParser.IsValidRow())
            {
                eventDataUpdater.AddFightData(tableRowParser.WinnersName, tableRowParser.LosersName);
            }
        }

    }
}
