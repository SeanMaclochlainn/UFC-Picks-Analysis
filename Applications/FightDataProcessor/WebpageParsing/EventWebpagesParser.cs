using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.ResultsPage;

namespace FightDataProcessor.WebpageParsing
{
    public class EventWebpagesParser
    {
        private UfcEvent ufcEvent;

        public EventWebpagesParser(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ParseAllWebpages()
        {
            ParseResultsPage();
        }

        private void ParseResultsPage()
        {
            ResultsPageParser resultsTableParser = new ResultsPageParser(ufcEvent);
            resultsTableParser.ParseResults();
        }

        private void ParsePicksPage()
        {
            
        }

    }
}
