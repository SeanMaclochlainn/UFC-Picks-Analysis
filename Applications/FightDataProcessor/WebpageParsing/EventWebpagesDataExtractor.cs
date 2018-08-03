using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class EventWebpagesDataExtractor
    {
        private UfcEvent ufcEvent;

        public EventWebpagesDataExtractor(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ExtractWebpagesData()
        {
            ParseResultsPage();
            ParseAllPicksPages();
        }

        private void ParseResultsPage()
        {
            ResultsPageFightExtractor resultsPageDataExtractor = new ResultsPageFightExtractor(ufcEvent);
            resultsPageDataExtractor.ExtractFights();
        }

        private void ParseAllPicksPages()
        {
            PicksPagesDataExtractor picksPagesDataExtractor = new PicksPagesDataExtractor(ufcEvent);
            picksPagesDataExtractor.ExtractAllPages();
        }

    }
}
