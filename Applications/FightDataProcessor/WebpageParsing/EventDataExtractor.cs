using FightData.Domain.Entities;
using FightDataProcessor.WebpageParsing.PicksPages;
using FightDataProcessor.WebpageParsing.ResultsPage;
using System.Collections.Generic;
using System.Linq;

namespace FightDataProcessor.WebpageParsing
{
    public class EventDataExtractor
    {
        private UfcEvent ufcEvent;

        public EventDataExtractor(UfcEvent ufcEvent)
        {
            this.ufcEvent = ufcEvent;
        }

        public void ExtractAllWebpages()
        {
            new ResultsPageFightExtractor(ufcEvent.GetResultsPage()).ExtractResults();
            ExtractAllPicksPages();
        }

        private void ExtractAllPicksPages()
        {
            PicksPagesDataExtractor picksPagesDataExtractor = new PicksPagesDataExtractor(ufcEvent);
            picksPagesDataExtractor.ExtractAllPages();
        }

    }
}
