using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.ResultsPage;

namespace FightDataProcessor.WebpageParsing
{
    public class EventWebpagesDataExtractor
    {
        private UfcEvent ufcEvent;

        public EventWebpagesDataExtractor(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ParseAllWebpages()
        {
            ParseResultsPage();
        }

        private void ParseResultsPage()
        {
            ResultsPageDataExtractor resultsPageDataExtractor = new ResultsPageDataExtractor(ufcEvent);
            resultsPageDataExtractor.ExtractResults();
        }

        private void ParsePicksPage()
        {
            
        }

    }
}
